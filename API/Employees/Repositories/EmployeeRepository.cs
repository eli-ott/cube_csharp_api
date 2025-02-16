using Microsoft.EntityFrameworkCore;
using MonApi.API.Employees.DTOs;
using MonApi.API.Employees.Models;
using MonApi.Shared.Data;
using MonApi.Shared.Repositories;

namespace MonApi.API.Employees.Repositories;

public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
{
    public EmployeeRepository(StockManagementContext context) : base(context)
    {
    }
      public async Task<ReturnEmployeeDto?> FindAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _context.Employees
                .Where(e => e.EmployeeId == id)
                .Select(employee => new ReturnEmployeeDto()
                {
                    EmployeeId = employee.EmployeeId,
                    FirstName = employee.FirstName,
                    LastName = employee.LastName,
                    Email = employee.Email,
                    Phone = employee.Phone,
                    RoleId = employee.RoleId,
                    CreationTime = employee.CreationTime,
                    UpdateTime = employee.UpdateTime,
                    DeletionTime = employee.DeletionTime,
                    PasswordId = employee.PasswordId
                })
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<List<ReturnEmployeeDto>> GetAll(CancellationToken cancellationToken = default)
        {
            return await _context.Employees
                .Select(employee => new ReturnEmployeeDto()
                {
                    EmployeeId = employee.EmployeeId,
                    FirstName = employee.FirstName,
                    LastName = employee.LastName,
                    Email = employee.Email,
                    Phone = employee.Phone,
                    RoleId = employee.RoleId,
                    CreationTime = employee.CreationTime,
                    UpdateTime = employee.UpdateTime,
                    DeletionTime = employee.DeletionTime,
                    PasswordId = employee.PasswordId
                }).ToListAsync(cancellationToken);
                
        }

    }
