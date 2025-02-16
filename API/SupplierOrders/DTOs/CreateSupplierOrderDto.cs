using System.ComponentModel.DataAnnotations;
using MonApi.API.SupplierOrderLines.DTOs;

namespace MonApi.API.SupplierOrders.DTOs;

public class CreateSupplierOrderDto
{
    [Required] public required DateTime? DeliveryDate { get; set; }
    [Required] public required int StatusId { get; set; }
    [Required] public required int EmployeeId { get; set; }
    [Required] public required List<CreateSupplierOrderLineDto> OrderLines { get; set; }
}