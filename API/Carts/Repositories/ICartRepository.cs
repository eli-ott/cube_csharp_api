using MonApi.API.Carts.DTOs;
using MonApi.API.Carts.Models;
using MonApi.Shared.Repositories;

namespace MonApi.API.Carts.Repositories;

public interface ICartRepository : IBaseRepository<Cart>
{
    Task<ReturnCartDto?> GetCart(int customerId, CancellationToken cancellationToken = default);
}