using System.ComponentModel.DataAnnotations;

namespace MonApi.API.CartLines.DTOs;

public class UpdateCartLineDto
{
    [Required] public required int CartId { get; set; }
    [Required] public int ProductId { get; set; }
    [Required] public required int Quantity { get; set; }
}