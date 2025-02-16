using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MonApi.API.SupplierOrders.DTOs;
using MonApi.API.SupplierOrders.Filters;
using MonApi.API.SupplierOrders.Services;
using MonApi.Shared.Pagination;

namespace MonApi.API.SupplierOrders.Controllers;

[ApiController]
[Route("supplier-orders")]
[Authorize]
public class SupplierOrderController : ControllerBase
{
    private readonly ISupplierOrdersService _ordersService;

    public SupplierOrderController(ISupplierOrdersService ordersService)
    {
        _ordersService = ordersService;
    }

    [HttpGet]
    public async Task<ActionResult<PagedResult<ReturnSupplierOrderDto>>> GetAll(
        [FromQuery] SupplierOrdersQueryParameters queryParameters)
    {
        return Ok(await _ordersService.GetAllOrders(queryParameters));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ReturnSupplierOrderDto>> GetById(int id)
    {
        return Ok(await _ordersService.GetOrderById(id));
    }

    [HttpPost]
    public async Task<ActionResult<ReturnSupplierOrderDto>> CreateOrder(CreateSupplierOrderDto createOrderDto)
    {
        return Ok(await _ordersService.CreateOrderAsync(createOrderDto));
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ReturnSupplierOrderDto>> UpdateOrder(int id, UpdateSupplierOrderDto updateOrderDto)
    {
        return Ok(await _ordersService.UpdateOrderAsync(id, updateOrderDto));
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<ReturnSupplierOrderDto>> DeleteOrder(int id)
    {
        return Ok(await _ordersService.DeleteOrderAsync(id));
    }
}