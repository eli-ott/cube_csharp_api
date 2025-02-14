using System.ComponentModel.DataAnnotations;

namespace MonApi.API.Roles.DTOs;

public class CreateRoleDTO
{
    [Required]
    [StringLength(50)]
    public required string Name { get; set; }
}