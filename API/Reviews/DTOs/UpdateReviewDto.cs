using System.ComponentModel.DataAnnotations;

namespace MonApi.API.Reviews.DTOs;

public class UpdateReviewDto
{
    [Required] public required int UserId { get; set; }
    [Required] public required int ProductId { get; set; }
    [Required] public required float Rating { get; set; }
    public string? Comment { get; set; }
}