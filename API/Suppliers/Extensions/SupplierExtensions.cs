using MonApi.API.Addresses.DTOs;
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
                Contact = createSupplierDTO.Contact,
                Email = createSupplierDTO.Email,
                Phone = createSupplierDTO.Phone,
                Siret = createSupplierDTO.Siret,
                AddressId = address.AddressId
            };
        }

        public static Supplier MapToSupplierModel(this ReturnSupplierDTO returnSupplierDTO, Address address)
        {
            return new Supplier()
            {
                SupplierId = returnSupplierDTO.SupplierId,
                LastName = returnSupplierDTO.LastName,
                FirstName = returnSupplierDTO.FirstName,
                Contact = returnSupplierDTO.Contact,
                Email = returnSupplierDTO.Email,
                Phone = returnSupplierDTO.Phone,
                Siret = returnSupplierDTO.Siret,
                AddressId = address.AddressId
            };
        }

        public static Supplier MapToSupplierModel(this UpdateSupplierDTO updateSupplierDTO, int id, Address address)
        {
            return new Supplier()
            {
                SupplierId = id,
                LastName = updateSupplierDTO.LastName,
                FirstName = updateSupplierDTO.FirstName,
                Contact = updateSupplierDTO.Contact,
                Email = updateSupplierDTO.Email,
                Phone = updateSupplierDTO.Phone,
                Siret = updateSupplierDTO.Siret,
                AddressId = address.AddressId
            };
        }


    }
}
