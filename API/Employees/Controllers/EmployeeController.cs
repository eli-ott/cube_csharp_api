using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using MonApi.API.Employees.DTOs;
using MonApi.API.Employees.Filters;
using MonApi.API.Employees.Models;
using MonApi.API.Employees.Services;

namespace MonApi.API.Employees.Controllers;

[ApiController]
[Route("employees")]
public class EmployeeController : ControllerBase
{
    private readonly IEmployeeService _employeeService;

    public EmployeeController(IEmployeeService employeeService)
    {
        _employeeService = employeeService;
    }
    
    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<ActionResult> LogCustomer(EmployeeLoginDto loginDto)
    {
        var token = await _employeeService.LogEmployee(loginDto);
        return Ok(new
        {
            token
        });
    }
    
    [Authorize]
    [HttpPost]
    public async Task<IActionResult> AddEmployeeAsync([FromBody] CreateEmployeeDto createEmployeeDto)
    {
        var isAdded = await _employeeService.AddEmployeeAsync(createEmployeeDto);
        return Ok(isAdded);
    }
    
    [Authorize]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetEmployeeByIdAsync([FromRoute] int id)
    {
        var employee = await _employeeService.GetEmployeeByIdAsync(id);
        return Ok(employee);
    }
    
    [Authorize]
    [HttpGet]
    public async Task<IActionResult> GetAllEmployeesAsync([FromQuery] EmployeeQueryParameters queryParameters)
    {
        var employees = await _employeeService.GetAllEmployeesAsync(queryParameters);
        return Ok(employees);
    }
    
    [Authorize]
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateEmployeeAsync([FromRoute] int id, [FromBody] UpdateEmployeeDto updateEmployeeDto)
    {
        var isAdded = await _employeeService.UpdateEmployeeAsync(id, updateEmployeeDto);
        return Ok(isAdded);
    }

    [AllowAnonymous]
    [HttpPost("request-password-reset")]
    public async Task<ActionResult> RequestPasswordReset([FromBody] EmployeeRequestPasswordResetDto requestResetDto)
    {
        await _employeeService.RequestPasswordReset(requestResetDto);
        return Ok();
    }

    [AllowAnonymous]
    [HttpPost("reset-password/{guid}")]
    public async Task<ActionResult> ResetPassword([FromRoute] string guid, [FromBody] ResetEmployeePasswordDto resetPasswordDto)
    {
        await _employeeService.ResetPassword(guid, resetPasswordDto);
        return Ok();
    }

    [Authorize]
    [HttpDelete("{id}")]
    public async Task<IActionResult> SoftDeleteEmployeeAsync([FromRoute] int id)
    {
        await _employeeService.SoftDeleteEmployeeAsync(id);
        return Ok();
    }
    
    
}