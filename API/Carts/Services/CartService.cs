using MonApi.API.Carts.DTOs;
using MonApi.API.Carts.Repositories;

namespace MonApi.API.Carts.Services
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;

        public CartService(ICartRepository cartRepository) {
        _cartRepository = cartRepository;
        }

        public async Task<ReturnCartDto> GetCartByCustomerId(int customerId) {
            ReturnCartDto returnedCart = await _cartRepository.GetCart(customerId) ?? throw new KeyNotFoundException("Not cart associated to this user");
            return returnedCart;
        }
    }
}
