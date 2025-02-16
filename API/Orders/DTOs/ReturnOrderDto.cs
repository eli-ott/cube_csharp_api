using System.ComponentModel.DataAnnotations;
using MonApi.API.Customers.DTOs;
using MonApi.API.Customers.Models;
using MonApi.API.OrderLines.DTOs;
using MonApi.API.Statuses.DTOs;
using MonApi.API.Statuses.Models;

namespace MonApi.API.Orders.DTOs;

public class ReturnOrderDto
{
    [Required] public required int OrderId { get; set; }
    [Required] public required DateTime? DeliveryDate { get; set; }
    public DateTime? DeletionTime { get; set; }
    [Required] public required DateTime UpdateTime { get; set; }
    [Required] public required DateTime CreationTime { get; set; }
    public ReturnCustomerDto? Customer { get; set; }
    [Required] public required ReturnStatusDto Status { get; set; }
    public List<ReturnOrderLineDto>? Lines { get; set; }
}