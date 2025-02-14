using MonApi.API.Discounts.DTOs;
using MonApi.API.Discounts.Models;
using MonApi.Shared.Repositories;

namespace MonApi.API.Discounts.Repositories;

public interface IDiscountRepository : IBaseRepository<Discount>
{
    Task<ReturnDiscountDto?> FindAsync(int discountId, CancellationToken cancellationToken = default);
}