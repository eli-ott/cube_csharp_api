using MonApi.API.Families.Models;
using MonApi.Shared.Repositories;
using MonApi.Shared.Pagination;
using MonApi.API.Families.Filters;
using MonApi.API.Families.DTOs;

namespace MonApi.API.Families.Repositories
{
    public interface IFamiliesRepository : IBaseRepository<Family>
    {
        Task<PagedResult<ReturnFamilyDTO>> GetPagedFamiliesAsync(FamilyQueryParameters queryParameters);
    }
}
