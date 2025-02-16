using MonApi.API.SupplierOrderLines.DTOs;
using MonApi.API.SupplierOrderLines.Models;

namespace MonApi.API.SupplierOrderLines.Extensions;

public static class SupplierOrderLinesExtensions
{
    public static SupplierOrderLine MapToModel(this CreateSupplierOrderLineDto orderLineDto, int orderId)
    {
        return new SupplierOrderLine
        {
            OrderId = orderId,
            ProductId = orderLineDto.ProductId,
            Quantity = orderLineDto.Quantity,
            UnitPrice = orderLineDto.UnitPrice
        };
    }

    public static SupplierOrderLine MapToModel(this ReturnSupplierOrderLineDto orderLineDto, int orderId)
    {
        return new SupplierOrderLine
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