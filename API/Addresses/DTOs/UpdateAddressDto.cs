using System.ComponentModel.DataAnnotations;

namespace MonApi.API.Addresses.DTOs
{
    public class UpdateAddressDto
    {
        [Required] public required int AddressId { get; set; }
        [Required][StringLength(255)] public required string AddressLine { get; set; }
        [Required][StringLength(255)] public required string City { get; set; }
        [Required][StringLength(10)] public required string ZipCode { get; set; }
        [Required][StringLength(255)] public required string Country { get; set; }
        [StringLength(255)] public string? Complement { get; set; }
    }
}
