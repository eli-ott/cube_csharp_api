using System.ComponentModel.DataAnnotations;

namespace MonApi.API.Employees.DTOs
{
    public class EmployeeLoginDto
    {
        [Required] [EmailAddress] public required string Email { get; set; }

        [Required] public required string Password { get; set; }
    }
}