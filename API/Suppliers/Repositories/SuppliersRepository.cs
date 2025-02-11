using Microsoft.EntityFrameworkCore;
using MonApi.API.Addresses.DTOs;
using MonApi.API.Addresses.Extensions;
using MonApi.API.Addresses.Repositories;
using MonApi.API.Customers.DTOs;
using MonApi.API.Suppliers.DTOs;
using MonApi.API.Suppliers.Extensions;
using MonApi.API.Suppliers.Models;
using MonApi.Shared.Data;
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

    }
}






