using MonApi.API.Roles.DTOs;
using MonApi.API.Roles.Filters;
using MonApi.API.Roles.Models;
using MonApi.API.Roles.Repositories;
using MonApi.Shared.Exceptions;
using MonApi.Shared.Pagination;

namespace MonApi.API.Roles.Services;

public class RoleService : IRoleService
{
    private readonly IRoleRepository _roleRepository;
    
    public RoleService(IRoleRepository roleRepository)
    {
        _roleRepository = roleRepository;
    }

    public async Task<Role> AddRoleAsync(Role role)
    {
        return await _roleRepository.AddAsync(role);
    }

    public async Task<PagedResult<ReturnRoleDTO>> GetAllRolesAsync(RoleQueryParameters queryParameters)
    {
        PagedResult<ReturnRoleDTO> roles = await _roleRepository.GetAll(queryParameters);
        return roles;
    }

    public async Task<Role> GetRoleByIdAsync(int roleId)
    {
        Role role = await _roleRepository.FindAsync(roleId) ?? throw new KeyNotFoundException("Role not found");
        
        if (role.DeletionTime != null) throw new SoftDeletedException("This role has been deleted.");

        return role;
    }

    public async Task<Role> UpdateRoleAsync(int id, Role newRole)
    {
        Role role = await _roleRepository.FindAsync(id) ?? throw new KeyNotFoundException("Id not found");

        if (role.DeletionTime != null) throw new SoftDeletedException("This role has been deleted.");

        role.Name = newRole.Name;
        
        await _roleRepository.UpdateAsync(role);

        return role;
    }

    public async Task<Role> SoftDeleteRoleAsync(int roleId)
    {
        Role role = await _roleRepository.FindAsync(roleId) ?? throw new KeyNotFoundException("Id not found");

        if (role.DeletionTime != null) throw new SoftDeletedException("This role has been deleted already.");

        role.DeletionTime = DateTime.UtcNow;
        await _roleRepository.UpdateAsync(role);
        return role;
    }
    
}