using Microsoft.EntityFrameworkCore;
using MonApi.API.Roles.DTOs;
using MonApi.API.Roles.Filters;
using MonApi.API.Roles.Models;
using MonApi.Shared.Data;
using MonApi.Shared.Pagination;
using MonApi.Shared.Repositories;

namespace MonApi.API.Roles.Repositories;

public class RoleRepository : BaseRepository<Role>, IRoleRepository
{
    public RoleRepository(StockManagementContext context) : base(context)
    {
    }

    public async Task<PagedResult<ReturnRoleDTO>> GetAll(RoleQueryParameters queryParameters, CancellationToken cancellationToken = default)
    {
        IQueryable<ReturnRoleDTO> query = from role in _context.Roles
                                          select new ReturnRoleDTO
                                          {
                                              RoleId = role.RoleId,
                                              Name = role.Name,
                                              DeletionTime = role.DeletionTime,
                                              CreationTime = role.CreationTime,
                                              UpdateTime = role.UpdateTime
                                          }; ;

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

        // Get the total count (before pagination)
        var totalCount = await query.CountAsync();

        // Get the items for the requested page
        var roles = await query
            .Skip((queryParameters.page - 1) * queryParameters.size)
            .Take(queryParameters.size)
            .ToListAsync();

        // Return the paginated result
        return new PagedResult<ReturnRoleDTO>
        {
            Items = roles,
            CurrentPage = queryParameters.page,
            PageSize = queryParameters.size,
            TotalCount = totalCount
        };
    }
}