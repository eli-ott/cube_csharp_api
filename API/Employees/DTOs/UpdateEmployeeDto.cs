using System.ComponentModel.DataAnnotations;

namespace MonApi.API.Employees.DTOs;

public class UpdateEmployeeDto
{
    [Required]
    [MaxLength(255)]
    public required string FirstName { get; set; }
    
    [Required]
    [MaxLength(255)]
    public required string LastName { get; set; }
    
    [Required]
    [EmailAddress]
    [MaxLength(255)]
    public required string Email { get; set; }
    
    [Required]
    [Phone]
    [MaxLength(255)]
    public required string Phone { get; set; }
    
    [Required]
    public required int RoleId { get; set; }
    
}