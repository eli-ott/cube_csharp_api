using System.ComponentModel.DataAnnotations;

namespace MonApi.API.SupplierOrderLines.DTOs;

public class CreateSupplierOrderLineDto
{
    [Required] public required int ProductId { get; set; }
    [Required] public required int Quantity { get; set; }
    [Required] public required float UnitPrice { get; set; }
}