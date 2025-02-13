using Microsoft.EntityFrameworkCore;
using MonApi.API.Statuses.DTOs;
using MonApi.API.Statuses.Filters;
using MonApi.API.Statuses.Models;
using MonApi.Shared.Data;
using MonApi.Shared.Pagination;
using MonApi.Shared.Repositories;

namespace MonApi.API.Statuses.Repositories;

public class StatusRepository : BaseRepository<Status>, IStatusRepository
{
    public StatusRepository(StockManagementContext context) : base(context)
    {
    }

    public async Task<PagedResult<ReturnStatusDto>> GetAllStatusesAsync(StatusQueryParameters queryParameters, CancellationToken cancellationToken = default)
    {
        IQueryable<ReturnStatusDto> query = (from status in _context.Statuses
                                             select new ReturnStatusDto
                                             {
                                                 StatusId = status.StatusId,
                                                 Name = status.Name,
                                                 DeletionTime = status.DeletionTime
                                             });

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
        var statuses = await query
            .Skip((queryParameters.page - 1) * queryParameters.size)
            .Take(queryParameters.size)
            .ToListAsync();

        // Return the paginated result
        return new PagedResult<ReturnStatusDto>
        {
            Items = statuses,
            CurrentPage = queryParameters.page,
            PageSize = queryParameters.size,
            TotalCount = totalCount
        };
    }
}