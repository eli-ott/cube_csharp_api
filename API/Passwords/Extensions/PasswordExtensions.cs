using MonApi.API.Customers.DTOs;
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

    public static UpdatePasswordDto MapToUpdatePasswordDto(this ReturnPasswordDto password)
    {
        return new UpdatePasswordDto
        {
            AttemptCount = password.AttemptCount,
            PasswordHash = password.PasswordHash,
            PasswordSalt = password.PasswordSalt,
            ResetDate = password.ResetDate
        };
    }

    public static Password MapReturnPasswordDtoToPasswordModel(this ReturnPasswordDto password)
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