using MonApi.Shared.Data;
using MonApi.Shared.Repositories;
using MonApi.API.Products.Models;
using MonApi.API.Products.DTOs;
using MonApi.API.Families.Models;
using MonApi.API.Suppliers.DTOs;
using Microsoft.EntityFrameworkCore;
using MonApi.API.Addresses.DTOs;

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

        public async Task<List<ReturnProductDTO>> GetAll(CancellationToken cancellationToken = default)
        {
            return await _context.Products
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
                }).ToListAsync();
        }
    }
}
