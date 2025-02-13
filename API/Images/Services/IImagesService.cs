using MonApi.API.Images.DTOs;

namespace MonApi.API.Images.Services;

public interface IImagesService
{
    Task DeleteImage(string imageId);
}