using MonApi.API.Reviews.DTOs;

namespace MonApi.API.Reviews.Services;

public interface IReviewService
{
    Task<ReturnReviewDto> CreateReview(CreateReviewDto createReviewDto);
    Task<ReturnReviewDto> UpdateReview(UpdateReviewDto updateReviewDto);
    Task<ReturnReviewDto> DeleteReview(DeleteReviewDto deleteReviewDto);
}