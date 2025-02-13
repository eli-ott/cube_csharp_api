using MonApi.Shared.Data;
using MonApi.Shared.Repositories;
using MonApi.API.Products.Models;
using MonApi.API.Products.DTOs;
using MonApi.API.Families.Models;
using MonApi.API.Suppliers.DTOs;
using Microsoft.EntityFrameworkCore;
using MonApi.API.Addresses.DTOs;
using MonApi.API.Products.Filters;
using MonApi.Shared.Pagination;

namespace MonApi.API.Products.Repositories
{
    public class ProductsRepository : BaseRepository<Product>, IProductsRepository
    {
        public ProductsRepository(StockManagementContext context) : base(context)
        {
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
                    IsBio = product.IsBio,
                    UnitPrice = product.UnitPrice,
                    CartonPrice = product.CartonPrice,
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
                    }
                })
                .FirstOrDefaultAsync();
        }

        public async Task<PagedResult<ReturnProductDTO>> GetAll(ProductQueryParameters queryParameters, CancellationToken cancellationToken = default)
        {
            IQueryable<ReturnProductDTO> query = _context.Products
                .Select(product => new ReturnProductDTO
                {
                    ProductId = product.ProductId,
                    Name = product.Name,
                    Cuvee = product.Cuvee,
                    Year = product.Year,
                    ProducerName = product.ProducerName,
                    IsBio = product.IsBio,
                    UnitPrice = product.UnitPrice,
                    CartonPrice = product.CartonPrice,
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

                    }
                });

            // Apply filters
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
                query = query.Where(p => p.Supplier.SupplierId == queryParameters.supplier_id);
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
    }
}
