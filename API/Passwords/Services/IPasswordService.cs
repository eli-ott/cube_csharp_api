using MonApi.API.Passwords.DTOs;
using MonApi.API.Passwords.Models;
using MonApi.Shared.Data;
using MonApi.Shared.Services;

namespace MonApi.API.Passwords.Services;

public interface IPasswordService : IBaseService<Password>
{
    Task UpdateAsync(UpdatePasswordDto passwordDto);
}