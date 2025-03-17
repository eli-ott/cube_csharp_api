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

    [Authorize(Roles = "Employee")]
    [HttpDelete("{imageId}")]
    public async Task<ActionResult> DeleteImage(string imageId)
    {
        await _imagesService.DeleteImage(imageId);
        return Ok();
    }
}