using MonApi.API.Statuses.DTOs;
using MonApi.API.Statuses.Filters;
using MonApi.API.Statuses.Models;
using MonApi.Shared.Pagination;
using MonApi.Shared.Repositories;

namespace MonApi.API.Statuses.Repositories;

public interface IStatusRepository : IBaseRepository<Status>
{
    Task<PagedResult<ReturnStatusDto>> GetAllStatusesAsync(StatusQueryParameters queryParameters, CancellationToken cancellationToken = default);
}