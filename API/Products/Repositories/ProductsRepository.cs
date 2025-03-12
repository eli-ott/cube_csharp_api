using MonApi.Shared.Data;
using MonApi.Shared.Repositories;
using MonApi.API.Products.Models;
using MonApi.API.Products.DTOs;
using MonApi.API.Families.Models;
using MonApi.API.Suppliers.DTOs;
using Microsoft.EntityFrameworkCore;
using MonApi.API.Addresses.DTOs;
using MonApi.API.Discounts.DTOs;
using MonApi.API.Images.DTOs;
using MonApi.API.Products.Filters;
using MonApi.API.Reviews.DTOs;
using MonApi.Shared.Pagination;
using MonApi.API.Families.DTOs;

namespace MonApi.API.Products.Repositories
{
    public class ProductsRepository : BaseRepository<Product>, IProductsRepository
    {
        private readonly string _apiPath;

        public ProductsRepository(StockManagementContext context) : base(context)
        {
            var apiUrl = Environment.GetEnvironmentVariable("URL_API")
                         ?? throw new NullReferenceException("URL_API is null");
            var uploadDir = Environment.GetEnvironmentVariable("UPLOAD_DIR")
                            ?? throw new NullReferenceException("UPLOAD_DIR is null");

            _apiPath = apiUrl + uploadDir;
        }

        public async Task<ReturnProductDTO?> FindProduct(int id, CancellationToken cancellationToken = default)
        {
            return await _context.Products
                .Where(p => p.ProductId == id)
                .Select(product => new ReturnProductDTO
                {
                    ProductId = product.ProductId,
                    Name = product.Name,
                    Cuvee = product.Cuvee,
                    Year = product.Year,
                    ProducerName = product.ProducerName,
                    Description = product.Description,
                    IsBio = product.IsBio,
                    UnitPrice = product.UnitPrice,
                    BoxPrice = product.BoxPrice,
                    Quantity = product.Quantity,
                    AutoRestock = product.AutoRestock,
                    AutoRestockTreshold = product.AutoRestockTreshold,
                    DeletionTime = product.DeletionTime,
                    UpdateTime = product.UpdateTime,
                    CreationTime = product.CreationTime,
                    Family = new ReturnFamilyDTO
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
                        }).FirstOrDefault(),
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
                }).FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<PagedResult<ReturnProductDTO>> GetAll(ProductQueryParameters queryParameters,
            CancellationToken cancellationToken = default)
        {
            IQueryable<ReturnProductDTO> query = _context.Products
                .OrderBy(product => product.ProductId)
                .Select(product => new ReturnProductDTO
                {
                    ProductId = product.ProductId,
                    Name = product.Name,
                    Cuvee = product.Cuvee,
                    Year = product.Year,
                    ProducerName = product.ProducerName,
                    Description = product.Description,
                    IsBio = product.IsBio,
                    UnitPrice = product.UnitPrice,
                    BoxPrice = product.BoxPrice,
                    Quantity = product.Quantity,
                    AutoRestock = product.AutoRestock,
                    AutoRestockTreshold = product.AutoRestockTreshold,
                    DeletionTime = product.DeletionTime,
                    UpdateTime = product.UpdateTime,
                    CreationTime = product.CreationTime,
                    Family = new ReturnFamilyDTO
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
                            Complement = product.Supplier.Address.Complement,
                        }
                    },
                    Images = product.Images.Select(image => new ReturnImageDto
                    {
                        ImageId = image.ImageId,
                        FormatType = image.FormatType,
                        ImageUrl = _apiPath + image.ImageId + image.FormatType,
                        CreationTime = image.CreationTime,
                        UpdateTime = image.UpdateTime,
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
                        }).FirstOrDefault(),
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
                });

            // Apply filters
            if (queryParameters.name != null)
            {
                query = query.Where(p => p.Name.ToLower().StartsWith(queryParameters.name.ToLower()));
            }

            if (queryParameters.year != null)
            {
                query = query.Where(p => p.Year == queryParameters.year);
            }

            if (queryParameters.is_bio != null)
            {
                query = query.Where(p => p.IsBio == queryParameters.is_bio);
            }
            if (queryParameters.price_min != null)
            {
                query = query.Where(p => p.UnitPrice >= queryParameters.price_min);
            }
            if (queryParameters.price_max != null)
            {
                query = query.Where(p => p.UnitPrice <= queryParameters.price_max);
            }

            if (queryParameters.family_id != null)
            {
                query = query.Where(p => p.Family.FamilyId == queryParameters.family_id);
            }

            if (queryParameters.supplier_id != null)
            {
                query = query.Where(p => p.Supplier!.SupplierId == queryParameters.supplier_id);
            }

            // deleted=all, deleted=only, deleted=none
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

            var products = await query
                .Skip(queryParameters.size * (queryParameters.page - 1))
                .Take(queryParameters.size)
                .ToListAsync(cancellationToken);

            return new PagedResult<ReturnProductDTO>
            {
                Items = products,
                CurrentPage = queryParameters.page,
                PageSize = queryParameters.size,
                TotalCount = totalCount
            };
        }

        public async Task UpdateRange(List<Product> products, CancellationToken cancellationToken = default)
        {
            _context.Set<Product>().UpdateRange(products);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}