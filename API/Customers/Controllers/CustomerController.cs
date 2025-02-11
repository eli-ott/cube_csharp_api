using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MonApi.API.Customers.DTOs;
using MonApi.API.Customers.Services;

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
}