using MonApi.API.Images.Models;

namespace MonApi.Shared.Utils;

public static class ImageUtils
{
    private static readonly HashSet<string> AllowedExtensions = new() { ".jpg", ".jpeg", ".png", ".webp" };
    private static readonly HashSet<string> AllowedMimeTypes = new() { "image/jpeg", "image/png", "image/webp" };

    public static void DeleteImageList(List<Image> images)
    {
        var uploadDir = Environment.GetEnvironmentVariable("UPLOAD_DIR")
                        ?? throw new NullReferenceException("Environment variable UPLOAD_DIR not found");
        var uploadPath = Environment.CurrentDirectory + '/' + uploadDir;

        foreach (var image in images)
        {
            var imageName = image.ImageId + image.FormatType;

            File.Delete(Path.Combine(uploadPath, imageName));
        }
    }

    public static async Task<List<Image>> AddImagesList(List<IFormFile> images, int productId)
    {
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
                    DeleteImageList(uploadedImages);
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
                    DeleteImageList(uploadedImages);
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

        return uploadedImages;
    }
}