using System.ComponentModel.DataAnnotations;

namespace MonApi.API.Reviews.DTOs;

public class DeleteReviewDto
{
    [Required] public required int UserId { get; set; }
    [Required] public required int ProductId { get; set; }
}