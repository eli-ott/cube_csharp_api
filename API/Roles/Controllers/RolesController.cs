using Microsoft.AspNetCore.Mvc;
using MonApi.API.Roles.Services;
using MonApi.API.Roles.DTOs;
using Microsoft.AspNetCore.Authorization;
using MonApi.API.Roles.Extensions;

namespace MonApi.API.Roles.Controllers;

[ApiController]
[Route("roles")]
[Authorize]

public class RolesController : ControllerBase
{
    private readonly IRoleService _roleService;

    public RolesController(IRoleService roleService)
    {
        _roleService = roleService;
    }

    [HttpGet]

    public async Task<IActionResult> GetAllRoles()
    {
        var roles = await _roleService.GetAllRolesAsync();
        return Ok(roles);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> FindRoleById([FromRoute] int id)
    {
        var role = await _roleService.GetRoleByIdAsync(id);
        return Ok(role);
    }


    [HttpPost]
    public async Task<IActionResult> AddRole([FromBody] CreateRoleDTO createRoleDto)
    {
        var roleToAdd = createRoleDto.MapToRoleModel();
        var isAdded = await _roleService.AddRoleAsync(roleToAdd);
        return Ok(isAdded);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateRole([FromRoute] int id, [FromBody] UpdateRoleDTO updateRoleDto)
    {
        var roleToUpdate = updateRoleDto.MapToRoleModel();
        var isAdded = await _roleService.UpdateRoleAsync(id, roleToUpdate);
        return Ok(isAdded);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRole([FromRoute] int id)
    {
        var isDeleted = await _roleService.SoftDeleteRoleAsync(id);
        return Ok(isDeleted);
    }



}