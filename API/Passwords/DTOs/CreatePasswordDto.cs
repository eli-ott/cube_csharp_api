using System.ComponentModel.DataAnnotations;
using MonApi.Shared.Validators;

namespace MonApi.API.Passwords.DTOs;

public class CreatePasswordDto
{
    [Required]
    [PasswordValidator]
    public required string Password { get; set; }
}