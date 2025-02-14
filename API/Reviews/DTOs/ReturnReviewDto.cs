using System.ComponentModel.DataAnnotations;

namespace MonApi.API.Reviews.DTOs;

public class ReturnReviewDto
{
    [Required] public required int UserId { get; set; }
    [Required] public required int ProductId { get; set; }
    [Required] public required float Rating { get; set; }
    public string? Comment { get; set; }
    public string? CustomerFirstName { get; set; }
    public string? CustomerLastName { get; set; }
    [Required] public required DateTime? UpdateTime { get; set; }
    [Required] public required DateTime CreationTime { get; set; }
}