using MonApi.API.Addresses.Models;
using MonApi.Shared.Data;
using MonApi.Shared.Repositories;

namespace MonApi.API.Addresses.Repositories;

public class AddressRepository : BaseRepository<Address>, IAddressRepository
{
    public AddressRepository(StockManagementContext context) : base(context)
    {
    }
}