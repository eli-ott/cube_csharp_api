using System.ComponentModel.DataAnnotations;

namespace MonApi.API.Passwords.DTOs;

public class UpdatePasswordDto
{
    public int PasswordId { get; set; }
    [Required] public required string PasswordHash { get; set; }
    [Required] public required string PasswordSalt { get; set; }
}