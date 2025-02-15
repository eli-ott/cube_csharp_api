using System.ComponentModel.DataAnnotations;
using MonApi.API.Products.DTOs;

namespace MonApi.API.CartLines.DTOs;

public class ReturnCartLineDto
{
    [Required] public required ReturnProductDTO Product { get; set; }
    [Required] public required int Quantity { get; set; }
    [Required] public required bool IsSetAside { get; set; }
    [Required] public required DateTime? UpdateTime { get; set; }
    [Required] public required DateTime? CreationTime { get; set; }
}