using MonApi.API.Orders.DTOs;
using MonApi.API.Orders.Filters;
using MonApi.Shared.Pagination;

namespace MonApi.API.Orders.Services;

public interface IOrdersService
{
    Task<PagedResult<ReturnOrderDto>> GetAllOrders(OrderQueryParameters queryParameters);
    Task<ReturnOrderDto?> GetOrderById(int orderId);
    Task<ReturnOrderDto> CreateOrderAsync(CreateOrderDto createOrderDto);
    Task<ReturnOrderDto> UpdateOrderAsync(int orderId, UpdateOrderDto updateOrderDto);
    Task<ReturnOrderDto> DeleteOrderAsync(int orderId);
}