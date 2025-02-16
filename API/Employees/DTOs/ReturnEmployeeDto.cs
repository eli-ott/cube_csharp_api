using System.ComponentModel.DataAnnotations;

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

    public int RoleId { get; set; }

    [Required]
    public int PasswordId { get; set; }
    
    public DateTime? DeletionTime { get; set; }

    [Required]
    public DateTime CreationTime { get; set; }

    [Required]
    public DateTime UpdateTime { get; set; }
}