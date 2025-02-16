using System.ComponentModel.DataAnnotations;

namespace MonApi.API.Discounts.DTOs;

public class ReturnDiscountDto
{
    [Required] public required int DiscountId { get; set; }
    [Required] [StringLength(50)] public required string Name { get; set; } = null!;
    [Required] public required int Value { get; set; }
    [Required] public required DateTime? StartDate { get; set; }
    [Required] public required DateTime EndDate { get; set; }
    [Required] public required DateTime CreationTime { get; set; }
    [Required] public required DateTime UpdateTime { get; set; }
    [Required] public required int ProductId { get; set; }
}