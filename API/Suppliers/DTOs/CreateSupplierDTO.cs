using MonApi.API.Addresses.DTOs;
using System.ComponentModel.DataAnnotations;


namespace MonApi.API.Suppliers.DTOs
{
    public class CreateSupplierDTO
    {
        [Required]
        [StringLength(255)]
        public required string LastName { get; set; }

        [Required]
        [StringLength(255)]
        public required string FirstName { get; set; }

        [Required]
        [StringLength(255)]
        public required string Contact { get; set; }

        [Required]
        [StringLength(255)]
        [EmailAddress]
        public required string Email { get; set; }

        [Required]
        [StringLength(15)]
        [Phone]
        public required string Phone { get; set; }

        [Required]
        [StringLength(255)]
        public required string Siret { get; set; }

        [Required]
        public required CreateAddressDto Address { get; set; }
    }
}
