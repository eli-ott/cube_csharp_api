using MonApi.API.SupplierOrders.DTOs;
using MonApi.API.SupplierOrders.Filters;
using MonApi.API.SupplierOrders.Models;
using MonApi.Shared.Pagination;
using MonApi.Shared.Repositories;

namespace MonApi.API.SupplierOrders.Repositories;

public interface ISupplierOrdersRepository : IBaseRepository<SupplierOrder>
{
    Task<ReturnSupplierOrderDto?> FindAsync(int orderId, CancellationToken cancellationToken = default);

    Task<PagedResult<ReturnSupplierOrderDto>> GetAll(SupplierOrdersQueryParameters queryParameters,
        CancellationToken cancellationToken = default);
}