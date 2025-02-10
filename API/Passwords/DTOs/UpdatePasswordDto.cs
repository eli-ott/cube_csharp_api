using System.ComponentModel.DataAnnotations;

namespace MonApi.API.Passwords.DTOs;

public class UpdatePasswordDto
{
    public int PasswordId { get; set; }
    [Required] public required string PasswordHash { get; set; } = null!;
    [Required] public required string PasswordSalt { get; set; } = null!;
    [Required] public required int AttemptCount { get; set; }
    public DateTime? ResetDate { get; set; }
}