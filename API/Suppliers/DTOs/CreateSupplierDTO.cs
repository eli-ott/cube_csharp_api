using MonApi.API.Addresses.DTOs;
using System.ComponentModel.DataAnnotations;


namespace MonApi.API.Suppliers.DTOs
{
    public class CreateSupplierDTO
    {
        [Required]
        [StringLength(255)]
        public string LastName { get; set; }

        [Required]
        [StringLength(255)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(255)]
        public string Contact { get; set; }

        [Required]
        [StringLength(255)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(15)]
        [Phone]
        public string Phone { get; set; }

        [Required]
        [StringLength(255)]
        public string Siret { get; set; }

        [Required]
        public CreateAddressDto Address { get; set; }
    }
}
