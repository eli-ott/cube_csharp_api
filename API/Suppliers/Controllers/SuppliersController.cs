using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MonApi.API.Suppliers.Services;
using MonApi.API.Suppliers.DTOs;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using MonApi.API.Suppliers.Extensions;
using Microsoft.AspNetCore.Authorization;

namespace MonApi.API.Suppliers.Controllers;

[ApiController]
[Route("suppliers")]
[Authorize]

public class SuppliersController : ControllerBase
{
    private readonly ISuppliersService _suppliersService;

    public SuppliersController(ISuppliersService SuppliersService)
    {
        _suppliersService = SuppliersService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllSuppliers()
    {
        var Suppliers = await _suppliersService.GetAll();
        return Ok(Suppliers);
    }


    [HttpGet("{id}")]
    public async Task<IActionResult> FindSupplierById([FromRoute] int id)
    {
        var family = await _suppliersService.FindById(id);
        return Ok(family);
    }


    [HttpPost]
    public async Task<IActionResult> AddSupplier([FromBody] CreateSupplierDTO createSupplierDTO)
    {
        var isAdded = await _suppliersService.AddAsync(createSupplierDTO);
        return Ok(isAdded);
    }

    //[HttpPut("{id}")]
    //public async Task<IActionResult> UpdateFamily([FromRoute] int id, [FromBody] UpdateSupplierDTO updateSupplierDTO)
    //{
    //    var familyToUpdate = updateSupplierDTO.MapToSupplierModel();
    //    var isAdded = await _SuppliersService.UpdateAsync(id, familyToUpdate);
    //    return Ok(isAdded);
    //}

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteFamily([FromRoute] int id)
    {
        var isDeleted = await _suppliersService.SoftDeleteAsync(id);
        return Ok(isDeleted);
    }




}