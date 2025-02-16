using MonApi.API.Discounts.DTOs;
using MonApi.API.Discounts.Models;

namespace MonApi.API.Discounts.Services;

public interface IDiscountService
{
    Task<ReturnDiscountDto> Create(CreateDiscountDto discountDto);
    Task<ReturnDiscountDto> Update(int discountId, UpdateDiscountDto discountDto);
    Task<ReturnDiscountDto> Delete(int discountId);
}