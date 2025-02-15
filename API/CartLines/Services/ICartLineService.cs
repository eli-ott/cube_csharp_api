using MonApi.API.CartLines.DTOs;

namespace MonApi.API.CartLines.Services;

public interface ICartLineService
{
    Task<ReturnCartLineDto> UpdateCartLine(UpdateCartLineDto updateCartLineDto);
    Task<ReturnCartLineDto> DeleteCartLine(DeleteCartLineDto cartLineDto);
    Task<ReturnCartLineDto> ToggleIsSetAside(UpdateSetAsideDto updateSetAsideDto);
}