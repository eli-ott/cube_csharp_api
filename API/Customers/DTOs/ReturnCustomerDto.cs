using System.ComponentModel.DataAnnotations;
using MonApi.API.Addresses.DTOs;
using MonApi.API.Addresses.Models;
using MonApi.API.Orders.DTOs;
using MonApi.API.Passwords.DTOs;
using MonApi.API.Passwords.Models;
using MonApi.API.Reviews.DTOs;
using MonApi.Shared.Validators;

namespace MonApi.API.Customers.DTOs;

public class ReturnCustomerDto
{
    [Required] public required int CustomerId { get; set; }
    [Required] [StringLength(255)] public required string LastName { get; set; } = null!;
    [Required] [StringLength(255)] public required string FirstName { get; set; } = null!;
    [Required] [StringLength(255)] [EmailAddress] public required string Email { get; set; } = null!;
    [Required] [StringLength(10)] [PhoneValidator] public required string Phone { get; set; } = null!;
    [Required] public required ReturnAddressDto Address { get; set; }
    [Required] public required bool Active { get; set; }
    [Required] public required DateTime? DeletionTime { get; set; }
    [Required] public required DateTime CreationTime { get; set; }
    [Required] public required DateTime UpdateTime { get; set; }
    public string? ValidationId { get; set; }
    public ReturnPasswordDto? Password { get; set; }
    public List<ReturnReviewDto>? Reviews { get; set; }
    public List<ReturnOrderDto>? Orders { get; set; }
}