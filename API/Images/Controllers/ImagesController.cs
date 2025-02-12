using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using MonApi.API.Images.DTOs;
using MonApi.API.Images.Services;

namespace MonApi.API.Images.Controllers;

[ApiController]
[Authorize]
[Route("images")]
public class ImagesController : ControllerBase
{
    private readonly IImagesService _imagesService;

    public ImagesController(IImagesService imagesService)
    {
        _imagesService = imagesService;
    }

    [HttpPost("upload")]
    public async Task<ActionResult> UploadImages([FromForm] List<IFormFile> files, [FromForm] int productId)
    {
        await _imagesService.UploadImages(files, productId);
        return Ok();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteImage(string imageId)
    {
        await _imagesService.DeleteImage(imageId);
        return Ok();
    }
}