using MonApi.API.Families.Models;
using MonApi.Shared.Repositories;
using MonApi.Shared.Pagination;
using MonApi.API.Families.Filters;

namespace MonApi.API.Families.Repositories
{
    public interface IFamiliesRepository : IBaseRepository<Family>
    {
        Task<PagedResult<Family>> GetPagedFamiliesAsync(FamilyQueryParameters queryParameters);
    }
}
