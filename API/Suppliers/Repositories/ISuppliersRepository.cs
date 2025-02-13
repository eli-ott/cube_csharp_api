using MonApi.API.Suppliers.DTOs;
using MonApi.API.Suppliers.Filters;
using MonApi.API.Suppliers.Models;
using MonApi.Shared.Pagination;
using MonApi.Shared.Repositories;

namespace MonApi.API.Suppliers.Repositories
{
    public interface ISuppliersRepository : IBaseRepository<Supplier>
    {
        Task<ReturnSupplierDTO?> FindAsync(int id, CancellationToken cancellationToken = default);
        Task<PagedResult<ReturnSupplierDTO>> GetAll(SupplierQueryParameters queryParameters, CancellationToken cancellationToken = default);
    }
}
