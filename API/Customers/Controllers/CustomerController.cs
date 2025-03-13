using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MonApi.API.CartLines.DTOs;
using MonApi.API.Carts.DTOs;
using MonApi.API.Customers.DTOs;
using MonApi.API.Customers.Filters;
using MonApi.API.Customers.Services;
using MonApi.Shared.Pagination;

namespace MonApi.API.Customers.Controllers;

[ApiController]
[Route("customers")]
public class CustomerController : ControllerBase
{
    private readonly ICustomersService _customersService;

    public CustomerController(ICustomersService customersService)
    {
        _customersService = customersService;
    }

    [Authorize]
    [HttpGet]
    public async Task<ActionResult<PagedResult<ReturnCustomerDto>>> GetCustomers(
        [FromQuery] CustomerQueryParameters queryParameters)
    {
        return Ok(await _customersService.GetAllCustomers(queryParameters));
    }

    [Authorize]
    [HttpGet("{id}")]
    public async Task<ActionResult<ReturnCustomerDto>> GetCustomer(int id)
    {
        return Ok(await _customersService.GetCustomerById(id));
    }

    [Authorize]
    [HttpGet("{id}/get-cart")]
    public async Task<ActionResult<ReturnCartDto>> GetCart(int id)
    {
        return Ok(await _customersService.GetCart(id));
    }

    [AllowAnonymous]
    [HttpGet("confirm-registration/{email}/{guid}")]
    public async Task<ActionResult> ConfirmRegistration(string email, string guid)
    {
        await _customersService.ConfirmRegistration(email, guid);
        return Ok();
    }

    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<ActionResult<ReturnCustomerDto>> RegisterCustomer(RegisterDTO registerDto)
    {
        return Ok(await _customersService.RegisterCustomer(registerDto));
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<ActionResult> LogCustomer(LoginDTO loginDTO)
    {
        var token = await _customersService.LogCustomer(loginDTO);
        return Ok(new
        {
            token
        });
    }

    [AllowAnonymous]
    [HttpPost("reset-password")]
    public async Task<ActionResult> ResetPassword(ResetPasswordDto resetPasswordDto)
    {
        await _customersService.ResetPassword(resetPasswordDto);
        return Ok();
    }

    [Authorize]
    [HttpPost("{id}/add-to-cart")]
    public async Task<ActionResult> AddToCart(int id, CreateCartLineDto cartLineDto)
    {
        await _customersService.AddToCart(id, cartLineDto);
        return Ok();
    }

    [Authorize]
    [HttpPut("{id}")]
    public async Task<ActionResult<ReturnCustomerDto>> UpdateCustomer(int id, UpdateCustomerDto updateCustomerDto)
    {
        return Ok(await _customersService.UpdateCustomer(id, updateCustomerDto));
    }

    [Authorize]
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteCustomer(int id)
    {
        await _customersService.SoftDeleteCustomer(id);
        return Ok();
    }

    [Authorize]
    [HttpGet("{id}/products/{productId}/review")]
    public async Task<ActionResult> GetCustomerProductReview(int id, int productId)
    {
        return Ok(await _customersService.GetCustomerProductReview(id, productId));
    }
}