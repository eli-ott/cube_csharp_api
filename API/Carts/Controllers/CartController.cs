using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MonApi.API.Carts.DTOs;
using MonApi.API.Carts.Services;

namespace MonApi.API.Carts.Controllers;

[ApiController]
[Route("cart")]
[Authorize]
public class CartController : ControllerBase
{
    private readonly ICartService _cartService;

    public CartController(ICartService cartService)
    {
        _cartService = cartService;
    }


    [HttpGet("{customerId}")]
    public async Task<ActionResult<ReturnCartDto>> GetCart([FromRoute] int customerId)
    {
        return Ok(await _cartService.GetCartByCustomerId(customerId));
    }

}