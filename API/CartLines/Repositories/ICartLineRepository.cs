using MonApi.API.CartLines.DTOs;
using MonApi.API.CartLines.Models;
using MonApi.Shared.Repositories;

namespace MonApi.API.CartLines.Repositories;

public interface ICartLineRepository : IBaseRepository<CartLine>
{
    Task<ReturnCartLineDto?> GetCartLine(int productId, int cartId, CancellationToken cancellationToken = default);
    Task RemoveRangeAsync(List<CartLine> lines, CancellationToken cancellationToken = default);
}