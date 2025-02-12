using System.Text.Json;
using MonApi.API.Images.Extensions;
using MonApi.API.Images.Models;
using MonApi.API.Images.Repositories;
using MonApi.Shared.Utils;

namespace MonApi.API.Images.Services;

public class ImagesService : IImagesService
{
    private readonly IImagesRepository _imagesRepository;
    private static readonly HashSet<string> AllowedExtensions = new() { ".jpg", ".jpeg", ".png", ".webp" };
    private static readonly HashSet<string> AllowedMimeTypes = new() { "image/jpeg", "image/png", "image/webp" };

    public ImagesService(IImagesRepository imagesRepository)
    {
        _imagesRepository = imagesRepository;
    }

    public async Task UploadImages(List<IFormFile> images, int productId)
    {
        if (images == null || images.Count == 0) throw new BadHttpRequestException("Aucun fichier envoyé");

        //TODO Check if the product exists

        var uploadedImages = new List<Image>();

        var uploadDir = Environment.GetEnvironmentVariable("UPLOAD_DIR")
                        ?? throw new KeyNotFoundException("Impossible de trouver le chemin d'upload d'images");
        var uploadPath = Environment.CurrentDirectory + "/" + uploadDir;

        var imageIndex = 0;
        foreach (var image in images)
        {
            imageIndex++;

            // If the image is larger than 5Mo
            if (image.Length > 5000000)
            {
                if (uploadedImages.Count > 0)
                {
                    // Delete the image added in the server if there is an error
                    ImageUtils.DeleteImageList(uploadedImages);
                }

                throw new BadHttpRequestException($"L'image n°{imageIndex} fais plus de 5Mo");
            }

            var guid = Guid.NewGuid();
            var imageExtension = Path.GetExtension(image.FileName).ToLower();
            var imageMimeType = image.ContentType;

            // Check if the images are either in .jpg, .jpeg, .png, .webp
            if (!AllowedExtensions.Contains(imageExtension) || !AllowedMimeTypes.Contains(imageMimeType))
            {
                if (uploadedImages.Count > 0)
                {
                    // Delete the image added in the server if there is an error
                    ImageUtils.DeleteImageList(uploadedImages);
                }

                throw new BadHttpRequestException(
                    "Les images doivent être aux formats suivant : .jpg, .jpeg, .png, .webp");
            }

            var imageName = guid + imageExtension;
            var imagePath = Path.Combine(uploadPath, imageName);

            // Vérifie si le chemin existe
            Directory.CreateDirectory(uploadPath);

            await using (var stream = new FileStream(imagePath, FileMode.Create))
            {
                await image.CopyToAsync(stream);
            }

            var imageUpload = new Image
            {
                ProductId = productId,
                ImageId = guid.ToString(),
                FormatType = imageExtension
            };
            uploadedImages.Add(imageUpload);
        }

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