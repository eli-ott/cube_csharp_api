using MonApi.API.Auth.DTOs;
using MonApi.API.Customers.Extensions;
using MonApi.API.Customers.Models;
using MonApi.API.Customers.Repositories;
using MonApi.API.Passwords.Extensions;
using MonApi.API.Passwords.Repositories;
using MonApi.Shared.Utils;

namespace MonApi.API.Auth.Services;

public class AuthService(ICustomersRepository customersRepository, IPasswordRepository passwordRepository)
{
    public async Task<bool> RegisterCustomer(RegisterDTO registerDTO)
    {
        if (await customersRepository.AnyAsync(u => u.Email == registerDTO.Email))
            return false;

        var password = registerDTO.MapToPasswordModel();

        // Hachage du mot de passe
        var hashedPassword = PasswordUtils.HashPassword(registerDTO.Password, out var salt);
        password.Hash = hashedPassword;
        password.Salt = Convert.ToBase64String(salt);

        var addedPassword = await passwordRepository.AddAsync(password);

        var customer = registerDTO.MapToCustomerModel(addedPassword);

        return await customersRepository.AddAsync(customer) is not null;
    }

    public async Task<Customer?> LogCustomer(LoginDTO loginDTO)
    {
        var customer = await customersRepository.FindByEmailAsync(loginDTO.Email);
        if (customer == null)
            return null;

        var isPasswordValid = PasswordUtils.VerifyPassword(
            loginDTO.Password,
            customer.Password.Hash,
            Convert.FromBase64String(customer.Password.Salt)
        );

        if (!isPasswordValid)
            return null;

        return customer;
    }
}
