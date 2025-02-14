using System.ComponentModel.DataAnnotations;

namespace MonApi.API.Roles.DTOs;

public class UpdateRoleDTO
{
    [Required]
    [StringLength(50)]
    public required string Name { get; set; }
}