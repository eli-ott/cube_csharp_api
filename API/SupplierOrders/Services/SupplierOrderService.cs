using MonApi.API.Employees.Repositories;
using MonApi.API.Statuses.Repositories;
using MonApi.API.SupplierOrderLines.Extensions;
using MonApi.API.SupplierOrderLines.Repositories;
using MonApi.API.SupplierOrders.DTOs;
using MonApi.API.SupplierOrders.Extensions;
using MonApi.API.SupplierOrders.Filters;
using MonApi.API.SupplierOrders.Repositories;
using MonApi.Shared.Exceptions;
using MonApi.Shared.Pagination;

namespace MonApi.API.SupplierOrders.Services;

public class SupplierOrderService : ISupplierOrdersService
{
    private readonly ISupplierOrdersRepository _ordersRepository;
    private readonly ISupplierOrderLinesRepository _orderLineRepository;
    private readonly IStatusRepository _statusRepository;
    private readonly IEmployeeRepository _employeeRepository;

    public SupplierOrderService(ISupplierOrdersRepository orderRepository, ISupplierOrderLinesRepository orderLineRepository,
        IStatusRepository statusRepository, IEmployeeRepository employeeRepository)
    {
        _ordersRepository = orderRepository;
        _orderLineRepository = orderLineRepository;
        _statusRepository = statusRepository;
        _employeeRepository = employeeRepository;
    }

    public async Task<PagedResult<ReturnSupplierOrderDto>> GetAllOrders(SupplierOrdersQueryParameters queryParameters)
    {
        return await _ordersRepository.GetAll(queryParameters);
    }

    public async Task<ReturnSupplierOrderDto?> GetOrderById(int orderId)
    {
        return await _ordersRepository.FindAsync(orderId);
    }

    public async Task<ReturnSupplierOrderDto> CreateOrderAsync(CreateSupplierOrderDto createOrderDto)
    {
        if (createOrderDto.DeliveryDate < DateTime.Now)
            throw new BadHttpRequestException("Delivery date should be before today");

        var statusCheck = await _statusRepository.AnyAsync(x => x.StatusId == createOrderDto.StatusId);
        if (!statusCheck)
            throw new NullReferenceException("Can't find the status");

        var employeeCheck = await _employeeRepository.AnyAsync(x => x.EmployeeId == createOrderDto.EmployeeId);
        if (!employeeCheck)
            throw new NullReferenceException("Can't find the employee");

        var addedOrder = await _ordersRepository.AddAsync(createOrderDto.MapToModel());

        var orderLinesToAdd = createOrderDto.OrderLines;
        var mappedLines = orderLinesToAdd.Select(x => x.MapToModel(addedOrder.OrderId)).ToList();

        await _orderLineRepository.AddRangeAsync(mappedLines);

        var newOrderDetails = await _ordersRepository.FindAsync(addedOrder.OrderId);

        return newOrderDetails!;
    }

    public async Task<ReturnSupplierOrderDto> UpdateOrderAsync(int orderId, UpdateSupplierOrderDto updateOrderDto)
    {
        if (updateOrderDto.DeliveryDate < DateTime.Now)
            throw new BadHttpRequestException("Delivery date should be before today");

        var foundOrder = await _ordersRepository.FindAsync(orderId)
                         ?? throw new NullReferenceException("Can't find the order");
        if (foundOrder.DeletionTime != null)
            throw new SoftDeletedException("Order is already deleted");

        foundOrder.DeliveryDate = updateOrderDto.DeliveryDate;
        foundOrder.Status.StatusId = updateOrderDto.StatusId;

        await _ordersRepository.UpdateAsync(foundOrder.MapToModel());

        return foundOrder;
    }

    public async Task<ReturnSupplierOrderDto> DeleteOrderAsync(int orderId)
    {
        var foundOrder = await _ordersRepository.FindAsync(orderId)
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

        await _ordersRepository.SoftDeleteAsync(foundOrder.MapToModel());

        return foundOrder;
    }
}