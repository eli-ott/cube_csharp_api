using System.ComponentModel.DataAnnotations;
using MonApi.API.Employees.DTOs;
using MonApi.API.Statuses.DTOs;
using MonApi.API.SupplierOrderLines.DTOs;

namespace MonApi.API.SupplierOrders.DTOs;

public class ReturnSupplierOrderDto
{
    [Required] public required int OrderId { get; set; }
    [Required] public required DateTime? DeliveryDate { get; set; }
    public DateTime? DeletionTime { get; set; }
    [Required] public required DateTime CreationTime { get; set; }
    [Required] public required DateTime UpdateTime { get; set; }
    [Required] public required ReturnEmployeeDto Employee { get; set; } // TODO Wait for the employee crud
    [Required] public required ReturnStatusDto Status { get; set; }
    public List<ReturnSupplierOrderLineDto>? Lines { get; set; }
}