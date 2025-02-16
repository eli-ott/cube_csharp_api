using System.ComponentModel.DataAnnotations;

namespace MonApi.API.Statuses.DTOs;

public class ReturnStatusDto
{
    public int StatusId { get; set; }
    [Required] [StringLength(50)] public required string Name { get; set; }
    public DateTime? DeletionTime { get; set; }
}