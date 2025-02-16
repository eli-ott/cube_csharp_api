using MonApi.API.Employees.DTOs;
using MonApi.API.Employees.Models;
using MonApi.API.Suppliers.DTOs;

namespace MonApi.API.Employees.Services;

public interface IEmployeeService
{
    public Task<ReturnEmployeeDto> AddEmployeeAsync(CreateEmployeeDto employee);
    public Task<ReturnEmployeeDto> GetEmployeeByIdAsync(int id);
    
    public Task<List<ReturnEmployeeDto>> GetAllEmployeesAsync();
    
    public Task<ReturnEmployeeDto> UpdateEmployeeAsync(int id, UpdateEmployeeDto employee);
    
    public Task<ReturnEmployeeDto> SoftDeleteEmployeeAsync(int id);

}