using System.ComponentModel.DataAnnotations;
using MonApi.API.Passwords.DTOs;
using MonApi.API.Roles.DTOs;

namespace MonApi.API.Employees.DTOs;

public class ReturnEmployeeDto
{
    [Required]
    public required int EmployeeId { get; set; }
    
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
    public required string Phone { get; set; }
    
    [Required]

    public required ReturnRoleDTO Role { get; set; }

    public int PasswordId { get; set; }
    public ReturnPasswordDto? Password { get; set; }
    
    public DateTime? DeletionTime { get; set; }

    [Required]
    public DateTime CreationTime { get; set; }

    [Required]
    public DateTime UpdateTime { get; set; }
}