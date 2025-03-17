using System.ComponentModel.DataAnnotations;

namespace MonApi.API.Discounts.DTOs;

public class CreateDiscountDto
{
    [Required] [StringLength(50)] public required string Name { get; set; } = null!;
    [Required] public required int Value { get; set; }
    public DateTime? StartDate { get; set; }
    [Required] public required DateTime EndDate { get; set; }
    [Required] public required int ProductId { get; set; }
}