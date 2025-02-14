using System.ComponentModel.DataAnnotations;
using MonApi.Shared.Validators;

namespace MonApi.API.Addresses.DTOs;

public class ReturnAddressDto
{
    public int AddressId { get; set; }
    [Required] [StringLength(255)] public required string AddressLine { get; set; }
    [Required] [StringLength(255)] public required string City { get; set; }

    [Required]
    [StringLength(10)]
    [ZipCodeValidator]
    public required string ZipCode { get; set; }

    [Required] [StringLength(255)] public required string Country { get; set; }
    [StringLength(255)] public string? Complement { get; set; }
    public DateTime? DeletionTime { get; set; }
    public DateTime UpdateTime { get; set; }
    public DateTime CreationTime { get; set; }
}