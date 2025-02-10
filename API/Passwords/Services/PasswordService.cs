using System.Security.Authentication;
using System.Text.Json;
using MonApi.API.Customers.Repositories;
using MonApi.API.Passwords.DTOs;
using MonApi.API.Passwords.Repositories;
using MonApi.Shared.Utils;

namespace MonApi.API.Passwords.Services;

public class PasswordService : IPasswordService
{
    private readonly IPasswordRepository _passwordRepository;

    public PasswordService(IPasswordRepository passwordRepository)
    {
        _passwordRepository = passwordRepository;
    }

    public async Task UpdateAsync(UpdatePasswordDto passwordDto)
    {
        var password = await _passwordRepository.FindAsync(passwordDto.PasswordId);
        if (password == null) throw new NullReferenceException("Le mot de passe est introuvable");
        if (password.DeletionTime != null) throw new BadHttpRequestException("Le mot de passe a été supprimé");

        Console.WriteLine(JsonSerializer.Serialize(password));

        var previousPasswordValid = PasswordUtils.VerifyPassword(
            passwordDto.PreviousPassword,
            password.PasswordHash,
            Convert.FromBase64String(password.PasswordSalt)
        );

        if (!previousPasswordValid)
        {
            // Si il y a trop d'essais on retourne une erreur
            if (password!.AttemptCount >= 3)
                throw new AuthenticationException(
                    "Nombre d'essais trop important, veuillez réinitialiser votre mot de passe");

            // On incrémente le nombre d'essais
            password.AttemptCount++;
            await _passwordRepository.UpdateAsync(password);

            throw new AuthenticationException("Mot de passe incorrect");
        }
        
        var passwordHash = PasswordUtils.HashPassword(passwordDto.Password, out var salt);

        var passwordCheck = await _passwordRepository.AnyAsync(x => x.PasswordId == password.PasswordId);
        if (!passwordCheck) throw new KeyNotFoundException("Mot de passe introuvable");

        password.ResetDate = DateTime.UtcNow;
        password.PasswordHash = passwordHash;
        password.PasswordSalt = Convert.ToBase64String(salt);
        password.AttemptCount = 0;

        await _passwordRepository.UpdateAsync(password);
    }
}