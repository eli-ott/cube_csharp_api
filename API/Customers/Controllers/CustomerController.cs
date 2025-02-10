using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using MonApi.API.Addresses.Extensions;
using MonApi.API.Addresses.Repositories;
using MonApi.API.Customers.DTOs;
using MonApi.API.Customers.Extensions;
using MonApi.API.Customers.Models;
using MonApi.API.Customers.Repositories;
using MonApi.API.Customers.Services;
using MonApi.API.Passwords.DTOs;
using MonApi.API.Passwords.Extensions;
using MonApi.API.Passwords.Models;
using MonApi.API.Passwords.Repositories;
using MonApi.Shared.Utils;

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
    } }