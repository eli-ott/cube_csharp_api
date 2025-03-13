using MonApi.API.Employees.DTOs;
using MonApi.API.Employees.Filters;
using MonApi.API.Employees.Models;
using MonApi.Shared.Pagination;
using MonApi.Shared.Repositories;

namespace MonApi.API.Employees.Repositories;

public interface IEmployeeRepository : IBaseRepository<Employee>
{
    Task<ReturnEmployeeDto?> FindByEmailAsync(string email, CancellationToken cancellationToken = default);
    Task<ReturnEmployeeDto?> FindAsync(int id, CancellationToken cancellationToken = default);
    Task<PagedResult<ReturnEmployeeDto>> GetAll(EmployeeQueryParameters queryParameters, CancellationToken cancellationToken = default);
    Task<ReturnEmployeeDto?> FindAsyncWithPassword(int id, CancellationToken cancellationToken = default);
}