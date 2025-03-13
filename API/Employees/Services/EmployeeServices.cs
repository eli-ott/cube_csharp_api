using System.IdentityModel.Tokens.Jwt;
using System.Security.Authentication;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using MonApi.API.Employees.DTOs;
using MonApi.API.Employees.Models;
using MonApi.API.Employees.Extensions;
using MonApi.API.Employees.Repositories;
using Microsoft.EntityFrameworkCore.Scaffolding;
using Microsoft.IdentityModel.Tokens;
using MonApi.API.Employees.Filters;
using MonApi.API.Passwords.Repositories;
using MonApi.API.Passwords.Extensions;
using MonApi.API.Roles.Models;
using MonApi.API.Suppliers.DTOs;
using MonApi.Shared.Exceptions;
using MonApi.Shared.Pagination;
using MonApi.Shared.Utils;
using Microsoft.AspNetCore.Identity.UI.Services;


namespace MonApi.API.Employees.Services;

public class EmployeeServices : IEmployeeService
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IPasswordRepository _passwordRepository;
    private readonly IEmailSender _emailSender;

    public EmployeeServices(IEmployeeRepository employeeRepository, IPasswordRepository passwordRepository, IEmailSender emailSender)
    {
        _employeeRepository = employeeRepository;
        _passwordRepository = passwordRepository;
        _emailSender = emailSender;
    }
    
    
    public async Task<string> LogEmployee(EmployeeLoginDto loginDto)
        {
            var foundEmployee = await _employeeRepository.FindByEmailAsync(loginDto.Email);

            if (foundEmployee == null)
                throw new KeyNotFoundException("Utilisateur introuvable");
            if (foundEmployee.DeletionTime != null)
                throw new BadHttpRequestException("L'utilisateur est supprimé");

            var passwordValid = PasswordUtils.VerifyPassword(
                loginDto.Password,
                foundEmployee.Password!.PasswordHash,
                Convert.FromBase64String(foundEmployee.Password.PasswordSalt)
            );

            // Si le mot de passe est mauvais on incrémente le nombre d'essais
            if (!passwordValid)
            {
                // Si il y a trop d'essais on retourne une erreur
                if (foundEmployee.Password!.AttemptCount >= 3)
                    throw new AuthenticationException(
                        "Nombre d'essais trop important, veuillez réinitialiser votre mot de passe");

                // On incrémente le nombre d'essais
                foundEmployee.Password.AttemptCount++;

                var passwordModelInvalid = foundEmployee.Password.MapToPasswordModel();
                await _passwordRepository.UpdateAsync(passwordModelInvalid);

                throw new AuthenticationException("Mot de passe incorrect");
            }

            // Mise à jour du nombre d'essais si l'utilisateur se connecte correctement
            foundEmployee.Password.AttemptCount = 0;

            var passwordModel = foundEmployee.Password.MapToPasswordModel();
            await _passwordRepository.UpdateAsync(passwordModel);
            
            // Faire une liste de Claims 
            List<Claim> claims = new List<Claim>
            {
                new(ClaimTypes.Role, "Employee"),
                new("EmployeeID", foundEmployee.EmployeeId.ToString()),
                new("Email", foundEmployee.Email),
                new("FirstName", foundEmployee.FirstName)
            };

            // Signer le token de connexion JWT
            var key = Environment.GetEnvironmentVariable("JWT_SECRET")
                      ?? throw new KeyNotFoundException("JWT_SECRET");
            var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
                SecurityAlgorithms.HmacSha256);

            // On créer un objet de token à partir de la clé de sécurité et l'on y ajoute une expiration, une audience et un issuer de sorte à pouvoir cibler nos clients d'API et éviter les tokens qui trainent trop longtemps dans la nature
            JwtSecurityToken jwt = new JwtSecurityToken(
                claims: claims,
                issuer: "Issuer",
                audience: "Audience",
                signingCredentials: signingCredentials,
                expires: DateTime.UtcNow.AddHours(12));

            // Générer le JWT à partir de l'objet JWT 
            string token = new JwtSecurityTokenHandler().WriteToken(jwt);

            return token;
        }



    public async Task<ReturnEmployeeDto> GetEmployeeByIdAsync(int id)
    {
        ReturnEmployeeDto employee =
            await _employeeRepository.FindAsync(id) ?? throw new KeyNotFoundException("Id not found");

        return employee;
    }

    public async Task<ReturnEmployeeDto> AddEmployeeAsync(CreateEmployeeDto createEmployeeDto)
    {
        if (await _employeeRepository.AnyAsync(s => s.Email == createEmployeeDto.Email))
            throw new ArgumentException("Employee already exist");

        var newPassword = createEmployeeDto.MapToPasswordModel();
        
        // Hachage du mot de passe
        var hashedPassword = PasswordUtils.HashPassword(createEmployeeDto.Password, out var salt);
        newPassword.PasswordHash = hashedPassword;
        newPassword.PasswordSalt = Convert.ToBase64String(salt);
        
        var createdPassword = await _passwordRepository.AddAsync(newPassword);

        var newEmployee = createEmployeeDto.MapToEmployeeModel(createdPassword);

        await _employeeRepository.AddAsync(newEmployee);


        ReturnEmployeeDto newEmployeeDetails = await _employeeRepository.FindAsync(newEmployee.EmployeeId) ??
                                               throw new KeyNotFoundException("Id not found");

        return newEmployeeDetails;
    }

    public async Task<ReturnEmployeeDto> UpdateEmployeeAsync(int id, UpdateEmployeeDto employee)
    {
        var model = await _employeeRepository.FindAsyncWithPassword(id) ??
                    throw new KeyNotFoundException("Id not found");

        if (model.DeletionTime != null) throw new Exception("Employee deleted");

        var updateEmployee = employee.MapToEmployeeModel(id);

        updateEmployee.PasswordId = model.Password!.PasswordId;
        updateEmployee.Role = new Role
        {
            RoleId = updateEmployee.Role.RoleId,
            Name = updateEmployee.Role.Name
        };
        updateEmployee.Email = updateEmployee.Email.ToLower();

        await _employeeRepository.UpdateAsync(updateEmployee);
        ReturnEmployeeDto newModifiedEmployeeDetails =
            await _employeeRepository.FindAsync(id) ?? throw new KeyNotFoundException("Id not found");


        return newModifiedEmployeeDetails;
    }

    public async Task RequestPasswordReset(EmployeeRequestPasswordResetDto requestResetDto)
    {
        var employee = await _employeeRepository.FindByEmailAsync(requestResetDto.Email);

        //  Si l'employé n'existe pas on ne fait rien
        if (employee == null) return;

        if (employee.DeletionTime != null) throw new SoftDeletedException("This employee has been deleted.");

        var password = await _passwordRepository.FindAsync(employee.Password!.PasswordId)
                       ?? throw new KeyNotFoundException("Le mot de passe à réinitialiser est introuvable");

        var baseUrl = Environment.GetEnvironmentVariable("URL_BACKOFFICE")
                     ?? throw new KeyNotFoundException("L'url du front n'est pas disponible");

        var guid = Guid.NewGuid();

        password.ResetToken = guid.ToString();

        await _passwordRepository.UpdateAsync(password);

        var completeUrl = $"{baseUrl}/forgot-password/confirmation/{guid}";

        var emailContent =
            $"Pour réinitialiser votre mot de passe veuillez utiliser sur le lien suivant : {completeUrl}";
        var emailSubject = "Réinitialisation de mot de passe";

        await _emailSender.SendEmailAsync(requestResetDto.Email, emailSubject, emailContent);

        return;
    }

    public async Task ResetPassword(string guid, ResetEmployeePasswordDto resetPasswordDto)
    {
        var employee = await _employeeRepository.FirstOrDefaultAsync(employee => employee.Password!.ResetToken == guid)
                       ?? throw new KeyNotFoundException("L'employé n'existe pas");

        var password = await _passwordRepository.FindAsync(employee.PasswordId)
                       ?? throw new KeyNotFoundException("Le mot de passe à réinitialiser est introuvable");

        if (password.DeletionTime != null) throw new SoftDeletedException("This password has been deleted.");

        var passwordHash = PasswordUtils.HashPassword(resetPasswordDto.Password, out var salt);

        password.PasswordHash = passwordHash;
        password.PasswordSalt = Convert.ToBase64String(salt);
        password.AttemptCount = 0;
        password.ResetDate = DateTime.UtcNow;
        password.ResetToken = null;

        await _passwordRepository.UpdateAsync(password);
    }

    public async Task<ReturnEmployeeDto> SoftDeleteEmployeeAsync(int id)
    {
        ReturnEmployeeDto employee =
            await _employeeRepository.FindAsyncWithPassword(id) ?? throw new KeyNotFoundException("Id not found");
        if (employee.DeletionTime != null) throw new Exception("Employee deleted");

        var foundPassword = await _passwordRepository.FindAsync(employee.Password!.PasswordId)
                            ?? throw new NullReferenceException("Can't find password");
        if (foundPassword.DeletionTime != null)
            Console.Error.WriteLine($"" +
                                    $"Password with id {foundPassword.PasswordId} already deleted");

        foundPassword.DeletionTime = DateTime.UtcNow;
        await _passwordRepository.UpdateAsync(foundPassword);

        Employee employeeToDelete = employee.MapToEmployeeModel(id);

        employeeToDelete.DeletionTime = DateTime.UtcNow;

        employee.DeletionTime = employeeToDelete.DeletionTime;

        Console.WriteLine(JsonSerializer.Serialize(employeeToDelete.Role));
        await _employeeRepository.UpdateAsync(employeeToDelete);

        return employee;
    }

    public async Task<PagedResult<ReturnEmployeeDto>> GetAllEmployeesAsync(EmployeeQueryParameters queryParameters)
    {
        var employee = await _employeeRepository.GetAll(queryParameters);
        return employee;
    }
}