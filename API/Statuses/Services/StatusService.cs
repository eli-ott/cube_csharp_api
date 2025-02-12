using MonApi.API.Statuses.DTOs;
using MonApi.API.Statuses.Extensions;
using MonApi.API.Statuses.Models;
using MonApi.API.Statuses.Repositories;

namespace MonApi.API.Statuses.Services;

public class StatusService : IStatusService
{
    private readonly IStatusRepository _statusRepository;

    public StatusService(IStatusRepository statusRepository)
    {
        _statusRepository = statusRepository;
    }

    public async Task<List<ReturnStatusDto>> GetStatusesAsync()
    {
        return await _statusRepository.GetAllStatusesAsync();
    }

    public async Task<ReturnStatusDto> GetStatusByIdAsync(int statusId)
    {
        var status = await _statusRepository.FindAsync(statusId)
            ?? throw new NullReferenceException("Le status n'éxiste pas");
        
        return status.ToReturnStatusDto();
    }
}