using MonApi.API.Orders.DTOs;
using MonApi.API.Orders.Filters;
using MonApi.API.Orders.Models;
using MonApi.Shared.Pagination;
using MonApi.Shared.Repositories;

namespace MonApi.API.Orders.Repositories;

public interface IOrderRepository : IBaseRepository<Order>
{
    Task<ReturnOrderDto?> FindAsync(int orderId, CancellationToken cancellationToken = default);

    Task<PagedResult<ReturnOrderDto>> GetAll(OrderQueryParameters queryParameters,
        CancellationToken cancellationToken = default);
}