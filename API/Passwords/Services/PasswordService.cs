using MonApi.API.Passwords.DTOs;
using MonApi.API.Passwords.Extensions;
using MonApi.API.Passwords.Models;
using MonApi.API.Passwords.Repositories;
using MonApi.Shared.Data;
using MonApi.Shared.Services;

namespace MonApi.API.Passwords.Services;

public class PasswordService : BaseService<Password>, IPasswordService
{
    private readonly IPasswordRepository _passwordRepository;

    public PasswordService(IPasswordRepository passwordRepository) : base(passwordRepository)
    {
        _passwordRepository = passwordRepository;
    }

    public async Task UpdateAsync(UpdatePasswordDto passwordDto)
    {
        var passwordCheck = await _passwordRepository.AnyAsync(x => x.PasswordId == passwordDto.PasswordId);
        if (!passwordCheck) throw new KeyNotFoundException("Mot de passe introuvable");

        var password = passwordDto.MapToPasswordModel();
        password.ResetDate = DateTime.UtcNow;

        await _passwordRepository.UpdateAsync(password);
    }
}