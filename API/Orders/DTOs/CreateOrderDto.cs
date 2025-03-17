using System.ComponentModel.DataAnnotations;
using MonApi.API.OrderLines.DTOs;

namespace MonApi.API.Orders.DTOs;

public class CreateOrderDto
{
    [Required] public required DateTime? DeliveryDate { get; set; }
    [Required] public required int StatusId { get; set; }
    [Required] public required int CustomerId { get; set; }
    [Required] public required List<CreateOrderLineDto> OrderLines { get; set; }
}