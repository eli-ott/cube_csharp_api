using MonApi.API.Employees.DTOs;
using MonApi.API.Employees.Models;
using MonApi.Shared.Repositories;

namespace MonApi.API.Employees.Repositories;

public interface IEmployeeRepository : IBaseRepository<Employee>
{
    Task<ReturnEmployeeDto?> FindAsync(int id, CancellationToken cancellationToken = default);
    Task<List<ReturnEmployeeDto>> GetAll(CancellationToken cancellationToken = default);
}