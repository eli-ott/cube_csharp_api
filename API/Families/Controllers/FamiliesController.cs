using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MonApi.API.Families.Services;
using MonApi.API.Families.DTOs;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using MonApi.API.Families.Extensions;
using Microsoft.AspNetCore.Authorization;
using MonApi.API.Families.Filters;

namespace MonApi.API.Families.Controllers;

[ApiController]
[Route("families")]
[Authorize]

public class FamiliesController : ControllerBase
{
    private readonly IFamiliesService _familiesService;

    public FamiliesController(IFamiliesService familiesService)
    {
        _familiesService = familiesService;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetAllFamilies([FromQuery] FamilyQueryParameters queryParameters)
    {
        var families = await _familiesService.GetAll(queryParameters);
        return Ok(families);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> FindFamilyById([FromRoute] int id)
    {
        var family = await _familiesService.FindById(id);
        return Ok(family);
    }

    [Authorize(Roles = "Employee")]
    [HttpPost]
    public async Task<IActionResult> AddFamily([FromBody] CreateFamilyDTO createFamilyDTO)
    {
        var familyToAdd = createFamilyDTO.MapToFamilyModel();
        var isAdded = await _familiesService.AddAsync(familyToAdd);
        return Ok(isAdded);
    }

    [Authorize(Roles = "Employee")]
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateFamily([FromRoute] int id, [FromBody] UpdateFamilyDTO updateFamilyDTO)
    {
        var familyToUpdate = updateFamilyDTO.MapToFamilyModel();
       
        return Ok(await _familiesService.UpdateAsync(id, familyToUpdate));
    }

    [Authorize(Roles = "Employee")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteFamily([FromRoute] int id)
    {
        var isDeleted = await _familiesService.SoftDeleteAsync(id);
        return Ok(isDeleted);
    }




}