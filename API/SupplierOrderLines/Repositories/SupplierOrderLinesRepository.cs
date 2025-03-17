using MonApi.API.SupplierOrderLines.Models;
using MonApi.Shared.Data;
using MonApi.Shared.Repositories;

namespace MonApi.API.SupplierOrderLines.Repositories;

public class SupplierOrderLinesRepository : BaseRepository<SupplierOrderLine>, ISupplierOrderLinesRepository
{
    public SupplierOrderLinesRepository(StockManagementContext context) : base(context)
    {
    }

    public async Task AddRangeAsync(List<SupplierOrderLine> lines, CancellationToken cancellationToken = default)
    {
        await _context.Set<SupplierOrderLine>().AddRangeAsync(lines, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateRange(List<SupplierOrderLine> lines, CancellationToken cancellationToken = default)
    {
        _context.Set<SupplierOrderLine>().UpdateRange(lines);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task RemoveRangeAsync(List<SupplierOrderLine> lines, CancellationToken cancellationToken = default)
    {
        _context.Set<SupplierOrderLine>().RemoveRange(lines);
        await _context.SaveChangesAsync(cancellationToken);
    }
}