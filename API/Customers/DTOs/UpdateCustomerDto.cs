using System.ComponentModel.DataAnnotations;
using MonApi.API.Addresses.DTOs;
using MonApi.API.Addresses.Models;
using MonApi.Shared.Validators;

namespace MonApi.API.Customers.DTOs;

public class UpdateCustomerDto
{
    [Required] [StringLength(255)] public required string LastName { get; set; } = null!;
    [Required] [StringLength(255)] public required string FirstName { get; set; } = null!;
    [Required] [StringLength(255)] [EmailAddress] public required string Email { get; set; } = null!;
    [Required] [StringLength(10)] [PhoneValidator] public required string Phone { get; set; } = null!;
    [Required] public required Address Address { get; set; }
}