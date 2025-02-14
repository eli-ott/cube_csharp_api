using Microsoft.EntityFrameworkCore;
using MonApi.API.Addresses.DTOs;
using MonApi.API.CartLines.DTOs;
using MonApi.API.Carts.DTOs;
using MonApi.API.Carts.Models;
using MonApi.API.Discounts.DTOs;
using MonApi.API.Families.Models;
using MonApi.API.Images.DTOs;
using MonApi.API.Products.DTOs;
using MonApi.API.Reviews.DTOs;
using MonApi.API.Suppliers.DTOs;
using MonApi.Shared.Data;
using MonApi.Shared.Repositories;

namespace MonApi.API.Carts.Repositories;

public class CartRepository : BaseRepository<Cart>, ICartRepository
{
    private readonly string _apiPath;

    public CartRepository(StockManagementContext context) : base(context)
    {
        var apiUrl = Environment.GetEnvironmentVariable("URL_API")
                     ?? throw new NullReferenceException("URL_API is null");
        var uploadDir = Environment.GetEnvironmentVariable("UPLOAD_DIR")
                        ?? throw new NullReferenceException("UPLOAD_DIR is null");

        _apiPath = apiUrl + uploadDir;
    }

    public async Task<ReturnCartDto?> GetCart(int customerId, CancellationToken cancellationToken = default)
    {
        return await (from cart in _context.Carts
            where cart.CustomerId == customerId
            select new ReturnCartDto
            {
                CartId = cart.CartId,
                CreationTime = cart.CreationTime,
                UpdateTime = cart.UpdateTime,
                CartLines = _context.CartLines.Where(line => line.CartId == cart.CartId)
                    .Select(line => new ReturnCartLineDto
                    {
                        Quantity = line.Quantity,
                        IsSetAside = line.IsSetAside,
                        CreationTime = line.CreationTime,
                        UpdateTime = line.UpdateTime,
                        Product = _context.Products
                            .Where(p => p.ProductId == line.ProductId)
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
                            }).FirstOrDefault()
                    }).ToList()
            }).FirstOrDefaultAsync(cancellationToken);
    }
}