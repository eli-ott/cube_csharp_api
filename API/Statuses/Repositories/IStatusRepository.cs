using MonApi.API.Statuses.DTOs;
using MonApi.API.Statuses.Models;
using MonApi.Shared.Repositories;

namespace MonApi.API.Statuses.Repositories;

public interface IStatusRepository : IBaseRepository<Status>
{
    Task<List<ReturnStatusDto>> GetAllStatusesAsync(CancellationToken cancellationToken = default);
}