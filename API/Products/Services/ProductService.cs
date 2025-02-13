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


            Product product = productToCreate.MapToProductModel();

            var newProductDetails = await _productsRepository.AddAsync(product);
            var family = await _familiesRepository.FindAsync(product.FamilyId) ?? throw new KeyNotFoundException("Family Id not found");
            var returnedSupplier = await _suppliersRepository.FindAsync(product.SupplierId) ?? throw new KeyNotFoundException("Supplier Id not found");

            var addedProductDetails = await _productsRepository.FindProduct(newProductDetails.ProductId);

            return addedProductDetails;
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

        public async Task<Product> UpdateAsync(Product product)
        {
            return product;
        }
    }
}
