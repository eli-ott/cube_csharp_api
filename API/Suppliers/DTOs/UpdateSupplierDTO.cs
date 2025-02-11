using MonApi.API.Addresses.DTOs;
using MonApi.API.Addresses.Models;
using MonApi.Models;

namespace MonApi.API.Suppliers.DTOs
{
    public class UpdateSupplierDTO
    {
        public string LastName { get; set; } = null!;

        public string FirstName { get; set; } = null!;
        public string Siret { get; set; } = null!;

        public string Contact { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string Phone { get; set; } = null!;

        public virtual UpdateAddressDto Address { get; set; } = null!;

    }
}
