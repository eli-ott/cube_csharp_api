using System.ComponentModel.DataAnnotations;
using MonApi.API.Products.DTOs;

namespace MonApi.API.SupplierOrderLines.DTOs;

public class ReturnSupplierOrderLineDto
{
    [Required] public required int OrderId { get; set; }
    public int? ProductId { get; set; }
    [Required] public required ReturnProductDTO Product { get; set; }
    [Required] public required int Quantity { get; set; }
    [Required] public required float UnitPrice { get; set; }
    [Required] public required DateTime UpdateTime { get; set; }
    [Required] public required DateTime CreationTime { get; set; }
    public DateTime? DeletionTime { get; set; }
}