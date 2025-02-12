using MonApi.API.Images.Models;

namespace MonApi.Shared.Utils;

public static class ImageUtils
{
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
}