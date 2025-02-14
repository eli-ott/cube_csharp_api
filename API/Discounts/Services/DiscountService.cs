using System.Text.Json;
using MonApi.API.Discounts.DTOs;
using MonApi.API.Discounts.Extensions;
using MonApi.API.Discounts.Models;
using MonApi.API.Discounts.Repositories;
using MonApi.API.Products.Repositories;
using MonApi.Shared.Data;
using MonApi.Shared.Exceptions;

namespace MonApi.API.Discounts.Services;

public class DiscountService : IDiscountService
{
    private readonly IDiscountRepository _discountRepository;
    private readonly IProductsRepository _productsRepository;

    public DiscountService(IDiscountRepository discountRepository, IProductsRepository productsRepository)
    {
        _discountRepository = discountRepository;
        _productsRepository = productsRepository;
    }

    public async Task<ReturnDiscountDto> Create(CreateDiscountDto discountDto)
    {
        var foundProduct = await _productsRepository.FindAsync(discountDto.ProductId)
                           ?? throw new NullReferenceException("Product not found");
        if (foundProduct.DeletionTime != null)
            throw new SoftDeletedException("Product is already deleted");

        var discountAlreadyExists = await _discountRepository.AnyAsync(x => x.ProductId == discountDto.ProductId);
        if (discountAlreadyExists)
            throw new BadHttpRequestException("There can only be one discount on a product");

        if (discountDto.Value < 0 || discountDto.Value > 100)
            throw new BadHttpRequestException("The discount pourcentage should be between 0 and 100");
        if (discountDto.EndDate < DateTime.UtcNow)
            throw new BadHttpRequestException("The discount end date should be at least today");

        var addedDiscount = await _discountRepository.AddAsync(discountDto.MapToDiscountModel());

        return addedDiscount.MapToReturnDiscountModel();
    }

    public async Task<ReturnDiscountDto> Update(int discountId, UpdateDiscountDto discountDto)
    {
        var foundDiscount = await _discountRepository.FindAsync(discountId)
                            ?? throw new NullReferenceException("Discount not found");

        if (discountDto.Value < 0 || discountDto.Value > 100)
            throw new BadHttpRequestException("The discount pourcentage should be between 0 and 100");
        if (discountDto.EndDate < DateTime.UtcNow)
            throw new BadHttpRequestException("The discount end date should be at least today");

        foundDiscount.Name = discountDto.Name;
        foundDiscount.Value = discountDto.Value;
        foundDiscount.StartDate = discountDto.StartDate;
        foundDiscount.EndDate = discountDto.EndDate;

        await _discountRepository.UpdateAsync(foundDiscount.MapToDiscountModel());

        return foundDiscount;
    }

    public async Task<ReturnDiscountDto> Delete(int discountId)
    {
        var foundDiscount = await _discountRepository.FindAsync(discountId)
                            ?? throw new NullReferenceException("Discount not found");

        await _discountRepository.DeleteAsync(foundDiscount.MapToDiscountModel());

        return foundDiscount;
    }
}