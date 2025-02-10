using MonApi.API.Customers.DTOs;
using MonApi.API.Customers.Models;

namespace MonApi.API.Customers.Services
{
    public interface ICustomersService
    {
        Task<ReturnCustomerDto> RegisterCustomer(RegisterDTO registerDto);
        Task<string> LogCustomer(LoginDTO loginDto);
    }
}
