using MonApi.API.Addresses.DTOs;
using MonApi.API.Addresses.Models;
using MonApi.Models;
using System.ComponentModel.DataAnnotations;

namespace MonApi.API.Suppliers.DTOs
{
    public class ReturnSupplierDTO
    {
        [Required]
        [StringLength(255)]
        public int SupplierId { get; set; }

        [Required]
        [StringLength(255)]
        public string LastName { get; set; } = null!;

        [Required]
        [StringLength(255)]
        public string FirstName { get; set; } = null!;

        [Required]
        [StringLength(255)]
        public string Contact { get; set; } = null!;


        [Required]
        [StringLength(255)]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required]
        [StringLength(15)]
        [Phone]
        public string Phone { get; set; } = null!;

        [Required]
        [StringLength(255)]
        public string Siret { get; set; } = null!;

        public DateTime? DeletionTime { get; set; }

        public DateTime UpdateTime { get; set; }

        public DateTime CreationTime { get; set; }

        [Required]
        public ReturnAddressDto Address { get; set; } = null!;

    }
}
