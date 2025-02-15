using System.ComponentModel.DataAnnotations;

namespace MonApi.API.CartLines.DTOs;

public class UpdateSetAsideDto
{
    [Required] public required int CartId { get; set; }
    [Required] public int ProductId { get; set; }
}