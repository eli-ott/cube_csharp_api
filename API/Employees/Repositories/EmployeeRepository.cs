using Microsoft.EntityFrameworkCore;
using MonApi.API.Employees.DTOs;
using MonApi.API.Employees.Filters;
using MonApi.API.Employees.Models;
using MonApi.API.Passwords.DTOs;
using MonApi.API.Roles.DTOs;
using MonApi.Shared.Data;
using MonApi.Shared.Pagination;
using MonApi.Shared.Repositories;

namespace MonApi.API.Employees.Repositories;

public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
{
    public EmployeeRepository(StockManagementContext context) : base(context)
    {
    }
    
    public async Task<ReturnEmployeeDto?> FindByEmailAsync(string email,
            CancellationToken cancellationToken = default)
        {
            return await (from employee in _context.Employees
                join password in _context.Passwords on employee.PasswordId equals password.PasswordId
                where employee.Email == email
                select new ReturnEmployeeDto
                {
                    EmployeeId = employee.EmployeeId,
                    FirstName = employee.FirstName,
                    LastName = employee.LastName,
                    Email = employee.Email,
                    Phone = employee.Phone,
                    CreationTime = employee.CreationTime,
                    UpdateTime = employee.UpdateTime,
                    DeletionTime = employee.DeletionTime,
                    Role = new ReturnRoleDTO()
                    {
                        RoleId = employee.Role.RoleId,
                        Name = employee.Role.Name
                    },
                    Password = new ReturnPasswordDto
                    {
                        PasswordId = password.PasswordId,
                        PasswordHash = password.PasswordHash,
                        PasswordSalt = password.PasswordSalt,
                        AttemptCount = password.AttemptCount,
                        UpdateTime = password.UpdateTime,
                        CreationTime = password.CreationTime
                    }
                }).FirstOrDefaultAsync(cancellationToken);
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
                Role = new ReturnRoleDTO
                {
                    RoleId = employee.Role.RoleId,
                    Name = employee.Role.Name
                },
                CreationTime = employee.CreationTime,
                UpdateTime = employee.UpdateTime,
                DeletionTime = employee.DeletionTime
            })
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<ReturnEmployeeDto?> FindAsyncWithPassword(int id, CancellationToken cancellationToken = default)
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
                Role = new ReturnRoleDTO
                {
                    RoleId = employee.Role.RoleId,
                    Name = employee.Role.Name
                },
                CreationTime = employee.CreationTime,
                UpdateTime = employee.UpdateTime,
                DeletionTime = employee.DeletionTime,
                Password = new ReturnPasswordDto()
                {
                    PasswordId = employee.PasswordId,
                    AttemptCount = employee.Password.AttemptCount,
                    PasswordHash = employee.Password.PasswordHash,
                    PasswordSalt = employee.Password.PasswordSalt,
                    CreationTime = employee.Password.CreationTime,
                    UpdateTime = employee.Password.UpdateTime
                }
            })
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<PagedResult<ReturnEmployeeDto>> GetAll(EmployeeQueryParameters queryParameters,
        CancellationToken cancellationToken = default)
    {
        var query = _context.Employees
            .Select(employee => new ReturnEmployeeDto()
            {
                EmployeeId = employee.EmployeeId,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Email = employee.Email,
                Phone = employee.Phone,
                Role = new ReturnRoleDTO
                {
                    RoleId = employee.Role.RoleId,
                    Name = employee.Role.Name
                },
                CreationTime = employee.CreationTime,
                UpdateTime = employee.UpdateTime,
                DeletionTime = employee.DeletionTime
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