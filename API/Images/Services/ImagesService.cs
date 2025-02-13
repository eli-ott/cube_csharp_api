using System.Text.Json;
using MonApi.API.Images.Extensions;
using MonApi.API.Images.Models;
using MonApi.API.Images.Repositories;
using MonApi.Shared.Utils;

namespace MonApi.API.Images.Services;

public class ImagesService : IImagesService
{
    private readonly IImagesRepository _imagesRepository;

    public ImagesService(IImagesRepository imagesRepository)
    {
        _imagesRepository = imagesRepository;
    }

    public async Task UploadImages(List<IFormFile> images, int productId)
    {
        if (images == null || images.Count == 0) throw new BadHttpRequestException("Aucun fichier envoyé");

        //TODO Check if the product exists

        var uploadedImages = await ImageUtils.AddImagesList(images, productId);

        await _imagesRepository.AddRangeAsync(uploadedImages);
    }

    public async Task DeleteImage(string imageId)
    {
        var foundImage = await _imagesRepository.GetImageByIdAsync(imageId);

        var uploadDir = Environment.GetEnvironmentVariable("UPLOAD_DIR")
                        ?? throw new NullReferenceException("Environment variable UPLOAD_DIR not found");
        var uploadPath = Environment.CurrentDirectory + '/' + uploadDir;

        var imagePath = Path.Combine(uploadPath, imageId);

        var imageExists = File.Exists(imagePath);

        // If the image is neither on the server nor in the DB
        if (foundImage == null && !imageExists)
            throw new NullReferenceException("L'image n'éxiste pas");

        if (foundImage != null)
        {
            await _imagesRepository.DeleteAsync(foundImage.MapToImageModel());
        }

        if (imageExists)
        {
            throw new KeyNotFoundException("L'image n'éxiste pas sur le serveur");
        }
    }
}