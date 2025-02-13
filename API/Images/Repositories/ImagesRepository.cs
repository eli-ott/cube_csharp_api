using Microsoft.EntityFrameworkCore;
using MonApi.API.Images.DTOs;
using MonApi.API.Images.Models;
using MonApi.Shared.Data;
using MonApi.Shared.Repositories;

namespace MonApi.API.Images.Repositories;

public class ImagesRepository : BaseRepository<Image>, IImagesRepository
{
    private readonly string _apiPath;
    public ImagesRepository(StockManagementContext context) : base(context)
    {
        var apiUrl = Environment.GetEnvironmentVariable("URL_API")
                     ?? throw new NullReferenceException("URL_API is null");
        var uploadDir = Environment.GetEnvironmentVariable("UPLOAD_DIR")
                        ?? throw new NullReferenceException("UPLOAD_DIR is null");
            
        _apiPath = apiUrl + uploadDir;
    }

    public async Task AddRangeAsync(List<Image> images, CancellationToken cancellationToken = default)
    {
        await _context.Set<Image>().AddRangeAsync(images, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task RemoveRangeAsync(List<Image> images, CancellationToken cancellationToken = default)
    {
        _context.Set<Image>().RemoveRange(images);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<List<ReturnImageDto>> GetImagesByProductIdAsync(int productId,
        CancellationToken cancellationToken = default)
    {
        return await (from image in _context.Images
            where image.ProductId == productId
            select new ReturnImageDto
            {
                ImageId = image.ImageId,
                FormatType = image.FormatType,
                ImageUrl = _apiPath + image.ImageId + image.FormatType,
                UpdateTime = image.UpdateTime,
                CreationTime = image.CreationTime
            }).ToListAsync(cancellationToken);
    }

    public async Task<ReturnImageDto?> GetImageByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        return await (from image in _context.Images
            where image.ImageId == id
            select new ReturnImageDto
            {
                ImageId = image.ImageId,
                FormatType = image.FormatType,
                ImageUrl = _apiPath + image.ImageId + image.FormatType,
                UpdateTime = image.UpdateTime,
                CreationTime = image.CreationTime
            }).FirstOrDefaultAsync(cancellationToken);
    }
}