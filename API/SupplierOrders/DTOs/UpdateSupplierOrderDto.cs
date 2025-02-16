using System.ComponentModel.DataAnnotations;

namespace MonApi.API.SupplierOrders.DTOs;

public class UpdateSupplierOrderDto
{
    [Required] public required DateTime? DeliveryDate { get; set; }
    [Required] public required int StatusId { get; set; }
}