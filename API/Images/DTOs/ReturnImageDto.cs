using System.ComponentModel.DataAnnotations;

namespace MonApi.API.Images.DTOs;

public class ReturnImageDto
{
    [Required]
    public required string ImageId { get; set; }
    [Required]
    [StringLength(10)]
    public required string FormatType { get; set; }
    [Required]
    public required DateTime UpdateTime { get; set; }
    [Required]
    public required DateTime CreationTime { get; set; }
}