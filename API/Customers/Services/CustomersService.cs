using System.IdentityModel.Tokens.Jwt;
using System.Security.Authentication;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.IdentityModel.Tokens;
using MonApi.API.Addresses.Extensions;
using MonApi.API.Addresses.Repositories;
using MonApi.API.Customers.DTOs;
using MonApi.API.Customers.Extensions;
using MonApi.API.Customers.Models;
using MonApi.API.Customers.Repositories;
using MonApi.API.Passwords.Extensions;
using MonApi.API.Passwords.Repositories;
using MonApi.Shared.Utils;

namespace MonApi.API.Customers.Services
{
    public class CustomersService : ICustomersService
    {
        private readonly ICustomersRepository _customersRepository;
        private readonly IPasswordRepository _passwordRepository;
        private readonly IAddressRepository _addressRepository;

        public CustomersService(ICustomersRepository customersRepository, IPasswordRepository passwordRepository,
            IAddressRepository addressRepository)
        {
            _customersRepository = customersRepository;
            _passwordRepository = passwordRepository;
            _addressRepository = addressRepository;
        }

        public async Task<ReturnCustomerDto> RegisterCustomer(RegisterDTO registerDto)
        {
            if (await _customersRepository.AnyAsync(u => u.Email == registerDto.Email))
                throw new ArgumentException("Email already exists");

            var address = registerDto.Address.MapToAddressModel();
            var addedAddress = await _addressRepository.AddAsync(address);

            var password = PasswordExtensions.MapToPasswordModel(registerDto);

            // Hachage du mot de passe
            var hashedPassword = PasswordUtils.HashPassword(registerDto.Password, out var salt);
            password.PasswordHash = hashedPassword;
            password.PasswordSalt = Convert.ToBase64String(salt);

            var addedPassword = await _passwordRepository.AddAsync(password);

            var customer = registerDto.MapToCustomerModel(addedPassword, addedAddress);

            var newCustomer = await _customersRepository.AddAsync(customer);

            var newCustomerDetails = await _customersRepository.FindAsync(newCustomer.CustomerId);

            return newCustomerDetails!;
        }

        public async Task<string> LogCustomer(LoginDTO loginDto)
        {
            var foundCustomer = await _customersRepository.FindByEmailAsync(loginDto.Email);

            if (foundCustomer == null)
                throw new KeyNotFoundException("Utilisateur introuvable");

            var passwordValid = PasswordUtils.VerifyPassword(
                loginDto.Password,
                foundCustomer.Password!.PasswordHash,
                Convert.FromBase64String(foundCustomer.Password.PasswordSalt)
            );

            // Si le mot de passe est mauvais on incrémente le nombre d'essais
            if (!passwordValid)
            {
                var password = foundCustomer.Password.MapReturnPasswordDtoToPasswordModel();

                // Si il y a trop d'essais on retourne une erreur
                if (password.AttemptCount >= 3)
                    throw new AuthenticationException(
                        "Nombre d'essais trop important, veuillez réinitialiser votre mot de passe");

                // On incrémente le nombre d'essais
                password.AttemptCount++;

                await _passwordRepository.UpdateAsync(password);

                throw new AuthenticationException("Mot de passe incorrect");
            }

            // Faire une liste de Claims 
            List<Claim> claims = new List<Claim>
            {
                new(ClaimTypes.Role, "Customer"),
                new("CustomerID", foundCustomer.CustomerId.ToString()),
                new("Email", foundCustomer.Email),
                new("FirstName", foundCustomer.FirstName)
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
    }
}