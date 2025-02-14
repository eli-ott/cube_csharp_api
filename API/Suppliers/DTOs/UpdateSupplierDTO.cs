using System.ComponentModel.DataAnnotations;
using MonApi.API.Addresses.DTOs;
using MonApi.API.Addresses.Models;
using MonApi.Models;

namespace MonApi.API.Suppliers.DTOs
{
    public class UpdateSupplierDTO
    {
        [Required] [StringLength(255)] public required string LastName { get; set; }
        [Required] [StringLength(255)] public required string FirstName { get; set; }
        [Required] [StringLength(255)] public required string Contact { get; set; }

        [Required]
        [StringLength(255)]
        [EmailAddress]
        public required string Email { get; set; }

        [Required] [StringLength(15)] [Phone] public required string Phone { get; set; }
        [Required] [StringLength(255)] public required string Siret { get; set; }
        [Required] public required UpdateAddressDto Address { get; set; }
    }
}