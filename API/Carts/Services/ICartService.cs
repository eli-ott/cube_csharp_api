using MonApi.API.Carts.DTOs;

namespace MonApi.API.Carts.Services
{
    public interface ICartService
    {
        Task<ReturnCartDto> GetCartByCustomerId(int customerId);
    }
}
