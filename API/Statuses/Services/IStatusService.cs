using MonApi.API.Statuses.DTOs;
using MonApi.API.Statuses.Filters;
using MonApi.API.Statuses.Models;
using MonApi.Shared.Pagination;

namespace MonApi.API.Statuses.Services;

public interface IStatusService
{
    Task<PagedResult<ReturnStatusDto>> GetStatusesAsync(StatusQueryParameters queryParameters);
    Task<ReturnStatusDto> GetStatusByIdAsync(int statusId);
}