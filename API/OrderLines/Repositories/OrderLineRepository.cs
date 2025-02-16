using MonApi.API.OrderLines.Models;
using MonApi.Shared.Data;
using MonApi.Shared.Repositories;

namespace MonApi.API.OrderLines.Repositories;

public class OrderLineRepository : BaseRepository<OrderLine>, IOrderLineRepository
{
    public OrderLineRepository(StockManagementContext context) : base(context)
    {
    }

    public async Task AddRangeAsync(List<OrderLine> lines, CancellationToken cancellationToken = default)
    {
        await _context.Set<OrderLine>().AddRangeAsync(lines, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateRange(List<OrderLine> lines, CancellationToken cancellationToken = default)
    {
        _context.Set<OrderLine>().UpdateRange(lines);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task RemoveRangeAsync(List<OrderLine> lines, CancellationToken cancellationToken = default)
    {
        _context.Set<OrderLine>().RemoveRange(lines);
        await _context.SaveChangesAsync(cancellationToken);
    }
}