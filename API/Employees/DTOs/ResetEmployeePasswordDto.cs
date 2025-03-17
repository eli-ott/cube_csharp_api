using System.ComponentModel.DataAnnotations;
using MonApi.Shared.Validators;

namespace MonApi.API.Employees.DTOs;

public class ResetEmployeePasswordDto
{
    [Required] [PasswordValidator] public required string Password { get; set; }
}