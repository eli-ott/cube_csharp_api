using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MonApi.API.Orders.DTOs;
using MonApi.API.Orders.Filters;
using MonApi.API.Orders.Services;
using MonApi.Shared.Pagination;

namespace MonApi.API.Orders.Controllers;

[ApiController]
[Route("orders")]
[Authorize]
public class OrderController : ControllerBase
{
    private readonly IOrdersService _ordersService;

    public OrderController(IOrdersService ordersService)
    {
        _ordersService = ordersService;
    }

    [HttpGet]
    public async Task<ActionResult<PagedResult<ReturnOrderDto>>> GetAll(
        [FromQuery] OrderQueryParameters queryParameters)
    {
        return Ok(await _ordersService.GetAllOrders(queryParameters));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ReturnOrderDto>> GetById(int id)
    {
        return Ok(await _ordersService.GetOrderById(id));
    }

    [HttpPost]
    public async Task<ActionResult<ReturnOrderDto>> CreateOrder(CreateOrderDto createOrderDto)
    {
        return Ok(await _ordersService.CreateOrderAsync(createOrderDto));
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<ReturnOrderDto>> UpdateOrder(int id, UpdateOrderDto updateOrderDto)
    {
        return Ok(await _ordersService.UpdateOrderAsync(id, updateOrderDto));
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<ReturnOrderDto>> DeleteOrder(int id)
    {
        return Ok(await _ordersService.DeleteOrderAsync(id));
    }
}