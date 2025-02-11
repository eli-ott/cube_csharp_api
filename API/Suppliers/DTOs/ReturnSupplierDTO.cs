using MonApi.API.Addresses.DTOs;
using MonApi.API.Addresses.Models;
using MonApi.Models;

namespace MonApi.API.Suppliers.DTOs
{
    public class ReturnSupplierDTO
    {
        public int SupplierId { get; set; }

        public string LastName { get; set; } = null!;

        public string FirstName { get; set; } = null!;

        public string Contact { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string Phone { get; set; } = null!;

        public string Siret { get; set; } = null!;

        public DateTime? DeletionTime { get; set; }

        public DateTime UpdateTime { get; set; }

        public DateTime CreationTime { get; set; }

        public ReturnAddressDto Address { get; set; } = null!;

    }
}
