using MonApi.API.Families.Models;
using MonApi.API.Products.DTOs;
using MonApi.API.Products.Filters;
using MonApi.API.Products.Models;
using MonApi.API.Suppliers.DTOs;
using MonApi.Shared.Pagination;
using MonApi.Shared.Repositories;

namespace MonApi.API.Products.Repositories
{
    public interface IProductsRepository : IBaseRepository<Product>
    {
        Task<ReturnProductDTO?> FindProduct(int id, CancellationToken cancellationToken = default);
        Task<PagedResult<ReturnProductDTO>> GetAll(ProductQueryParameters queryParameters, CancellationToken cancellationToken = default);
        Task UpdateRange(List<Product> products, CancellationToken cancellationToken = default);
    }
}
