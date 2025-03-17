using System.ComponentModel.DataAnnotations;

namespace MonApi.API.Roles.DTOs;

public class UpdateRoleDTO
{
    [Key]
    public required int RoleId { get; set; }
    [Required]
    [StringLength(50)]
    public required string Name { get; set; }
}