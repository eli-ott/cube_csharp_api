using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using MonApi.API.Passwords.DTOs;
using MonApi.Shared.Validators;

namespace MonApi.API.Employees.DTOs;

public class CreateEmployeeDto
{
    [Required]
    [StringLength(255)]
    public required string FirstName { get; set; }
    
    [Required]
    [StringLength(255)]
    public required string LastName { get; set; }
    
    [Required]
    [EmailAddress]
    [StringLength(255)]
    public required string Email { get; set; }
    
    [Required]
    [Phone]
    public required string Phone { get; set; }
    
    [Required]
    public required int RoleId { get; set; }
    
    [Required]
    [PasswordValidator]
    public required string Password { get; set; }

}