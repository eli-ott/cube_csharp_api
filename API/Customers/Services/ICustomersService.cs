using MonApi.API.CartLines.DTOs;
using MonApi.API.Carts.DTOs;
using MonApi.API.Customers.DTOs;
using MonApi.API.Customers.Filters;
using MonApi.API.Customers.Models;
using MonApi.API.Passwords.DTOs;
using MonApi.API.Reviews.DTOs;
using MonApi.Shared.Pagination;

namespace MonApi.API.Customers.Services
{
    public interface ICustomersService
    {
        Task<ReturnCustomerDto> RegisterCustomer(RegisterDTO registerDto);
        Task<string> LogCustomer(LoginDTO loginDto);
        Task ResetPassword(ResetPasswordDto resetPasswordDto);
        Task ConfirmRegistration(string email, string guid);
        Task<PagedResult<ReturnCustomerDto>> GetAllCustomers(CustomerQueryParameters queryParameters);
        Task<ReturnCustomerDto> GetCustomerById(int customerId);
        Task<ReturnCustomerDto> UpdateCustomer(int customerId, UpdateCustomerDto updateCustomerDto);
        Task SoftDeleteCustomer(int customerId);
        Task<ReturnCartDto> GetCart(int customerId);
        Task AddToCart(int customerId, CreateCartLineDto cartLineDto);
        Task<ReturnReviewDto?> GetCustomerProductReview(int customerId, int productId);
    }
}
