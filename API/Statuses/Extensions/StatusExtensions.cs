using MonApi.API.Statuses.DTOs;
using MonApi.API.Statuses.Models;

namespace MonApi.API.Statuses.Extensions;

public static class StatusExtensions
{
    public static ReturnStatusDto ToReturnStatusDto(this Status status)
    {
        return new ReturnStatusDto
        {
            StatusId = status.StatusId,
            Name = status.Name,
            DeletionTime = status.DeletionTime
        };
    }
}