using MonApi.API.Reviews.DTOs;
using MonApi.API.Reviews.Models;
using MonApi.Shared.Repositories;

namespace MonApi.API.Reviews.Repositories;

public interface IReviewRepository : IBaseRepository<Review>
{
    Task<ReturnReviewDto?> FindAsync(int userId, int productId, CancellationToken cancellationToken = default);

    Task<List<ReturnReviewDto>> GetReviewsByProductAsync(int productId, CancellationToken cancellationToken = default);

    new Task UpdateAsync(Review review, CancellationToken cancellationToken = default);
    Task RemoveRangeAsync(List<Review> reviews, CancellationToken cancellationToken = default);
}