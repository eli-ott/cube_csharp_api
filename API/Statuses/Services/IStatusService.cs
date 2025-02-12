using MonApi.API.Statuses.DTOs;
using MonApi.API.Statuses.Models;

namespace MonApi.API.Statuses.Services;

public interface IStatusService
{
    Task<List<ReturnStatusDto>> GetStatusesAsync();
    Task<ReturnStatusDto> GetStatusByIdAsync(int statusId);
}