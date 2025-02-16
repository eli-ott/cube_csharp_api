using MonApi.API.OrderLines.Models;
using MonApi.Shared.Repositories;

namespace MonApi.API.OrderLines.Repositories;

public interface IOrderLineRepository : IBaseRepository<OrderLine>
{
    Task AddRangeAsync(List<OrderLine> lines, CancellationToken cancellationToken = default);
    Task UpdateRange(List<OrderLine> lines, CancellationToken cancellationToken = default);
}