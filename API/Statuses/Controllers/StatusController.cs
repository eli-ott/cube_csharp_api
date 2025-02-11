using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MonApi.API.Statuses.DTOs;
using MonApi.API.Statuses.Services;

namespace MonApi.API.Statuses.Controllers;

[ApiController]
[Authorize]
[Route("status")]
public class StatusController : ControllerBase
{
    private readonly IStatusService _statusService;

    public StatusController(IStatusService statusService)
    {
        _statusService = statusService;
    }

    [HttpGet]
    public async Task<ActionResult<List<ReturnStatusDto>>> GetAll()
    {
        return Ok(await _statusService.GetStatusesAsync());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ReturnStatusDto>> GetById(int id)
    {
        return Ok(await _statusService.GetStatusByIdAsync(id));
    }
}