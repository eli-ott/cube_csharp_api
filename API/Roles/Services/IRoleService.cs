using MonApi.API.Roles.Models;

namespace MonApi.API.Roles.Services;

public interface IRoleService
{
    public Task<Role> AddRoleAsync(Role role);
    
    public Task<Role> GetRoleByIdAsync(int roleId);
    
    public Task<List<Role>> GetAllRolesAsync();
    
    public Task<Role> UpdateRoleAsync(int roleId, Role role);
    
    public Task<Role> SoftDeleteRoleAsync(int roleId);
    
}