using MonApi.API.Addresses.Models;
using MonApi.API.Customers.Models;
using MonApi.API.Customers.DTOs;
using MonApi.API.Passwords.Models;

namespace MonApi.API.Customers.Extensions
{
    public static class CustomerExtensions
    {
        public static Customer MapToCustomerModel(this RegisterDTO registerDTO, Password password, Address address)
        {
            return new Customer
            {
                FirstName = registerDTO.FirstName,
                LastName = registerDTO.LastName,
                Email = registerDTO.Email,
                Phone = registerDTO.Phone,
                Active = false,
                PasswordId = password.PasswordId,
                AddressId = address.AddressId
            };
        }
    }
}
