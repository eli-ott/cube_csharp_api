using System.ComponentModel.DataAnnotations;
using MonApi.API.Addresses.DTOs;
using MonApi.API.Addresses.Models;
using MonApi.API.Passwords.DTOs;
using MonApi.API.Passwords.Models;

namespace MonApi.API.Customers.DTOs;

public class ReturnCustomerDto
{
    [Required] public required int CustomerId { get; set; }
    [Required] [StringLength(255)] public required string LastName { get; set; } = null!;
    [Required] [StringLength(255)] public required string FirstName { get; set; } = null!;
    [Required] [StringLength(255)] public required string Email { get; set; } = null!;
    [Required] [StringLength(10)] public required string Phone { get; set; } = null!;
    [Required] public required ReturnAddressDto Address { get; set; }
    [Required] public required bool Active { get; set; }
    [Required] public required DateTime? DeletionTime { get; set; }
    [Required] public required DateTime CreationTime { get; set; }
    [Required] public required DateTime UpdateTime { get; set; }
    public ReturnPasswordDto? Password { get; set; }
}