using MonApi.API.Passwords.DTOs;

namespace MonApi.API.Passwords.Services;

public interface IPasswordService
{
    Task UpdateAsync(UpdatePasswordDto passwordDto, int id);
}