using System.ComponentModel.DataAnnotations;

namespace MonApi.API.Employees.DTOs
{
    public class EmployeeRequestPasswordResetDto
    {
        [Required]
        [StringLength(255)]
        public required string Email { get; set; }
    }
}
