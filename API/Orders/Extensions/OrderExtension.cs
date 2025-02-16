using MonApi.API.Orders.DTOs;
using MonApi.API.Orders.Models;

namespace MonApi.API.Orders.Extensions;

public static class OrderExtension
{
    public static Order MapToModel(this CreateOrderDto orderDto)
    {
        return new Order
        {
            DeliveryDate = orderDto.DeliveryDate,
            StatusId = orderDto.StatusId,
            CustomerId = orderDto.CustomerId
        };
    }

    public static Order MapToModel(this ReturnOrderDto orderDto)
    {
        return new Order
        {
            OrderId = orderDto.OrderId,
            DeliveryDate = orderDto.DeliveryDate,
            StatusId = orderDto.Status.StatusId,
            CustomerId = orderDto.Customer.CustomerId,
            CreationTime = orderDto.CreationTime,
            UpdateTime = orderDto.UpdateTime,
            DeletionTime = orderDto.DeletionTime
        };
    }
}