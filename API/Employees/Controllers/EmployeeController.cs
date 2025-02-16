using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MonApi.API.Employees.DTOs;
using MonApi.API.Employees.Filters;
using MonApi.API.Employees.Models;
using MonApi.API.Employees.Services;

namespace MonApi.API.Employees.Controllers;

[ApiController]
[Route("employees")]
[Authorize]

public class EmployeeController : ControllerBase
{
    private readonly IEmployeeService _employeeService;

    public EmployeeController(IEmployeeService employeeService)
    {
        _employeeService = employeeService;
    }
    
    [HttpPost]
    public async Task<IActionResult> AddEmployeeAsync([FromBody] CreateEmployeeDto createEmployeeDto)
    {
        var isAdded = await _employeeService.AddEmployeeAsync(createEmployeeDto);
        return Ok(isAdded);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetEmployeeByIdAsync([FromRoute] int id)
    {
        var employee = await _employeeService.GetEmployeeByIdAsync(id);
        return Ok(employee);
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllEmployeesAsync([FromQuery] EmployeeQueryParameters queryParameters)
    {
        var employees = await _employeeService.GetAllEmployeesAsync(queryParameters);
        return Ok(employees);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateEmployeeAsync([FromRoute] int id, [FromBody] UpdateEmployeeDto updateEmployeeDto)
    {
        var isAdded = await _employeeService.UpdateEmployeeAsync(id, updateEmployeeDto);
        return Ok(isAdded);
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> SoftDeleteEmployeeAsync([FromRoute] int id)
    {
        await _employeeService.SoftDeleteEmployeeAsync(id);
        return Ok();
    }
    
    
}