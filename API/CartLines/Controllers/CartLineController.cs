using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MonApi.API.CartLines.DTOs;
using MonApi.API.CartLines.Services;

namespace MonApi.API.CartLines.Controllers;

[ApiController]
[Route("cart-lines")]
[Authorize]
public class CartLineController : ControllerBase
{
    private readonly ICartLineService _cartLineService;

    public CartLineController(ICartLineService cartLineService)
    {
        _cartLineService = cartLineService;
    }

    [HttpPost("toggle-set-aside")]
    public async Task<ActionResult<ReturnCartLineDto>> ToggleIsSetAside(UpdateSetAsideDto updateSetAsideDto)
    {
        return Ok(await _cartLineService.ToggleIsSetAside(updateSetAsideDto));
    }
    
    [HttpPut]
    public async Task<ActionResult<ReturnCartLineDto>> UpdateCartLine(UpdateCartLineDto updateCartLineDto)
    {
        return Ok(await _cartLineService.UpdateCartLine(updateCartLineDto));
    }

    [HttpDelete]
    public async Task<ActionResult<ReturnCartLineDto>> DeleteCartLine(DeleteCartLineDto deleteCartLineDto)
    {
        return Ok(await _cartLineService.DeleteCartLine(deleteCartLineDto));
    }
}