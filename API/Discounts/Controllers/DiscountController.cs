using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using MonApi.API.Discounts.DTOs;
using MonApi.API.Discounts.Models;
using MonApi.API.Discounts.Services;

namespace MonApi.API.Discounts.Controllers;

[ApiController]
[Route("discounts")]
[Authorize(Roles = "Employee")]
public class DiscountController : ControllerBase
{
    private readonly IDiscountService _discountService;

    public DiscountController(IDiscountService discountService)
    {
        _discountService = discountService;
    }

    [HttpPost]
    public async Task<ActionResult<Discount>> CreateDiscount(CreateDiscountDto discountDto)
    {
        return Ok(await _discountService.Create(discountDto));
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Discount>> UpdateDiscount(int id, UpdateDiscountDto discountDto)
    {
        return Ok(await _discountService.Update(id, discountDto));
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<Discount>> DeleteDiscount(int id)
    {
        return Ok(await _discountService.Delete(id));
    }
}