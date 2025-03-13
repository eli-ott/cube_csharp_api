using MonApi.API.Employees.DTOs;
using MonApi.API.Employees.Filters;
using MonApi.API.Employees.Models;
using MonApi.API.Suppliers.DTOs;
using MonApi.Shared.Pagination;

namespace MonApi.API.Employees.Services;

public interface IEmployeeService
{
    
    Task<string> LogEmployee(EmployeeLoginDto loginDto);
    public Task<ReturnEmployeeDto> AddEmployeeAsync(CreateEmployeeDto employee);
    public Task<ReturnEmployeeDto> GetEmployeeByIdAsync(int id);
    
    public Task<PagedResult<ReturnEmployeeDto>> GetAllEmployeesAsync(EmployeeQueryParameters queryParameters);
    
    public Task<ReturnEmployeeDto> UpdateEmployeeAsync(int id, UpdateEmployeeDto employee);
    
    public Task<ReturnEmployeeDto> SoftDeleteEmployeeAsync(int id);

}