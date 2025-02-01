using MonApi.API.Customers.Models;
using MonApi.API.Auth.DTOs;
using MonApi.API.Passwords.Models;

namespace MonApi.API.Customers.Extensions
{
    public static class CustomerExtensions
    {
        public static Customer MapToCustomerModel(this RegisterDTO registerDTO, Password password)
        {
            return new Customer()
            {
                CustomerId = Guid.NewGuid().ToString(),
                FirstName = registerDTO.FirstName,
                LastName = registerDTO.LastName,
                Email = registerDTO.Email,
                PhoneNumber = registerDTO.PhoneNumber,
                Active = true,
                PasswordId = password.PasswordId,
            };
        }
    }
}
