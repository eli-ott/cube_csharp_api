using Microsoft.EntityFrameworkCore;
using MonApi.API.Employees.DTOs;
using MonApi.API.Employees.Filters;
using MonApi.API.Employees.Models;
using MonApi.Shared.Data;
using MonApi.Shared.Pagination;
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

        public async Task<PagedResult<ReturnEmployeeDto>> GetAll(EmployeeQueryParameters queryParameters, CancellationToken cancellationToken = default)
        {
            var query = _context.Employees
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
                });

            // Apply filters
            if (!string.IsNullOrWhiteSpace(queryParameters.email))
            {
                query = query.Where(c => c.Email.ToLower() == queryParameters.email.ToLower());
            }

            if (!string.IsNullOrWhiteSpace(queryParameters.first_name))
            {
                query = query.Where(c => c.FirstName.ToLower() == queryParameters.first_name.ToLower());
            }

            if (!string.IsNullOrWhiteSpace(queryParameters.last_name))
            {
                query = query.Where(c => c.LastName.ToLower() == queryParameters.last_name.ToLower());
            }

            if (!string.IsNullOrWhiteSpace(queryParameters.phone))
            {
                query = query.Where(c => c.Phone.ToLower() == queryParameters.phone.ToLower());
            }

            if (queryParameters.deleted == "only")
            {
                query = query.Where(f => f.DeletionTime != null);
            }
            else if (queryParameters.deleted == "all")
            {
            }
            else
                // Default to only returning undeleted items
            {
                query = query.Where(f => f.DeletionTime == null);
            }

            // Get total count before pagination
            var totalCount = await query.CountAsync(cancellationToken);

            // Apply pagination: note the multiplication uses PageSize!
            var employees = await query
                .Skip((queryParameters.page - 1) * queryParameters.size)
                .Take(queryParameters.size)
                .ToListAsync(cancellationToken);

            return new PagedResult<ReturnEmployeeDto>
            {
                Items = employees,
                CurrentPage = queryParameters.page,
                PageSize = queryParameters.size,
                TotalCount = totalCount
            };
        }

    }
