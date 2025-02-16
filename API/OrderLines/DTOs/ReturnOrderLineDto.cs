using System.ComponentModel.DataAnnotations;
using MonApi.API.Customers.Models;
using MonApi.API.Products.DTOs;
using MonApi.API.Products.Models;

namespace MonApi.API.OrderLines.DTOs;

public class ReturnOrderLineDto
{
    [Required] public required int Quantity { get; set; }
    [Required] public required float UnitPrice { get; set; }
    [Required] public required DateTime UpdateTime { get; set; }
    [Required] public required DateTime CreationTime { get; set; }
    public DateTime? DeletionTime { get; set; }
    [Required] public required ReturnProductDTO Product { get; set; }
}