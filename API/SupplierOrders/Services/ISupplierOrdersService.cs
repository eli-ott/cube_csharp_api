using MonApi.API.SupplierOrders.DTOs;
using MonApi.API.SupplierOrders.Filters;
using MonApi.Shared.Pagination;

namespace MonApi.API.SupplierOrders.Services;

public interface ISupplierOrdersService
{
    Task<PagedResult<ReturnSupplierOrderDto>> GetAllOrders(SupplierOrdersQueryParameters queryParameters);
    Task<ReturnSupplierOrderDto?> GetOrderById(int orderId);
    Task<ReturnSupplierOrderDto> CreateOrderAsync(CreateSupplierOrderDto createOrderDto);
    Task<ReturnSupplierOrderDto> UpdateOrderAsync(int orderId, UpdateSupplierOrderDto updateOrderDto);
    Task<ReturnSupplierOrderDto> DeleteOrderAsync(int orderId);
}