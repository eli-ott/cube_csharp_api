using MonApi.API.Reviews.DTOs;
using MonApi.API.Reviews.Models;

namespace MonApi.API.Reviews.Extensions;

public static class ReviewExtensions
{
    public static Review MapReviewToModel(this CreateReviewDto reviewDto)
    {
        return new Review
        {
            Rating = reviewDto.Rating,
            Comment = reviewDto.Comment,
            ProductId = reviewDto.ProductId,
            UserId = reviewDto.UserId
        };
    }

    public static Review MapReviewToModel(this ReturnReviewDto reviewDto)
    {
        return new Review
        {
            UserId = reviewDto.UserId,
            ProductId = reviewDto.ProductId,
            Rating = reviewDto.Rating,
            Comment = reviewDto.Comment
        };
    }

    public static ReturnReviewDto MapToReturnReviewDto(this Review review)
    {
        return new ReturnReviewDto
        {
            UserId = review.UserId,
            ProductId = review.ProductId,
            Rating = review.Rating,
            Comment = review.Comment,
            CreationTime = review.CreationTime,
            UpdateTime = review.UpdateTime
        };
    }
}