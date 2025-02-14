using Microsoft.EntityFrameworkCore;
using MonApi.API.Discounts.DTOs;
using MonApi.API.Discounts.Models;
using MonApi.Shared.Data;
using MonApi.Shared.Repositories;

namespace MonApi.API.Discounts.Repositories;

public class DiscountRepository : BaseRepository<Discount>, IDiscountRepository
{
    public DiscountRepository(StockManagementContext context) : base(context)
    {
    }

    public async Task<ReturnDiscountDto?> FindAsync(int discountId, CancellationToken cancellationToken = default)
    {
        return await _context.Discounts.Where(d => d.DiscountId == discountId)
            .Select(d => new ReturnDiscountDto
            {
                DiscountId = d.DiscountId,
                Name = d.Name,
                Value = d.Value,
                StartDate = d.StartDate,
                EndDate = d.EndDate,
                CreationTime = d.CreationTime,
                UpdateTime = d.UpdateTime,
                ProductId = d.ProductId,
            }).FirstOrDefaultAsync(cancellationToken);
    }
}