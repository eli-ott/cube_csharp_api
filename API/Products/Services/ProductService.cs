using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using MonApi.API.Addresses.Extensions;
using MonApi.API.Addresses.Models;
using MonApi.API.Addresses.Repositories;
using MonApi.API.Families.Models;
using MonApi.API.Families.Repositories;
using MonApi.API.Products.DTOs;
using MonApi.API.Products.Extensions;
using MonApi.API.Products.Models;
using MonApi.API.Products.Repositories;
using MonApi.API.Suppliers.DTOs;
using MonApi.API.Suppliers.Extensions;
using MonApi.API.Suppliers.Models;
using MonApi.API.Suppliers.Repositories;
using System.Diagnostics;
using MonApi.API.Images.Repositories;
using MonApi.Shared.Utils;

namespace MonApi.API.Products.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductsRepository _productsRepository;
        private readonly IImagesRepository _imagesRepository;
        private readonly IFamiliesRepository _familiesRepository;
        private readonly ISuppliersRepository _suppliersRepository;

        public ProductService(ISuppliersRepository suppliersRepository, IAddressRepository addressesRepository,
            IFamiliesRepository familiesRepository, IProductsRepository productsRepository,
            IImagesRepository imagesRepository)
        {
            _imagesRepository = imagesRepository;
            _suppliersRepository = suppliersRepository;
            _productsRepository = productsRepository;
            _familiesRepository = familiesRepository;
        }

        public async Task<ReturnProductDTO> AddAsync(CreateProductDTO productToCreate)
        {
            if (productToCreate.UnitPrice == null && productToCreate.CartonPrice == null)
                throw new ArgumentException("At least one type of price is required");
            if (productToCreate.AutoRestock && productToCreate.AutoRestockTreshold == null)
                throw new ArgumentException("Treshold is needed if the automatic restock is activated");
            if (productToCreate.CartonPrice <= 0 || productToCreate.UnitPrice <= 0)
                throw new ArgumentException("The prices have to be superior to zero");
            if (productToCreate.AutoRestockTreshold < 0)
                throw new ArgumentException("The restock treshold has to be superior to zero");

            var imagesToUpload = productToCreate.Images ?? new List<IFormFile>();

            Product product = productToCreate.MapToProductModel();

            var newProductDetails = await _productsRepository.AddAsync(product);
            var family = await _familiesRepository.FindAsync(product.FamilyId) ??
                         throw new KeyNotFoundException("Family Id not found");
            var returnedSupplier = await _suppliersRepository.FindAsync(product.SupplierId) ??
                                   throw new KeyNotFoundException("Supplier Id not found");

            var addedProductDetails = await _productsRepository.FindProduct(newProductDetails.ProductId);

            if (imagesToUpload.Count > 0)
            {
                var uploadedImages = await ImageUtils.AddImagesList(imagesToUpload, addedProductDetails!.ProductId);
                await _imagesRepository.AddRangeAsync(uploadedImages);
            }

            return addedProductDetails!;
        }

        public async Task<List<ReturnProductDTO>> GetAll()
        {
            var products = await _productsRepository.GetAll();
            return products;
        }

        public async Task<ReturnProductDTO> GetById(int id)
        {
            ReturnProductDTO returnedProduct = await _productsRepository.FindProduct(id);
            return returnedProduct;
        }

        public async Task<ReturnProductDTO> SoftDeleteAsync(int id)
        {
            Product product = await _productsRepository.FindAsync(id) ?? throw new KeyNotFoundException("Id not found");
            if (product.DeletionTime != null) throw new Exception("Product already deleted");

            product.DeletionTime = DateTime.UtcNow;
            _productsRepository.UpdateAsync(product);
            ReturnProductDTO returnProductDTO = await _productsRepository.FindProduct(product.ProductId);

            return returnProductDTO;
        }

        public async Task<ReturnProductDTO> UpdateAsync(int id, UpdateProductDTO toUpdateProduct)
        {
            Product productToModify = toUpdateProduct.MapToProductModel(id);
            await _productsRepository.UpdateAsync(productToModify);
            ReturnProductDTO modifiedProductDetails = await _productsRepository.FindProduct(productToModify.ProductId);
            return modifiedProductDetails;
        }

        public async Task<ReturnProductRestockDTO> ToggleRestock(int id)
        {
            ReturnProductDTO foundProduct = await _productsRepository.FindProduct(id) ??
                                            throw new KeyNotFoundException("Id not found");
            foundProduct.AutoRestock = !foundProduct.AutoRestock;

            Product productToUpdate = foundProduct.MapToProductModel();
            productToUpdate.ProductId = id;
            await _productsRepository.UpdateAsync(productToUpdate);
            return productToUpdate.MapToRestockDTO();
        }

        public async Task<ReturnProductBioDTO> ToggleIsBio(int id)
        {
            ReturnProductDTO foundProduct = await _productsRepository.FindProduct(id) ??
                                            throw new KeyNotFoundException("Id not found");
            foundProduct.IsBio = !foundProduct.IsBio;

            Product productToUpdate = foundProduct.MapToProductModel();
            productToUpdate.ProductId = id;
            await _productsRepository.UpdateAsync(productToUpdate);
            return productToUpdate.MapToBioDTO();
        }
    }
}