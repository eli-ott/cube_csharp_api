using System.Text.Json;
using MonApi.API.Customers.Repositories;
using MonApi.API.OrderLines.Extensions;
using MonApi.API.OrderLines.Repositories;
using MonApi.API.Orders.DTOs;
using MonApi.API.Orders.Extensions;
using MonApi.API.Orders.Filters;
using MonApi.API.Orders.Repositories;
using MonApi.API.Statuses.Repositories;
using MonApi.API.SupplierOrders.Models;
using MonApi.API.SupplierOrders.Repositories;
using MonApi.Shared.Exceptions;
using MonApi.Shared.Pagination;

namespace MonApi.API.Orders.Services;

public class OrdersService : IOrdersService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IOrderLineRepository _orderLineRepository;
    private readonly IStatusRepository _statusRepository;
    private readonly ICustomersRepository _customersRepository;
    private readonly ISupplierOrdersRepository _supplierOrdersRepository;

    public OrdersService(IOrderRepository orderRepository, IOrderLineRepository orderLineRepository,
        IStatusRepository statusRepository, ICustomersRepository customersRepository, ISupplierOrdersRepository supplierOrdersRepository)
    {
        _orderRepository = orderRepository;
        _orderLineRepository = orderLineRepository;
        _statusRepository = statusRepository;
        _customersRepository = customersRepository;
        _supplierOrdersRepository = supplierOrdersRepository;
    }

    public async Task<PagedResult<ReturnOrderDto>> GetAllOrders(OrderQueryParameters queryParameters)
    {
        return await _orderRepository.GetAll(queryParameters);
    }

    public async Task<ReturnOrderDto?> GetOrderById(int orderId)
    {
        return await _orderRepository.FindAsync(orderId);
    }

    public async Task<ReturnOrderDto> CreateOrderAsync(CreateOrderDto createOrderDto)
    {
        if (createOrderDto.DeliveryDate < DateTime.Now)
            throw new BadHttpRequestException("Delivery date should be before today");

        var statusCheck = await _statusRepository.AnyAsync(x => x.StatusId == createOrderDto.StatusId);
        if (!statusCheck)
            throw new NullReferenceException("Can't find the status");

        var customerCheck = await _customersRepository.AnyAsync(x => x.CustomerId == createOrderDto.CustomerId);
        if (!customerCheck)
            throw new NullReferenceException("Can't find the customer");

        var addedOrder = await _orderRepository.AddAsync(createOrderDto.MapToModel());

        var orderLinesToAdd = createOrderDto.OrderLines;
        var mappedLines = orderLinesToAdd.Select(x => x.MapToModel(addedOrder.OrderId)).ToList();
        
        mappedLines.ForEach((line) =>
        {
            if (line.Product.AutoRestock && line.Product.Quantity <= line.Product.AutoRestockTreshold)
            {
                _supplierOrdersRepository.AddAsync(new SupplierOrder
                {
                    EmployeeId = 999999,
                    StatusId = 1,
                    DeliveryDate = new DateTime()
                });
            }
        });

        await _orderLineRepository.AddRangeAsync(mappedLines);

        var newOrderDetails = await _orderRepository.FindAsync(addedOrder.OrderId);

        return newOrderDetails!;
    }

    public async Task<ReturnOrderDto> UpdateOrderAsync(int orderId, UpdateOrderDto updateOrderDto)
    {
        if (updateOrderDto.DeliveryDate < DateTime.Now)
            throw new BadHttpRequestException("Delivery date should be before today");

        var foundOrder = await _orderRepository.FindAsync(orderId)
                         ?? throw new NullReferenceException("Can't find the order");
        if (foundOrder.DeletionTime != null)
            throw new SoftDeletedException("Order is already deleted");

        foundOrder.DeliveryDate = updateOrderDto.DeliveryDate;
        foundOrder.Status.StatusId = updateOrderDto.StatusId;

        await _orderRepository.UpdateAsync(foundOrder.MapToModel());

        return foundOrder;
    }

    public async Task<ReturnOrderDto> DeleteOrderAsync(int orderId)
    {
        var foundOrder = await _orderRepository.FindAsync(orderId)
                         ?? throw new NullReferenceException("Can't find the order");
        if (foundOrder.DeletionTime != null)
            throw new SoftDeletedException("Order already deleted");

        foundOrder.DeletionTime = DateTime.UtcNow;
        
        var linesToDelete = foundOrder.Lines!;
        var mappedLines = linesToDelete.Select(line =>
        {
            line.DeletionTime = DateTime.UtcNow;
            return line.MapToModel(foundOrder.OrderId);
        }).ToList();
        await _orderLineRepository.UpdateRange(mappedLines);

        await _orderRepository.SoftDeleteAsync(foundOrder.MapToModel());

        return foundOrder;
    }
}