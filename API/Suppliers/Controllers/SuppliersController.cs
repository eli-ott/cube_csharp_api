﻿using Microsoft.AspNetCore.Mvc;
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
        var supplier = await _suppliersService.FindById(id);
        return Ok(supplier);
    }


    [HttpPost]
    public async Task<IActionResult> AddSupplier([FromBody] CreateSupplierDTO createSupplierDTO)
    {
        var isAdded = await _suppliersService.AddAsync(createSupplierDTO);
        return Ok(isAdded);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateSupplier([FromRoute] int id, [FromBody] UpdateSupplierDTO updateSupplierDTO)
    {
        var isAdded = await _suppliersService.UpdateAsync(id, updateSupplierDTO);
        return Ok(isAdded);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSupplier([FromRoute] int id)
    {
        await _suppliersService.SoftDeleteAsync(id);
        return Ok();
    }




}