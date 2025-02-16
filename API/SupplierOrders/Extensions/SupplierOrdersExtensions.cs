using MonApi.API.Orders.DTOs;
using MonApi.API.SupplierOrders.DTOs;
using MonApi.API.SupplierOrders.Models;

namespace MonApi.API.SupplierOrders.Extensions;

public static class SupplierOrdersExtensions
{
    public static SupplierOrder MapToModel(this CreateSupplierOrderDto orderDto)
    {
        return new SupplierOrder
        {
            DeliveryDate = orderDto.DeliveryDate,
            StatusId = orderDto.StatusId,
            EmployeeId = orderDto.EmployeeId
        };
    }

    public static SupplierOrder MapToModel(this ReturnSupplierOrderDto orderDtoDto)
    {
        return new SupplierOrder
        {
            OrderId = orderDtoDto.OrderId,
            DeliveryDate = orderDtoDto.DeliveryDate,
            StatusId = orderDtoDto.Status.StatusId,
            EmployeeId = orderDtoDto.Employee.EmployeeId,
            CreationTime = orderDtoDto.CreationTime,
            UpdateTime = orderDtoDto.UpdateTime,
            DeletionTime = orderDtoDto.DeletionTime
        };
    }
}