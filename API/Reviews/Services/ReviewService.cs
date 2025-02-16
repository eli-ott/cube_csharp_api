using MonApi.API.Customers.Repositories;
using MonApi.API.Products.Repositories;
using MonApi.API.Reviews.DTOs;
using MonApi.API.Reviews.Extensions;
using MonApi.API.Reviews.Repositories;
using MonApi.Shared.Exceptions;

namespace MonApi.API.Reviews.Services;

public class ReviewService : IReviewService
{
    private readonly IReviewRepository _reviewRepository;
    private readonly ICustomersRepository _customersRepository;
    private readonly IProductsRepository _productsRepository;

    public ReviewService(IReviewRepository reviewRepository, ICustomersRepository customersRepository,
        IProductsRepository productsRepository)
    {
        _reviewRepository = reviewRepository;
        _customersRepository = customersRepository;
        _productsRepository = productsRepository;
    }

    public async Task<ReturnReviewDto> CreateReview(CreateReviewDto createReviewDto)
    {
        if (createReviewDto.Rating < 0 || createReviewDto.Rating > 5)
            throw new BadHttpRequestException("Rating must be between 0 and 5");

        // Check if the user already added a review for this product
        var reviewForProductExists = await _reviewRepository.AnyAsync(x =>
            x.ProductId == createReviewDto.ProductId && x.UserId == createReviewDto.UserId);
        if (reviewForProductExists)
            throw new BadHttpRequestException("The user has already added a review for this product");

        // Check if the customer and the product exist
        var foundCustomer = await _customersRepository.FindAsync(createReviewDto.UserId)
                            ?? throw new NullReferenceException("Customer not found");
        if (foundCustomer.DeletionTime != null) throw new SoftDeletedException("The customer has been deleted");

        var foundProduct = await _productsRepository.FindAsync(createReviewDto.ProductId)
                           ?? throw new NullReferenceException("Product not found");
        if (foundProduct.DeletionTime != null) throw new SoftDeletedException("The product has been deleted");

        var addedReview = await _reviewRepository.AddAsync(createReviewDto.MapReviewToModel());

        return addedReview.MapToReturnReviewDto();
    }

    public async Task<ReturnReviewDto> UpdateReview(UpdateReviewDto updateReviewDto)
    {
        if (updateReviewDto.Rating < 0 || updateReviewDto.Rating > 5)
            throw new BadHttpRequestException("Rating must be between 0 and 5");

        var foundReview = await _reviewRepository.FindAsync(updateReviewDto.UserId, updateReviewDto.ProductId)
                          ?? throw new NullReferenceException("Review not found");

        // Check if the customer and the product exist
        var foundCustomer = await _customersRepository.FindAsync(updateReviewDto.UserId)
                            ?? throw new NullReferenceException("Customer not found");
        if (foundCustomer.DeletionTime != null) throw new SoftDeletedException("The customer has been deleted");

        var foundProduct = await _productsRepository.FindAsync(updateReviewDto.ProductId)
                           ?? throw new NullReferenceException("Product not found");
        if (foundProduct.DeletionTime != null) throw new SoftDeletedException("The product has been deleted");

        foundReview.Rating = updateReviewDto.Rating;
        foundReview.Comment = updateReviewDto.Comment;

        await _reviewRepository.UpdateAsync(foundReview.MapReviewToModel());

        return foundReview;
    }

    public async Task<ReturnReviewDto> DeleteReview(DeleteReviewDto deleteReviewDto)
    {
        var foundReview = await _reviewRepository.FindAsync(deleteReviewDto.UserId, deleteReviewDto.ProductId)
                          ?? throw new NullReferenceException("Review not found");

        await _reviewRepository.DeleteAsync(foundReview.MapReviewToModel());

        return foundReview;
    }
}