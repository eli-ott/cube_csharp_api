using System.ComponentModel.DataAnnotations;
using MonApi.API.CartLines.DTOs;

namespace MonApi.API.Carts.DTOs;

public class ReturnCartDto
{
    [Required] public required int CartId { get; set; }
    [Required] public required DateTime? UpdateTime { get; set; }
    [Required] public required DateTime? CreationTime { get; set; }
    [Required] public required List<ReturnCartLineDto> CartLines { get; set; }
}