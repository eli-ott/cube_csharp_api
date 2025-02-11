using MonApi.API.Suppliers.Models;
using MonApi.Shared.Data;
using MonApi.Shared.Repositories;

namespace MonApi.API.Suppliers.Repositories
{
    public class SuppliersRepository : BaseRepository<Supplier>, ISuppliersRepository
    {
        public SuppliersRepository(StockManagementContext context) : base(context)
        {
        }
    }
}
