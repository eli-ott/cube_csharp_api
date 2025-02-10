using Microsoft.EntityFrameworkCore;
using MonApi.API.Statuses.DTOs;
using MonApi.API.Statuses.Models;
using MonApi.Shared.Data;
using MonApi.Shared.Repositories;

namespace MonApi.API.Statuses.Repositories;

public class StatusRepository : BaseRepository<Status>, IStatusRepository
{
    public StatusRepository(StockManagementContext context) : base(context)
    {
    }

    public async Task<List<ReturnStatusDto>> GetAllStatusesAsync(CancellationToken cancellationToken = default)
    {
        return await (from status in _context.Statuses
            select new ReturnStatusDto
            {
                StatusId = status.StatusId,
                Name = status.Name,
                DeletionTime = status.DeletionTime
            }).ToListAsync(cancellationToken);
    }
}