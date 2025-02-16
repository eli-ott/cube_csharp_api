using MonApi.Shared.Repositories;
using MonApi.API.Customers.Models;
using Microsoft.EntityFrameworkCore;
using MonApi.API.Addresses.DTOs;
using MonApi.API.Customers.DTOs;
using MonApi.API.Passwords.DTOs;
using MonApi.Shared.Data;
using MonApi.Shared.Pagination;
using MonApi.API.Customers.Filters;
using MonApi.API.Discounts.DTOs;
using MonApi.API.Families.Models;
using MonApi.API.Images.DTOs;
using MonApi.API.OrderLines.DTOs;
using MonApi.API.Orders.DTOs;
using MonApi.API.Products.DTOs;
using MonApi.API.Reviews.DTOs;
using MonApi.API.Statuses.DTOs;
using MonApi.API.Suppliers.DTOs;

namespace MonApi.API.Customers.Repositories
{
    public class CustomersRepository : BaseRepository<Customer>, ICustomersRepository
    {
        private readonly string _apiPath;

        public CustomersRepository(StockManagementContext context) : base(context)
        {
            var apiUrl = Environment.GetEnvironmentVariable("URL_API")
                         ?? throw new NullReferenceException("URL_API is null");
            var uploadDir = Environment.GetEnvironmentVariable("UPLOAD_DIR")
                            ?? throw new NullReferenceException("UPLOAD_DIR is null");

            _apiPath = apiUrl + uploadDir;
        }

        public async Task<ReturnCustomerDto?> FindAsync(int id, CancellationToken cancellationToken = default)
        {
            return await (from customer in _context.Customers
                join address in _context.Addresses on customer.AddressId equals address.AddressId
                where customer.CustomerId == id
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
                    },
                    Reviews = _context.Reviews.Where(r => r.UserId == customer.CustomerId)
                        .Select(r => new ReturnReviewDto
                        {
                            UserId = r.UserId,
                            ProductId = r.ProductId,
                            Rating = r.Rating,
                            Comment = r.Comment,
                            CreationTime = r.CreationTime,
                            UpdateTime = r.UpdateTime,
                            CustomerFirstName = r.User.FirstName,
                            CustomerLastName = r.User.LastName
                        }).ToList(),
                    Orders = customer.Orders.Select(order => new ReturnOrderDto
                    {
                        OrderId = order.OrderId,
                        DeliveryDate = order.DeliveryDate,
                        DeletionTime = order.DeletionTime,
                        CreationTime = order.CreationTime,
                        UpdateTime = order.CreationTime,
                        Status = new ReturnStatusDto
                        {
                            StatusId = order.Status.StatusId,
                            Name = order.Status.Name,
                            DeletionTime = order.Status.DeletionTime
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
                                    Discount = product.Discounts
                                        .Where(discount => discount.ProductId == product.ProductId)
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
                    }).ToList()
                }).FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<ReturnCustomerDto?> FindWithPasswordAsync(int id,
            CancellationToken cancellationToken = default)
        {
            return await (from customer in _context.Customers
                join address in _context.Addresses on customer.AddressId equals address.AddressId
                join password in _context.Passwords on customer.PasswordId equals password.PasswordId
                where customer.CustomerId == id
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
                    },
                    Password = new ReturnPasswordDto
                    {
                        PasswordId = password.PasswordId,
                        PasswordHash = password.PasswordHash,
                        PasswordSalt = password.PasswordSalt,
                        AttemptCount = password.AttemptCount,
                        UpdateTime = password.UpdateTime,
                        CreationTime = password.CreationTime
                    },
                    Reviews = _context.Reviews.Where(r => r.UserId == customer.CustomerId)
                        .Select(r => new ReturnReviewDto
                        {
                            UserId = r.UserId,
                            ProductId = r.ProductId,
                            Rating = r.Rating,
                            Comment = r.Comment,
                            CreationTime = r.CreationTime,
                            UpdateTime = r.UpdateTime,
                            CustomerFirstName = r.User.FirstName,
                            CustomerLastName = r.User.LastName
                        }).ToList(),
                    Orders = customer.Orders.Select(order => new ReturnOrderDto
                    {
                        OrderId = order.OrderId,
                        DeliveryDate = order.DeliveryDate,
                        DeletionTime = order.DeletionTime,
                        CreationTime = order.CreationTime,
                        UpdateTime = order.CreationTime,
                        Status = new ReturnStatusDto
                        {
                            StatusId = order.Status.StatusId,
                            Name = order.Status.Name,
                            DeletionTime = order.Status.DeletionTime
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
                                    Discount = product.Discounts
                                        .Where(discount => discount.ProductId == product.ProductId)
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
                    }).ToList()
                }).FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<ReturnCustomerDto?> FindByEmailAsync(string email,
            CancellationToken cancellationToken = default)
        {
            return await (from customer in _context.Customers
                join address in _context.Addresses on customer.AddressId equals address.AddressId
                join password in _context.Passwords on customer.PasswordId equals password.PasswordId
                where customer.Email == email
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
                    },
                    Password = new ReturnPasswordDto
                    {
                        PasswordId = password.PasswordId,
                        PasswordHash = password.PasswordHash,
                        PasswordSalt = password.PasswordSalt,
                        AttemptCount = password.AttemptCount,
                        UpdateTime = password.UpdateTime,
                        CreationTime = password.CreationTime
                    }
                }).FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<PagedResult<ReturnCustomerDto>> ListAsync(CustomerQueryParameters queryParameters,
            CancellationToken cancellationToken = default)
        {
            IQueryable<ReturnCustomerDto> query =
                from customer in _context.Customers
                join address in _context.Addresses on customer.AddressId equals address.AddressId
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
                };

            // Apply filters
            if (!string.IsNullOrWhiteSpace(queryParameters.email))
            {
                query = query.Where(c => c.Email.ToLower() == queryParameters.email.ToLower());
            }

            if (!string.IsNullOrWhiteSpace(queryParameters.first_name))
            {
                query = query.Where(c => c.FirstName.ToLower() == queryParameters.first_name.ToLower());
            }

            if (!string.IsNullOrWhiteSpace(queryParameters.last_name))
            {
                query = query.Where(c => c.LastName.ToLower() == queryParameters.last_name.ToLower());
            }

            if (!string.IsNullOrWhiteSpace(queryParameters.phone))
            {
                query = query.Where(c => c.Phone.ToLower() == queryParameters.phone.ToLower());
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

            if (queryParameters.active == "only")
            {
                query = query.Where(f => f.Active == true);
            }
            else if (queryParameters.active == "all")
            {
            }
            else
                // Default to only returning undeleted items
            {
                query = query.Where(f => f.Active == false);
            }

            // Get total count before pagination
            var totalCount = await query.CountAsync(cancellationToken);

            // Apply pagination: note the multiplication uses PageSize!
            var customers = await query
                .Skip((queryParameters.page - 1) * queryParameters.size)
                .Take(queryParameters.size)
                .ToListAsync(cancellationToken);

            return new PagedResult<ReturnCustomerDto>
            {
                Items = customers,
                CurrentPage = queryParameters.page,
                PageSize = queryParameters.size,
                TotalCount = totalCount
            };
        }
    }
}