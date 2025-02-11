using MonApi.API.Addresses.Models;
using MonApi.API.Suppliers.DTOs;
using MonApi.API.Suppliers.Models;

namespace MonApi.API.Suppliers.Extensions
{
    public static class SupplierExtensions
    {
        public static Supplier MapToSupplierModel(this CreateSupplierDTO createSupplierDTO, Address address)
        {
            return new Supplier()
            {
                LastName = createSupplierDTO.LastName,
                FirstName = createSupplierDTO.FirstName,
                Email = createSupplierDTO.Email,
                Phone = createSupplierDTO.Phone,
                Siret = createSupplierDTO.Siret,
                AddressId = address.AddressId
            };
        }
    }
}
