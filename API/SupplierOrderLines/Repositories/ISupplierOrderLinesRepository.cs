using MonApi.API.SupplierOrderLines.Models;
using MonApi.Shared.Repositories;

namespace MonApi.API.SupplierOrderLines.Repositories;

public interface ISupplierOrderLinesRepository : IBaseRepository<SupplierOrderLine>
{
    Task AddRangeAsync(List<SupplierOrderLine> lines, CancellationToken cancellationToken = default);
    Task UpdateRange(List<SupplierOrderLine> lines, CancellationToken cancellationToken = default);
    Task RemoveRangeAsync(List<SupplierOrderLine> lines, CancellationToken cancellationToken = default);
}