using Microsoft.EntityFrameworkCore;
using MonApi.API.Images.DTOs;
using MonApi.API.Images.Models;
using MonApi.Shared.Data;
using MonApi.Shared.Repositories;

namespace MonApi.API.Images.Repositories;

public class ImagesRepository : BaseRepository<Image>, IImagesRepository
{
    public ImagesRepository(StockManagementContext context) : base(context)
    {
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
                UpdateTime = image.UpdateTime,
                CreationTime = image.CreationTime
            }).FirstOrDefaultAsync(cancellationToken);
    }
}