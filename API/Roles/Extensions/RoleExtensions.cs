using MonApi.API.Roles.Models;
using MonApi.API.Roles.DTOs;

namespace MonApi.API.Roles.Extensions;

public static class RoleExtensions
{
    public static Role MapToRoleModel(this CreateRoleDTO createRolesDto)
    {
        return new Role()
        {
            Name = createRolesDto.Name
        };
    }

    public static Role MapToRoleModel(this UpdateRoleDTO updateRoleDto)
    {
        return new Role()
        {
            Name = updateRoleDto.Name
        };
    }
}