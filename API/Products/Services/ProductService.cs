using System.Text.Json;
using MonApi.API.Addresses.Repositories;
using MonApi.API.CartLines.Repositories;
using MonApi.API.Discounts.Extensions;
using MonApi.API.Discounts.Repositories;
using MonApi.API.Families.Repositories;
using MonApi.API.Images.Extensions;
using MonApi.API.Images.Models;
using MonApi.API.Products.DTOs;
using MonApi.API.Products.Extensions;
using MonApi.API.Products.Filters;
using MonApi.API.Products.Models;
using MonApi.API.Products.Repositories;
using MonApi.API.Suppliers.Repositories;
using MonApi.Shared.Exceptions;
using MonApi.Shared.Pagination;
using MonApi.API.Images.Repositories;
using MonApi.API.Reviews.Extensions;
using MonApi.API.Reviews.Models;
using MonApi.API.Reviews.Repositories;
using MonApi.Shared.Utils;

namespace MonApi.API.Products.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductsRepository _productsRepository;
        private readonly IImagesRepository _imagesRepository;
        private readonly IFamiliesRepository _familiesRepository;
        private readonly ISuppliersRepository _suppliersRepository;
        private readonly IReviewRepository _reviewRepository;
        private readonly IDiscountRepository _discountRepository;
        private readonly ICartLineRepository _cartLineRepository;

        public ProductService(ISuppliersRepository suppliersRepository, IReviewRepository reviewRepository,
            IFamiliesRepository familiesRepository, IProductsRepository productsRepository,
            IImagesRepository imagesRepository, IDiscountRepository discountRepository,
            ICartLineRepository cartLineRepository)
        {
            _imagesRepository = imagesRepository;
            _suppliersRepository = suppliersRepository;
            _productsRepository = productsRepository;
            _familiesRepository = familiesRepository;
            _reviewRepository = reviewRepository;
            _discountRepository = discountRepository;
            _cartLineRepository = cartLineRepository;
        }

        public async Task<ReturnProductDTO> AddAsync(CreateProductDTO productToCreate)
        {
            if (productToCreate.UnitPrice == null && productToCreate.BoxPrice == null)
                throw new ArgumentException("At least one type of price is required");
            if (productToCreate.AutoRestock && productToCreate.AutoRestockTreshold == null)
                throw new ArgumentException("Treshold is needed if the automatic restock is activated");
            if (productToCreate.BoxPrice <= 0 || productToCreate.UnitPrice <= 0)
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

            if (imagesToUpload.Count > 0)
            {
                var uploadedImages = await ImageUtils.AddImagesList(imagesToUpload, newProductDetails.ProductId);
                await _imagesRepository.AddRangeAsync(uploadedImages);
            }

            var addedProductDetails = await _productsRepository.FindProduct(newProductDetails.ProductId)
                                      ?? throw new KeyNotFoundException("Product not found");


            return addedProductDetails!;
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
            var product = await _productsRepository.FindProduct(id) ?? throw new KeyNotFoundException("Id not found");
            if (product.DeletionTime != null) throw new SoftDeletedException("This product has been deleted already.");
            
            var cartLines = await _cartLineRepository.ListAsync(line => line.ProductId == product.ProductId);
            await _cartLineRepository.RemoveRangeAsync(cartLines);

            var productImages = await _imagesRepository.GetImagesByProductIdAsync(product.ProductId);
            var mappedImages = productImages.Select(x => x.MapToImageModel()).ToList();

            if (productImages.Count > 0)
            {
                ImageUtils.DeleteImageList(mappedImages);
                await _imagesRepository.RemoveRangeAsync(mappedImages);
            }

            // Delete the discounts associated with this product
            if (product.Discount != null)
            {
                await _discountRepository.DeleteAsync(product.Discount.MapToDiscountModel());
            }

            var reviews = await _reviewRepository.GetReviewsByProductAsync(product.ProductId);
            if (reviews.Count > 0)
            {
                var mappedReviews = reviews.Select(x => x.MapReviewToModel()).ToList();
                await _reviewRepository.RemoveRangeAsync(mappedReviews);
            }

            product.DeletionTime = DateTime.UtcNow;
            await _productsRepository.SoftDeleteAsync(product.MapToProductModel());

            return product;
        }

        public async Task<ReturnProductDTO> UpdateAsync(int id, UpdateProductDTO toUpdateProduct)
        {
            var imagesToUpload = toUpdateProduct.Images ?? new List<IFormFile>();
            var foundProduct = await _productsRepository.FindProduct(id);
            Product productToModify = toUpdateProduct.MapToProductModel(id);

            var currentImagesCount = await _imagesRepository.CountAsync(x => x.ProductId == productToModify.ProductId);

            var foundSupplier = await _suppliersRepository.FindAsync(productToModify.SupplierId)
                                ?? throw new KeyNotFoundException("Supplier not found");
            if (foundSupplier.DeletionTime != null)
            {
                if (toUpdateProduct.AutoRestock != foundProduct!.AutoRestock
                    || toUpdateProduct.Quantity != foundProduct.Quantity)
                {
                    throw new BadHttpRequestException(
                        "The supplier has been deleted so you can't change the autorestock or the quantity");
                }
            }

            // If the total of the old and new images are bigger than 5
            if (imagesToUpload.Count + currentImagesCount > 5)
                throw new BadHttpRequestException("Vous pouvez ajouter 5 images au maximum");

            if (imagesToUpload.Count > 0)
            {
                var uploadedImages = await ImageUtils.AddImagesList(imagesToUpload, productToModify.ProductId);
                await _imagesRepository.AddRangeAsync(uploadedImages);
            }

            await _productsRepository.UpdateAsync(productToModify);

            ReturnProductDTO modifiedProductDetails = await _productsRepository.FindProduct(productToModify.ProductId)
                                                      ?? throw new KeyNotFoundException("Product not found");

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