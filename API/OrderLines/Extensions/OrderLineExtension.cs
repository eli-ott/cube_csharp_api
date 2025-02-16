using MonApi.API.OrderLines.DTOs;
using MonApi.API.OrderLines.Models;

namespace MonApi.API.OrderLines.Extensions;

public static class OrderLineExtension
{
    public static OrderLine MapToModel(this CreateOrderLineDto orderLineDto, int orderId)
    {
        return new OrderLine
        {
            OrderId = orderId,
            ProductId = orderLineDto.ProductId,
            Quantity = orderLineDto.Quantity,
            UnitPrice = orderLineDto.UnitPrice
        };
    }

    public static OrderLine MapToModel(this ReturnOrderLineDto orderLineDto, int orderId)
    {
        return new OrderLine
        {
            OrderId = orderId,
            ProductId = orderLineDto.Product.ProductId,
            Quantity = orderLineDto.Quantity,
            UnitPrice = orderLineDto.UnitPrice,
            CreationTime = orderLineDto.CreationTime,
            UpdateTime = orderLineDto.UpdateTime,
            DeletionTime = orderLineDto.DeletionTime
        };
    }
}