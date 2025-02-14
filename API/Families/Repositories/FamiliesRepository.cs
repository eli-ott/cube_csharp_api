using MonApi.Shared.Repositories;
using MonApi.API.Families.Models;
using MonApi.Shared.Data;
using MonApi.Shared.Pagination;
using MonApi.API.Families.Filters;
using Microsoft.EntityFrameworkCore;
using MonApi.API.Families.DTOs;

namespace MonApi.API.Families.Repositories
{
    public class FamiliesRepository : BaseRepository<Family>, IFamiliesRepository
    {
        public FamiliesRepository(StockManagementContext context) : base(context)
        {
        }

        public async Task<PagedResult<ReturnFamilyDTO>> GetPagedFamiliesAsync(FamilyQueryParameters queryParameters)
        {
            IQueryable<ReturnFamilyDTO> query = from family in _context.Families select new ReturnFamilyDTO { FamilyId = family.FamilyId, Name = family.Name, DeletionTime = family.DeletionTime }; ;

            if(queryParameters.deleted == "only")
            {
                query = query.Where(f => f.DeletionTime != null);
            }
            else if (queryParameters.deleted == "all")
            {
            }
            else
            // Default to only returning undeleted items
            {
                query = query.Where(f => f.DeletionTime == null);
            }

            // Get the total count (before pagination)
            var totalCount = await query.CountAsync();

            // Get the items for the requested page
            var families = await query
                .Skip((queryParameters.page - 1) * queryParameters.size)
                .Take(queryParameters.size)
                .ToListAsync();

            // Return the paginated result
            return new PagedResult<ReturnFamilyDTO>
            {
                Items = families,
                CurrentPage = queryParameters.page,
                PageSize = queryParameters.size,
                TotalCount = totalCount
            };
        }
    }
}
