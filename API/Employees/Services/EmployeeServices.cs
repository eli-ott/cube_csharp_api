using System.Text.Json;
using MonApi.API.Employees.DTOs;
using MonApi.API.Employees.Models;
using MonApi.API.Employees.Extensions;
using MonApi.API.Employees.Repositories;
using Microsoft.EntityFrameworkCore.Scaffolding;
using MonApi.API.Employees.Filters;
using MonApi.API.Passwords.Repositories;
using MonApi.API.Passwords.Extensions;
using MonApi.API.Roles.Models;
using MonApi.API.Suppliers.DTOs;
using MonApi.Shared.Exceptions;
using MonApi.Shared.Pagination;
using MonApi.Shared.Utils;


namespace MonApi.API.Employees.Services;

public class EmployeeServices : IEmployeeService
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IPasswordRepository _passwordRepository;

    public EmployeeServices(IEmployeeRepository employeeRepository, IPasswordRepository passwordRepository)
    {
        _employeeRepository = employeeRepository;
        _passwordRepository = passwordRepository;
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

        await _employeeRepository.UpdateAsync(updateEmployee);
        ReturnEmployeeDto newModifiedEmployeeDetails =
            await _employeeRepository.FindAsync(id) ?? throw new KeyNotFoundException("Id not found");


        return newModifiedEmployeeDetails;
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