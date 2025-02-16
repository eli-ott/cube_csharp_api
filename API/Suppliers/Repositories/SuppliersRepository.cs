using Microsoft.EntityFrameworkCore;
using MonApi.API.Addresses.DTOs;
using MonApi.API.Addresses.Extensions;
using MonApi.API.Addresses.Repositories;
using MonApi.API.Customers.DTOs;
using MonApi.API.Suppliers.DTOs;
using MonApi.API.Suppliers.Extensions;
using MonApi.API.Suppliers.Filters;
using MonApi.API.Suppliers.Models;
using MonApi.Shared.Data;
using MonApi.Shared.Pagination;
using MonApi.Shared.Repositories;
using System.Security.Cryptography.X509Certificates;
using MonApi.API.Images.DTOs;
using MonApi.API.Products.DTOs;
using MonApi.API.Reviews.DTOs;

namespace MonApi.API.Suppliers.Repositories
{
    public class SuppliersRepository : BaseRepository<Supplier>, ISuppliersRepository
    {
        private readonly string _apiPath;

        public SuppliersRepository(StockManagementContext context) : base(context)
        {
            var apiUrl = Environment.GetEnvironmentVariable("URL_API")
                         ?? throw new NullReferenceException("URL_API is null");
            var uploadDir = Environment.GetEnvironmentVariable("UPLOAD_DIR")
                            ?? throw new NullReferenceException("UPLOAD_DIR is null");

            _apiPath = apiUrl + uploadDir;
        }

        public async Task<ReturnSupplierDTO?> FindAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _context.Suppliers
                .Where(s => s.SupplierId == id)
                .Select(supplier => new ReturnSupplierDTO
                {
                    SupplierId = supplier.SupplierId,
                    FirstName = supplier.FirstName,
                    LastName = supplier.LastName,
                    Email = supplier.Email,
                    Contact = supplier.Contact,
                    Phone = supplier.Phone,
                    Siret = supplier.Siret,
                    CreationTime = supplier.CreationTime,
                    UpdateTime = supplier.UpdateTime,
                    DeletionTime = supplier.DeletionTime,
                    Address = new ReturnAddressDto
                    {
                        AddressId = supplier.Address.AddressId,
                        AddressLine = supplier.Address.AddressLine,
                        City = supplier.Address.City,
                        Country = supplier.Address.Country,
                        ZipCode = supplier.Address.ZipCode,
                        Complement = supplier.Address.Complement,
                        CreationTime = supplier.Address.CreationTime,
                        UpdateTime = supplier.Address.UpdateTime,
                        DeletionTime = supplier.Address.DeletionTime
                    },
                    Products = _context.Products.Where(p => p.SupplierId == supplier.SupplierId)
                        .Select(product => new ReturnProductDTO
                        {
                            ProductId = product.ProductId,
                            Name = product.Name,
                            AutoRestock = product.AutoRestock,
                            AutoRestockTreshold = product.AutoRestockTreshold,
                            BoxPrice = product.BoxPrice,
                            Cuvee = product.Cuvee,
                            CreationTime = product.CreationTime,
                            UpdateTime = product.UpdateTime,
                            DeletionTime = product.DeletionTime,
                            Year = product.Year,
                            ProducerName = product.ProducerName,
                            IsBio = product.IsBio,
                            Quantity = product.Quantity,
                            Family = product.Family,
                            Images = product.Images.Select(image => new ReturnImageDto
                            {
                                ImageId = image.ImageId,
                                FormatType = image.FormatType,
                                ImageUrl = _apiPath + image.ImageId + image.FormatType,
                                CreationTime = image.CreationTime,
                                UpdateTime = image.UpdateTime
                            }).ToList(),
                            Reviews = _context.Reviews.Where(r => r.ProductId == product.ProductId)
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
                                }).ToList()
                        }).ToList()
                })
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<PagedResult<ReturnSupplierDTO>> GetAll(SupplierQueryParameters queryParameters,
            CancellationToken cancellationToken = default)
        {
            IQueryable<ReturnSupplierDTO> query = _context.Suppliers
                .Select(supplier => new ReturnSupplierDTO
                {
                    SupplierId = supplier.SupplierId,
                    FirstName = supplier.FirstName,
                    LastName = supplier.LastName,
                    Email = supplier.Email,
                    Contact = supplier.Contact,
                    Phone = supplier.Phone,
                    Siret = supplier.Siret,
                    CreationTime = supplier.CreationTime,
                    UpdateTime = supplier.UpdateTime,
                    DeletionTime = supplier.DeletionTime,
                    Address = new ReturnAddressDto
                    {
                        AddressId = supplier.Address.AddressId,
                        AddressLine = supplier.Address.AddressLine,
                        City = supplier.Address.City,
                        Country = supplier.Address.Country,
                        ZipCode = supplier.Address.ZipCode,
                        Complement = supplier.Address.Complement,
                        CreationTime = supplier.Address.CreationTime,
                        UpdateTime = supplier.Address.UpdateTime,
                        DeletionTime = supplier.Address.DeletionTime
                    }
                });

            // Apply filters
            if (!string.IsNullOrWhiteSpace(queryParameters.first_name))
            {
                query = query.Where(f => f.FirstName.ToLower() == queryParameters.first_name.ToLower());
            }

            if (!string.IsNullOrWhiteSpace(queryParameters.last_name))
            {
                query = query.Where(f => f.LastName.ToLower() == queryParameters.last_name.ToLower());
            }

            if (!string.IsNullOrWhiteSpace(queryParameters.email))
            {
                query = query.Where(f => f.Email.ToLower() == queryParameters.email.ToLower());
            }

            if (!string.IsNullOrWhiteSpace(queryParameters.siret))
            {
                query = query.Where(f => f.Siret.ToLower() == queryParameters.siret.ToLower());
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

            // Get total count before pagination
            var totalCount = await query.CountAsync(cancellationToken);

            // Apply pagination: note the multiplication uses PageSize!
            var suppliers = await query
                .Skip((queryParameters.page - 1) * queryParameters.size)
                .Take(queryParameters.size)
                .ToListAsync(cancellationToken);

            return new PagedResult<ReturnSupplierDTO>
            {
                Items = suppliers,
                CurrentPage = queryParameters.page,
                PageSize = queryParameters.size,
                TotalCount = totalCount
            };
        }
    }
}