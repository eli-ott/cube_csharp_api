using MonApi.API.Roles.DTOs;
using MonApi.API.Roles.Filters;
using MonApi.API.Roles.Models;
using MonApi.Shared.Pagination;
using MonApi.Shared.Repositories;


namespace MonApi.API.Roles.Repositories;

public interface IRoleRepository : IBaseRepository<Role>
{
    Task<PagedResult<ReturnRoleDTO>> GetAll(RoleQueryParameters queryParameters, CancellationToken cancellationToken = default);
}