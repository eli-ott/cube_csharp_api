using MonApi.API.Discounts.DTOs;
using MonApi.API.Discounts.Models;

namespace MonApi.API.Discounts.Extensions;

public static class DiscountsExtensions
{
    public static Discount MapToDiscountModel(this CreateDiscountDto discountDto)
    {
        return new Discount
        {
            Name = discountDto.Name,
            Value = discountDto.Value,
            StartDate = discountDto.StartDate,
            EndDate = discountDto.EndDate,
            ProductId = discountDto.ProductId
        };
    }

    public static Discount MapToDiscountModel(this ReturnDiscountDto discountDto)
    {
        return new Discount
        {
            DiscountId = discountDto.DiscountId,
            Name = discountDto.Name,
            Value = discountDto.Value,
            StartDate = discountDto.StartDate,
            EndDate = discountDto.EndDate,
            ProductId = discountDto.ProductId
        };
    }

    public static ReturnDiscountDto MapToReturnDiscountModel(this Discount discount)
    {
        return new ReturnDiscountDto
        {
            DiscountId = discount.DiscountId,
            Name = discount.Name,
            StartDate = discount.StartDate,
            EndDate = discount.EndDate,
            ProductId = discount.ProductId,
            Value = discount.Value,
            CreationTime = discount.CreationTime,
            UpdateTime = discount.UpdateTime
        };
    }
}