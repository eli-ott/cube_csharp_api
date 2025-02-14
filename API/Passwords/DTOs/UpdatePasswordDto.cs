using System.ComponentModel.DataAnnotations;
using MonApi.Shared.Validators;

namespace MonApi.API.Passwords.DTOs;

public class UpdatePasswordDto
{
    [Required] public required string PreviousPassword { get; set; }
    [Required] [PasswordValidator] public required string Password { get; set; }
}