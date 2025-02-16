using MonApi.API.Customers.DTOs;
using MonApi.API.Employees.DTOs;
using MonApi.API.Passwords.DTOs;
using MonApi.API.Passwords.Models;

namespace MonApi.API.Passwords.Extensions;

public static class PasswordExtensions
{
    public static Password MapToPasswordModel(RegisterDTO registerDto)
    {
        return new Password
        {
            PasswordHash = registerDto.Password,
            PasswordSalt = registerDto.Password,
            AttemptCount = 0
        };
    }
    
    public static Password MapToPasswordModel(this CreateEmployeeDto createEmployeeDto)
    {
        return new Password
        {
            PasswordHash = createEmployeeDto.Password,
            PasswordSalt = createEmployeeDto.Password,
            AttemptCount = 0
        };
    }

    public static Password MapToPasswordModel(this ReturnPasswordDto password)
    {
        return new Password
        {
            PasswordId = password.PasswordId,
            PasswordHash = password.PasswordHash,
            PasswordSalt = password.PasswordSalt,
            AttemptCount = password.AttemptCount,
            ResetDate = password.ResetDate
        };
    }
}