using MonApi.API.Families.Models;
using MonApi.API.Products.DTOs;
using MonApi.API.Products.Models;
using MonApi.API.Suppliers.DTOs;
using MonApi.Shared.Repositories;

namespace MonApi.API.Products.Repositories
{
    public interface IProductsRepository : IBaseRepository<Product>
    {
        Task<ReturnProductDTO?> FindProduct(int id, CancellationToken cancellationToken = default);
        Task<List<ReturnProductDTO>> GetAll( CancellationToken cancellationToken = default);
    }
}
