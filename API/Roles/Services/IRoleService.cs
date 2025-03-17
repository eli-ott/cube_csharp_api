using MonApi.API.Roles.DTOs;
using MonApi.API.Roles.Filters;
using MonApi.API.Roles.Models;
using MonApi.Shared.Pagination;

namespace MonApi.API.Roles.Services;

public interface IRoleService
{
    public Task<Role> AddRoleAsync(Role role);
    
    public Task<Role> GetRoleByIdAsync(int roleId);
    
    public Task<PagedResult<ReturnRoleDTO>> GetAllRolesAsync(RoleQueryParameters queryParameters);
    
    public Task<Role> UpdateRoleAsync(int roleId, Role role);
    
    public Task<Role> SoftDeleteRoleAsync(int roleId);
    
}