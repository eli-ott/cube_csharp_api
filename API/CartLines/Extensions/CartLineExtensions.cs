using MonApi.API.CartLines.DTOs;
using MonApi.API.CartLines.Models;

namespace MonApi.API.CartLines.Extensions;

public static class CartLineExtensions
{
    public static CartLine MapToModel(this CreateCartLineDto cartLineDto, int cartId)
    {
        return new CartLine
        {
            ProductId = cartLineDto.ProductId,
            Quantity = cartLineDto.Quantity,
            CartId = cartId
        };
    }

    public static CartLine MapToModel(this ReturnCartLineDto cartLineDto, int cartId)
    {
        return new CartLine
        {
            ProductId = cartLineDto.Product.ProductId,
            CartId = cartId,
            Quantity = cartLineDto.Quantity,
            IsSetAside = cartLineDto.IsSetAside,
            UpdateTime = cartLineDto.UpdateTime,
            CreationTime = cartLineDto.CreationTime
        };
    }
}