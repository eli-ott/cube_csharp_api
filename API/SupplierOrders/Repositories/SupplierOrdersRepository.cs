using Microsoft.EntityFrameworkCore;
using MonApi.API.Addresses.DTOs;
using MonApi.API.Discounts.DTOs;
using MonApi.API.Employees.DTOs;
using MonApi.API.Families.Models;
using MonApi.API.Images.DTOs;
using MonApi.API.Products.DTOs;
using MonApi.API.Roles.DTOs;
using MonApi.API.Statuses.DTOs;
using MonApi.API.SupplierOrderLines.DTOs;
using MonApi.API.SupplierOrders.DTOs;
using MonApi.API.SupplierOrders.Filters;
using MonApi.API.SupplierOrders.Models;
using MonApi.API.Suppliers.DTOs;
using MonApi.Shared.Data;
using MonApi.Shared.Pagination;
using MonApi.Shared.Repositories;

namespace MonApi.API.SupplierOrders.Repositories;

public class SupplierOrdersRepository : BaseRepository<SupplierOrder>, ISupplierOrdersRepository
{
    private readonly string _apiPath;

    public SupplierOrdersRepository(StockManagementContext context) : base(context)
    {
        var apiUrl = Environment.GetEnvironmentVariable("URL_API")
                     ?? throw new NullReferenceException("URL_API is null");
        var uploadDir = Environment.GetEnvironmentVariable("UPLOAD_DIR")
                        ?? throw new NullReferenceException("UPLOAD_DIR is null");

        _apiPath = apiUrl + uploadDir;
    }

    public async Task<ReturnSupplierOrderDto?> FindAsync(int orderId, CancellationToken cancellationToken = default)
    {
        return await (from order in _context.SupplierOrders
            join status in _context.Statuses on order.StatusId equals status.StatusId
            where order.OrderId == orderId
            select new ReturnSupplierOrderDto
            {
                OrderId = order.OrderId,
                DeliveryDate = order.DeliveryDate,
                DeletionTime = order.DeletionTime,
                CreationTime = order.CreationTime,
                UpdateTime = order.CreationTime,
                Employee = (from employee in _context.Employees
                    where employee.EmployeeId == order.EmployeeId
                    select new ReturnEmployeeDto
                    {
                        EmployeeId = employee.EmployeeId,
                        FirstName = employee.FirstName,
                        LastName = employee.LastName,
                        Email = employee.Email,
                        Phone = employee.Phone,
                        CreationTime = employee.CreationTime,
                        UpdateTime = employee.UpdateTime,
                        DeletionTime = employee.DeletionTime,
                        Role = new ReturnRoleDTO
                        {
                            RoleId = employee.Role.RoleId,
                            Name = employee.Role.Name,
                            CreationTime = employee.Role.CreationTime,
                            UpdateTime = employee.Role.UpdateTime,
                            DeletionTime = employee.Role.DeletionTime
                        }
                    }).FirstOrDefault(),
                Status = new ReturnStatusDto
                {
                    StatusId = status.StatusId,
                    Name = status.Name,
                    DeletionTime = status.DeletionTime
                },
                Lines = (from supplierOrderLine in _context.SupplierOrderLines
                    join product in _context.Products on supplierOrderLine.ProductId equals product.ProductId
                    where supplierOrderLine.OrderId == order.OrderId
                    select new ReturnSupplierOrderLineDto
                    {
                        OrderId = supplierOrderLine.OrderId,
                        Quantity = supplierOrderLine.Quantity,
                        UnitPrice = supplierOrderLine.UnitPrice,
                        UpdateTime = supplierOrderLine.UpdateTime,
                        CreationTime = supplierOrderLine.CreationTime,
                        DeletionTime = supplierOrderLine.DeletionTime,
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
                                CreationTime = product.Supplier.CreationTime,
                                UpdateTime = product.Supplier.UpdateTime,
                                Address = new ReturnAddressDto
                                {
                                    AddressId = product.Supplier.Address.AddressId,
                                    AddressLine = product.Supplier.Address.AddressLine,
                                    City = product.Supplier.Address.City,
                                    ZipCode = product.Supplier.Address.ZipCode,
                                    Country = product.Supplier.Address.Country,
                                    Complement = product.Supplier.Address.Complement
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

    public async Task<PagedResult<ReturnSupplierOrderDto>> GetAll(SupplierOrdersQueryParameters queryParameters,
        CancellationToken cancellationToken = default)
    {
        var query = from order in _context.SupplierOrders
            join status in _context.Statuses on order.StatusId equals status.StatusId
            select new ReturnSupplierOrderDto
            {
                OrderId = order.OrderId,
                DeliveryDate = order.DeliveryDate,
                DeletionTime = order.DeletionTime,
                CreationTime = order.CreationTime,
                UpdateTime = order.CreationTime,
                Employee = (from employee in _context.Employees
                    where employee.EmployeeId == order.EmployeeId
                    select new ReturnEmployeeDto
                    {
                        EmployeeId = employee.EmployeeId,
                        FirstName = employee.FirstName,
                        LastName = employee.LastName,
                        Email = employee.Email,
                        Phone = employee.Phone,
                        CreationTime = employee.CreationTime,
                        UpdateTime = employee.UpdateTime,
                        DeletionTime = employee.DeletionTime,
                        Role = new ReturnRoleDTO
                        {
                            RoleId = employee.Role.RoleId,
                            Name = employee.Role.Name,
                            CreationTime = employee.Role.CreationTime,
                            UpdateTime = employee.Role.UpdateTime,
                            DeletionTime = employee.Role.DeletionTime
                        }
                    }).FirstOrDefault(),
                Status = new ReturnStatusDto
                {
                    StatusId = status.StatusId,
                    Name = status.Name,
                    DeletionTime = status.DeletionTime
                }
            };

        if (!string.IsNullOrWhiteSpace(queryParameters.employee_id))
        {
            query = query.Where(
                o => o.Employee.EmployeeId.ToString().ToLower() == queryParameters.employee_id.ToLower());
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

        return new PagedResult<ReturnSupplierOrderDto>
        {
            Items = orders,
            CurrentPage = queryParameters.page,
            PageSize = queryParameters.size,
            TotalCount = totalCount
        };
    }
}