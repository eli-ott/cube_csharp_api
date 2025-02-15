using MonApi.API.CartLines.DTOs;
using MonApi.API.CartLines.Extensions;
using MonApi.API.CartLines.Repositories;
using MonApi.API.Carts.Repositories;

namespace MonApi.API.CartLines.Services;

public class CartLineService : ICartLineService
{
    private readonly ICartLineRepository _cartLineRepository;
    private readonly ICartRepository _cartRepository;

    public CartLineService(ICartLineRepository cartLineRepository, ICartRepository cartRepository)
    {
        _cartLineRepository = cartLineRepository;
        _cartRepository = cartRepository;
    }

    public async Task<ReturnCartLineDto> UpdateCartLine(UpdateCartLineDto updateCartLineDto)
    {
        var foundCart = await _cartRepository.FindAsync(updateCartLineDto.CartId)
                        ?? throw new NullReferenceException("Can't find the cart");

        var foundCartLine =
            await _cartLineRepository.GetCartLine(updateCartLineDto.ProductId, updateCartLineDto.CartId)
            ?? throw new NullReferenceException("Can't find cart line");

        foundCartLine.Quantity = updateCartLineDto.Quantity;

        var mappedCartLine = foundCartLine.MapToModel(foundCart.CartId);
        await _cartLineRepository.UpdateAsync(mappedCartLine);

        return foundCartLine;
    }

    public async Task<ReturnCartLineDto> DeleteCartLine(DeleteCartLineDto cartLineDto)
    {
        var foundCart = await _cartRepository.FindAsync(cartLineDto.CartId)
                        ?? throw new NullReferenceException("Can't find the cart");

        var foundCartLine =
            await _cartLineRepository.GetCartLine(cartLineDto.ProductId, cartLineDto.CartId)
            ?? throw new NullReferenceException("Can't find cart line");

        var mappedCartLine = foundCartLine.MapToModel(foundCart.CartId);
        await _cartLineRepository.DeleteAsync(mappedCartLine);

        return foundCartLine;
    }

    public async Task<ReturnCartLineDto> ToggleIsSetAside(UpdateSetAsideDto updateSetAsideDto)
    {
        var foundCart = await _cartRepository.FindAsync(updateSetAsideDto.CartId)
                        ?? throw new NullReferenceException("Can't find the cart");

        var foundCartLine =
            await _cartLineRepository.GetCartLine(updateSetAsideDto.ProductId, updateSetAsideDto.CartId)
            ?? throw new NullReferenceException("Can't find cart line");
        
        foundCartLine.IsSetAside = !foundCartLine.IsSetAside;
        
        var mappedCartLine = foundCartLine.MapToModel(foundCart.CartId);
        await _cartLineRepository.UpdateAsync(mappedCartLine);
        
        return foundCartLine;
    }
}