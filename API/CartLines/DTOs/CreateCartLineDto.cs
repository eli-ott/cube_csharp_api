using System.ComponentModel.DataAnnotations;

namespace MonApi.API.CartLines.DTOs;

public class CreateCartLineDto
{
    [Required] public required int ProductId { get; set; }
    [Required] public required int Quantity { get; set; }
}