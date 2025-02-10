using MonApi.API.Customers.DTOs;
using MonApi.API.Customers.Models;
using MonApi.Shared.Services;

namespace MonApi.API.Customers.Services
{
    public interface ICustomersService : IBaseService<Customer>
    {
        Task<ReturnCustomerDto> RegisterCustomer(RegisterDTO registerDto);
        Task<string> LogCustomer(LoginDTO loginDto);
    }
}
