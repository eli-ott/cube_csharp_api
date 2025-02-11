using MonApi.Shared.Validators;
using System.ComponentModel.DataAnnotations;
using MonApi.API.Addresses.DTOs;
using MonApi.API.Addresses.Models;

namespace MonApi.API.Customers.DTOs;

public class RegisterDTO
{
    [StringLength(255)] public required string FirstName { get; set; }

    [StringLength(255)] public required string LastName { get; set; }

    [StringLength(15)] [PhoneValidator] public required string Phone { get; set; }
    [StringLength(255)] [EmailAddress] public required string Email { get; set; }
    [PasswordValidator] public required string Password { get; set; }
    public required CreateAddressDto Address { get; set; }
}