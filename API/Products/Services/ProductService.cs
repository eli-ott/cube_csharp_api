using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using MonApi.API.Addresses.Extensions;
using MonApi.API.Addresses.Models;
using MonApi.API.Addresses.Repositories;
using MonApi.API.Families.Models;
using MonApi.API.Families.Repositories;
using MonApi.API.Products.DTOs;
using MonApi.API.Products.Extensions;
using MonApi.API.Products.Filters;
using MonApi.API.Products.Models;
using MonApi.API.Products.Repositories;
using MonApi.API.Suppliers.DTOs;
using MonApi.API.Suppliers.Extensions;
using MonApi.API.Suppliers.Models;
using MonApi.API.Suppliers.Repositories;
using MonApi.Shared.Pagination;
using System.Diagnostics;

namespace MonApi.API.Products.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductsRepository _productsRepository;
        private readonly IAddressRepository _addressesRepository;
        private readonly IFamiliesRepository _familiesRepository;
        private readonly ISuppliersRepository _suppliersRepository;

        public ProductService(ISuppliersRepository suppliersRepository, IAddressRepository addressesRepository, IFamiliesRepository familiesRepository, IProductsRepository productsRepository)
        {
            _addressesRepository = addressesRepository;
            _suppliersRepository = suppliersRepository;
            _productsRepository = productsRepository;
            _familiesRepository = familiesRepository;
        }

        public async Task<ReturnProductDTO> AddAsync(CreateProductDTO productToCreate)
        {

            if (productToCreate.UnitPrice == null && productToCreate.CartonPrice == null) throw new ArgumentException("At least one type of price is required");
            if (productToCreate.AutoRestock && productToCreate.AutoRestockTreshold == null) throw new ArgumentException("Treshold is needed if the automatic restock is activated");
            if (productToCreate.CartonPrice <= 0 || productToCreate.UnitPrice <= 0) throw new ArgumentException("The prices have to be superior to zero");
            if (productToCreate.AutoRestockTreshold < 0) throw new ArgumentException("The restock treshold has to be superior to zero");


            Product product = productToCreate.MapToProductModel();

            var newProductDetails = await _productsRepository.AddAsync(product);
            var family = await _familiesRepository.FindAsync(product.FamilyId) ?? throw new KeyNotFoundException("Family Id not found");
            var returnedSupplier = await _suppliersRepository.FindAsync(product.SupplierId) ?? throw new KeyNotFoundException("Supplier Id not found");

            var addedProductDetails = await _productsRepository.FindProduct(newProductDetails.ProductId) 
                ?? throw new KeyNotFoundException("Product not found");

            return addedProductDetails;
        }

        public async Task<PagedResult<ReturnProductDTO>> GetAll(ProductQueryParameters queryParameters)
        {
            var products = await _productsRepository.GetAll(queryParameters);
            return products;
        }

        public async Task<ReturnProductDTO> GetById(int id)
        {
            ReturnProductDTO returnedProduct = await _productsRepository.FindProduct(id) 
                ?? throw new KeyNotFoundException("Product not found");

            return returnedProduct;
        }

        public async Task<ReturnProductDTO> SoftDeleteAsync(int id)
        {
            Product product = await _productsRepository.FindAsync(id) ?? throw new KeyNotFoundException("Id not found");
            if (product.DeletionTime != null) throw new Exception("Product already deleted");

            product.DeletionTime = DateTime.UtcNow;
            await _productsRepository.UpdateAsync(product);
            ReturnProductDTO returnProductDTO = await _productsRepository.FindProduct(product.ProductId)
                ?? throw new KeyNotFoundException("Product not found");

            return returnProductDTO;
        }

        public async Task<ReturnProductDTO> UpdateAsync(int id, UpdateProductDTO toUpdateProduct)
        {
            Product productToModify = toUpdateProduct.MapToProductModel(id);
            await _productsRepository.UpdateAsync(productToModify);

            ReturnProductDTO modifiedProductDetails = await _productsRepository.FindProduct(productToModify.ProductId) 
                ?? throw new KeyNotFoundException("Product not found");

            return modifiedProductDetails;
        }

        public async Task<ReturnProductRestockDTO> ToggleRestock(int id)
        {
            ReturnProductDTO foundProduct = await _productsRepository.FindProduct(id) ?? throw new KeyNotFoundException("Id not found");
            foundProduct.AutoRestock = !foundProduct.AutoRestock;

            Product productToUpdate = foundProduct.MapToProductModel();
            productToUpdate.ProductId = id;
            await _productsRepository.UpdateAsync(productToUpdate);
            return productToUpdate.MapToRestockDTO();
        }

        public async Task<ReturnProductBioDTO> ToggleIsBio(int id)
        {
            ReturnProductDTO foundProduct = await _productsRepository.FindProduct(id) ?? throw new KeyNotFoundException("Id not found");
            foundProduct.IsBio = !foundProduct.IsBio;

            Product productToUpdate = foundProduct.MapToProductModel();
            productToUpdate.ProductId = id;
            await _productsRepository.UpdateAsync(productToUpdate);
            return productToUpdate.MapToBioDTO();
        }
    }
}
