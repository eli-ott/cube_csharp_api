using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using MonApi.API.Reviews.DTOs;
using MonApi.API.Reviews.Models;
using MonApi.Shared.Data;
using MonApi.Shared.Repositories;

namespace MonApi.API.Reviews.Repositories;

public class ReviewRepository : BaseRepository<Review>, IReviewRepository
{
    public ReviewRepository(StockManagementContext context) : base(context)
    {
    }

    public async Task<ReturnReviewDto?> FindAsync(int userId, int productId,
        CancellationToken cancellationToken = default)
    {
        return await (from review in _context.Reviews
            where review.UserId == userId && review.ProductId == productId
            select new ReturnReviewDto
            {
                UserId = review.UserId,
                ProductId = review.ProductId,
                Rating = review.Rating,
                Comment = review.Comment,
                CreationTime = review.CreationTime,
                CustomerFirstName = review.User.FirstName,
                CustomerLastName = review.User.LastName,
                UpdateTime = review.UpdateTime
            }).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<List<ReturnReviewDto>> GetReviewsByProductAsync(int productId,
        CancellationToken cancellationToken = default)
    {
        return await (from review in _context.Reviews
            where review.ProductId == productId
            select new ReturnReviewDto
            {
                UserId = review.UserId,
                ProductId = review.ProductId,
                Rating = review.Rating,
                Comment = review.Comment,
                CreationTime = review.CreationTime,
                CustomerFirstName = review.User.FirstName,
                CustomerLastName = review.User.LastName,
                UpdateTime = review.UpdateTime
            }).ToListAsync(cancellationToken);
    }

    public new async Task UpdateAsync(Review review, CancellationToken cancellationToken = default)
    {
        var reviewFound = _context.Reviews
            .FirstOrDefault(r => r.ProductId == review.ProductId && r.UserId == review.UserId);

        if (reviewFound != null)
        {
            reviewFound.Rating = review.Rating;
            reviewFound.Comment = review.Comment;

            await _context.SaveChangesAsync(cancellationToken); // EF Core tracks changes and updates automatically
        }
    }

    public async Task RemoveRangeAsync(List<Review> reviews, CancellationToken cancellationToken = default)
    {
        _context.Set<Review>().RemoveRange(reviews);
        await _context.SaveChangesAsync(cancellationToken);
    }
}