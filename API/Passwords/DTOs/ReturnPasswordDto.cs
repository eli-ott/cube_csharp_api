using System.ComponentModel.DataAnnotations;

namespace MonApi.API.Passwords.DTOs;

public class ReturnPasswordDto
{
    public int PasswordId { get; set; }
    [Required] public required string PasswordHash { get; set; } = null!;
    [Required] public required string PasswordSalt { get; set; } = null!;
    [Required] public required int AttemptCount { get; set; }
    public DateTime? ResetDate { get; set; }
    public DateTime? DeletionTime { get; set; }
    [Required] public required DateTime CreationTime { get; set; }
    [Required] public required DateTime UpdateTime { get; set; }
}