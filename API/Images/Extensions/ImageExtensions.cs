using MonApi.API.Images.DTOs;
using MonApi.API.Images.Models;

namespace MonApi.API.Images.Extensions;

public static class ImageExtensions
{
    public static Image MapToImageModel(this ReturnImageDto imageDto)
    {
        return new Image
        {
            ImageId = imageDto.ImageId,
            FormatType = imageDto.FormatType
        };
    }
}