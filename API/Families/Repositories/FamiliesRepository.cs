using MonApi.Shared.Repositories;
using MonApi.API.Families.Models;
using MonApi.Shared.Data;

namespace MonApi.API.Families.Repositories
{
    public class FamiliesRepository : BaseRepository<Family>, IFamiliesRepository
    {
        public FamiliesRepository(StockManagementContext context) : base(context)
        {
        }
    }
}
