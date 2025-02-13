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

namespace MonApi.API.Suppliers.Repositories
{
    public class SuppliersRepository : BaseRepository<Supplier>, ISuppliersRepository
    {
        public SuppliersRepository(StockManagementContext context) : base(context)
        { }

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
                    }
                })
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<PagedResult<ReturnSupplierDTO>> GetAll(SupplierQueryParameters queryParameters, CancellationToken cancellationToken = default)
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






