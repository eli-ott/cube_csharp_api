namespace MonApi.API.Addresses.DTOs;

public class ReturnAddressDto
{
    public int AddressId { get; set; }
    public required string AddressLine { get; set; }
    public required string City { get; set; }
    public required string ZipCode { get; set; }
    public required string Country { get; set; }
    public string? Complement { get; set; }
    public DateTime? DeletionTime { get; set; }
    public DateTime UpdateTime { get; set; }
    public DateTime CreationTime { get; set; }
}