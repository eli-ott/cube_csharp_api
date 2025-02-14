using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
    public async Task<ActionResult<PagedResult<ReturnCustomerDto>>> GetCustomers([FromQuery] CustomerQueryParameters queryParameters)
    {
        return Ok(await _customersService.GetAllCustomers(queryParameters));
    }

    [Authorize]
    [HttpGet("{id}")]
    public async Task<ActionResult<ReturnCustomerDto>> GetCustomer(int id)
    {
        return Ok(await _customersService.GetCustomerById(id));
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
}