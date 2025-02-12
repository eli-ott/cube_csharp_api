using MonApi.API.Images.DTOs;

namespace MonApi.API.Images.Services;

public interface IImagesService
{
    Task UploadImages(List<IFormFile> files, int productId);
    Task DeleteImage(string imageId);
}