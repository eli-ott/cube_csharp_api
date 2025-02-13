using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MonApi.API.Statuses.DTOs;
using MonApi.API.Statuses.Filters;
using MonApi.API.Statuses.Services;
using MonApi.Shared.Pagination;

namespace MonApi.API.Statuses.Controllers;

[ApiController]
[Authorize]
[Route("statuses")]
public class StatusController : ControllerBase
{
    private readonly IStatusService _statusService;

    public StatusController(IStatusService statusService)
    {
        _statusService = statusService;
    }

    [HttpGet]
    public async Task<ActionResult<PagedResult<ReturnStatusDto>>> GetAll([FromQuery] StatusQueryParameters queryParameters)
    {
        return Ok(await _statusService.GetStatusesAsync(queryParameters));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ReturnStatusDto>> GetById(int id)
    {
        return Ok(await _statusService.GetStatusByIdAsync(id));
    }
}