using MonApi.API.Addresses.Models;
using MonApi.API.Customers.Models;
using MonApi.API.Customers.DTOs;
using MonApi.API.Passwords.Models;

namespace MonApi.API.Customers.Extensions
{
    public static class CustomerExtensions
    {
        public static Customer MapToCustomerModel(this RegisterDTO registerDTO, Password password, Address address, Guid guid)
        {
            return new Customer
            {
                FirstName = registerDTO.FirstName,
                LastName = registerDTO.LastName,
                Email = registerDTO.Email,
                Phone = registerDTO.Phone,
                Active = false,
                PasswordId = password.PasswordId,
                AddressId = address.AddressId,
                ValidationId = guid.ToString()
            };
        }
        
        public static Customer MapToCustomerModel(this ReturnCustomerDto customerDto)
        {
            return new Customer
            {
                CustomerId = customerDto.CustomerId,
                FirstName = customerDto.FirstName,
                LastName = customerDto.LastName,
                Email = customerDto.Email,
                Phone = customerDto.Phone,
                Active = customerDto.Active,
                ValidationId = customerDto.ValidationId!,
                PasswordId = customerDto.Password!.PasswordId,
                AddressId = customerDto.Address.AddressId,
                DeletionTime = customerDto.DeletionTime
            };
        }
    }
}
