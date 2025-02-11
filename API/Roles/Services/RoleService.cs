using MonApi.API.Roles.Models;
using MonApi.API.Roles.Repositories;

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

    public async Task<List<Role>> GetAllRolesAsync()
    {
        List<Role> roles = await _roleRepository.ListAsync();
        return roles;
    }

    public async Task<Role> GetRoleByIdAsync(int roleId)
    {
        Role role = await _roleRepository.FindAsync(roleId) ?? throw new Exception("Role not found");
        
        if (role.DeletionTime != null) throw new Exception("Role deleted");

        return role;
    }

    public async Task<Role> UpdateRoleAsync(int id, Role newRole)
    {
        Role role = await _roleRepository.FindAsync(id) ?? throw new KeyNotFoundException("Id not found");

        if (role.DeletionTime != null) throw new Exception("Role deleted");
        
        role.Name = newRole.Name;
        
        await _roleRepository.UpdateAsync(role);

        return role;
    }

    public async Task<Role> SoftDeleteRoleAsync(int roleId)
    {
        Role role = await _roleRepository.FindAsync(roleId) ?? throw new KeyNotFoundException("Id not found");

        if (role.DeletionTime != null) throw new Exception("Role already deleted");

        role.DeletionTime = DateTime.UtcNow;
        await _roleRepository.UpdateAsync(role);
        return role;
    }
    
}