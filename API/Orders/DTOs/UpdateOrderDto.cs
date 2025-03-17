using System.ComponentModel.DataAnnotations;

namespace MonApi.API.Orders.DTOs;

public class UpdateOrderDto
{
    [Required] public required DateTime? DeliveryDate { get; set; }
    [Required] public required int StatusId { get; set; }
}