using MonApi.Shared.Repositories;
using MonApi.API.Families.Models;
using MonApi.Shared.Data;
using Microsoft.EntityFrameworkCore;
using MonApi.API.Families.Repositories;
using MonApi.Models;

namespace MonApi.API.Customers.Repositories
{
    public class FamiliesRepository : BaseRepository<Family>, IFamiliesRepository
    {
        public FamiliesRepository(StockManagementContext context) : base(context)
        {
        }
    }
}
