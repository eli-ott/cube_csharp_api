using MonApi.API.Images.DTOs;
using MonApi.API.Images.Models;
using MonApi.Shared.Repositories;

namespace MonApi.API.Images.Repositories;

public interface IImagesRepository : IBaseRepository<Image>
{
    Task AddRangeAsync(List<Image> images, CancellationToken cancellationToken = default);
    Task RemoveRangeAsync(List<Image> images, CancellationToken cancellationToken = default);
    Task<List<ReturnImageDto>> GetImagesByProductIdAsync(int productId, CancellationToken cancellationToken = default);
    Task<ReturnImageDto?> GetImageByIdAsync(string imageId, CancellationToken cancellationToken = default);
}