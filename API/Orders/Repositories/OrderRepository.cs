using Microsoft.EntityFrameworkCore;
using MonApi.API.Addresses.DTOs;
using MonApi.API.Customers.DTOs;
using MonApi.API.Discounts.DTOs;
using MonApi.API.Families.Models;
using MonApi.API.Images.DTOs;
using MonApi.API.OrderLines.DTOs;
using MonApi.API.OrderLines.Models;
using MonApi.API.Orders.DTOs;
using MonApi.API.Orders.Filters;
using MonApi.API.Orders.Models;
using MonApi.API.Passwords.DTOs;
using MonApi.API.Products.DTOs;
using MonApi.API.Statuses.DTOs;
using MonApi.API.Suppliers.DTOs;
using MonApi.Shared.Data;
using MonApi.Shared.Pagination;
using MonApi.Shared.Repositories;

namespace MonApi.API.Orders.Repositories;

public class OrderRepository : BaseRepository<Order>, IOrderRepository
{
    private readonly string _apiPath;

    public OrderRepository(StockManagementContext context) : base(context)
    {
        var apiUrl = Environment.GetEnvironmentVariable("URL_API")
                     ?? throw new NullReferenceException("URL_API is null");
        var uploadDir = Environment.GetEnvironmentVariable("UPLOAD_DIR")
                        ?? throw new NullReferenceException("UPLOAD_DIR is null");

        _apiPath = apiUrl + uploadDir;
    }

    public async Task<ReturnOrderDto?> FindAsync(int orderId, CancellationToken cancellationToken = default)
    {
        return await (from order in _context.Orders
            join status in _context.Statuses on order.StatusId equals status.StatusId
            where order.OrderId == orderId
            select new ReturnOrderDto
            {
                OrderId = order.OrderId,
                DeliveryDate = order.DeliveryDate,
                DeletionTime = order.DeletionTime,
                CreationTime = order.CreationTime,
                UpdateTime = order.CreationTime,
                Customer = (from customer in _context.Customers
                    join address in _context.Addresses on customer.AddressId equals address.AddressId
                    where customer.CustomerId == order.CustomerId
                    select new ReturnCustomerDto
                    {
                        CustomerId = customer.CustomerId,
                        FirstName = customer.FirstName,
                        LastName = customer.LastName,
                        Email = customer.Email,
                        Phone = customer.Phone,
                        Active = customer.Active,
                        CreationTime = customer.CreationTime,
                        UpdateTime = customer.UpdateTime,
                        DeletionTime = customer.DeletionTime,
                        ValidationId = customer.ValidationId,
                        Address = new ReturnAddressDto
                        {
                            AddressId = address.AddressId,
                            AddressLine = address.AddressLine,
                            City = address.City,
                            Country = address.Country,
                            ZipCode = address.ZipCode,
                            Complement = address.Complement,
                            CreationTime = address.CreationTime,
                            UpdateTime = address.UpdateTime,
                            DeletionTime = address.DeletionTime
                        }
                    }).FirstOrDefault(),
                Status = new ReturnStatusDto
                {
                    StatusId = status.StatusId,
                    Name = status.Name,
                    DeletionTime = status.DeletionTime
                },
                Lines = (from orderLine in _context.OrderLines
                    join product in _context.Products on orderLine.ProductId equals product.ProductId
                    where orderLine.OrderId == order.OrderId
                    select new ReturnOrderLineDto
                    {
                        Quantity = orderLine.Quantity,
                        UnitPrice = orderLine.UnitPrice,
                        UpdateTime = orderLine.UpdateTime,
                        CreationTime = orderLine.CreationTime,
                        DeletionTime = orderLine.DeletionTime,
                        Product = new ReturnProductDTO
                        {
                            ProductId = product.ProductId,
                            Name = product.Name,
                            Cuvee = product.Cuvee,
                            Year = product.Year,
                            ProducerName = product.ProducerName,
                            IsBio = product.IsBio,
                            UnitPrice = product.UnitPrice,
                            BoxPrice = product.BoxPrice,
                            Quantity = product.Quantity,
                            AutoRestock = product.AutoRestock,
                            AutoRestockTreshold = product.AutoRestockTreshold,
                            DeletionTime = product.DeletionTime,
                            UpdateTime = product.UpdateTime,
                            CreationTime = product.CreationTime,
                            Family = new Family
                            {
                                FamilyId = product.FamilyId,
                                Name = product.Family.Name
                            },
                            Supplier = new ReturnSupplierDTO
                            {
                                SupplierId = product.SupplierId,
                                LastName = product.Supplier.LastName,
                                FirstName = product.Supplier.FirstName,
                                Contact = product.Supplier.Contact,
                                Email = product.Supplier.Email,
                                Phone = product.Supplier.Phone,
                                Siret = product.Supplier.Siret,
                                Address = new ReturnAddressDto
                                {
                                    AddressId = product.Supplier.Address.AddressId,
                                    AddressLine = product.Supplier.Address.AddressLine,
                                    City = product.Supplier.Address.City,
                                    ZipCode = product.Supplier.Address.ZipCode,
                                    Country = product.Supplier.Address.Country,
                                    Complement = product.Supplier.Address.Complement,
                                }
                            },
                            Images = product.Images.Select(image => new ReturnImageDto
                            {
                                ImageId = image.ImageId,
                                FormatType = image.FormatType,
                                ImageUrl = _apiPath + image.ImageId + image.FormatType,
                                CreationTime = image.CreationTime,
                                UpdateTime = image.UpdateTime
                            }).ToList(),
                            Discount = product.Discounts.Where(discount => discount.ProductId == product.ProductId)
                                .Select(discount => new ReturnDiscountDto
                                {
                                    DiscountId = discount.DiscountId,
                                    Value = discount.Value,
                                    Name = discount.Name,
                                    StartDate = discount.StartDate,
                                    EndDate = discount.EndDate,
                                    CreationTime = discount.CreationTime,
                                    UpdateTime = discount.UpdateTime,
                                    ProductId = discount.ProductId
                                }).FirstOrDefault()
                        }
                    }).ToList()
            }).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<PagedResult<ReturnOrderDto>> GetAll(OrderQueryParameters queryParameters,
        CancellationToken cancellationToken = default)
    {
        var query = from order in _context.Orders
            join status in _context.Statuses on order.StatusId equals status.StatusId
            select new ReturnOrderDto
            {
                OrderId = order.OrderId,
                DeliveryDate = order.DeliveryDate,
                DeletionTime = order.DeletionTime,
                CreationTime = order.CreationTime,
                UpdateTime = order.CreationTime,
                Customer = (from customer in _context.Customers
                    join address in _context.Addresses on customer.AddressId equals address.AddressId
                    where customer.CustomerId == order.CustomerId
                    select new ReturnCustomerDto
                    {
                        CustomerId = customer.CustomerId,
                        FirstName = customer.FirstName,
                        LastName = customer.LastName,
                        Email = customer.Email,
                        Phone = customer.Phone,
                        Active = customer.Active,
                        CreationTime = customer.CreationTime,
                        UpdateTime = customer.UpdateTime,
                        DeletionTime = customer.DeletionTime,
                        ValidationId = customer.ValidationId,
                        Address = new ReturnAddressDto
                        {
                            AddressId = address.AddressId,
                            AddressLine = address.AddressLine,
                            City = address.City,
                            Country = address.Country,
                            ZipCode = address.ZipCode,
                            Complement = address.Complement,
                            CreationTime = address.CreationTime,
                            UpdateTime = address.UpdateTime,
                            DeletionTime = address.DeletionTime
                        }
                    }).FirstOrDefault(),
                Status = new ReturnStatusDto
                {
                    StatusId = status.StatusId,
                    Name = status.Name,
                    DeletionTime = status.DeletionTime
                }
            };

        if (!string.IsNullOrWhiteSpace(queryParameters.customer_id))
        {
            query = query.Where(
                o => o.Customer.CustomerId.ToString().ToLower() == queryParameters.customer_id.ToLower());
        }

        if (!string.IsNullOrWhiteSpace(queryParameters.status_id))
        {
            query = query.Where(
                o => o.Status.StatusId.ToString().ToLower() == queryParameters.status_id.ToLower());
        }

        if (queryParameters.deleted == "only")
        {
            query = query.Where(f => f.DeletionTime != null);
        }
        else if (queryParameters.deleted == "all")
        {
        }
        else
            // Default to only returning undeleted items
        {
            query = query.Where(f => f.DeletionTime == null);
        }

        var totalCount = await query.CountAsync(cancellationToken);

        // Apply pagination: note the multiplication uses PageSize!
        var orders = await query
            .Skip((queryParameters.page - 1) * queryParameters.size)
            .Take(queryParameters.size)
            .ToListAsync(cancellationToken);

        return new PagedResult<ReturnOrderDto>
        {
            Items = orders,
            CurrentPage = queryParameters.page,
            PageSize = queryParameters.size,
            TotalCount = totalCount
        };
    }
}