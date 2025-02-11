using MonApi.API.Customers.DTOs;
using MonApi.API.Customers.Models;
using MonApi.API.Passwords.DTOs;

namespace MonApi.API.Customers.Services
{
    public interface ICustomersService
    {
        Task<ReturnCustomerDto> RegisterCustomer(RegisterDTO registerDto);
        Task<string> LogCustomer(LoginDTO loginDto);
        Task ResetPassword(ResetPasswordDto resetPasswordDto);
        Task ConfirmRegistration(string email, string guid);
    }
}
