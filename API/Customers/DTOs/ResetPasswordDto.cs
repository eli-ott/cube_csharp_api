using System.ComponentModel.DataAnnotations;
using MonApi.Shared.Validators;

namespace MonApi.API.Customers.DTOs;

public class ResetPasswordDto
{
    [Required] [PasswordValidator] public required string Password { get; set; }
}