using MonApi.API.Products.DTOs;
using MonApi.API.Products.Models;

namespace MonApi.API.Products.Services
{
    public interface IProductService
    {
        Task<ReturnProductDTO> AddAsync(CreateProductDTO product);
        Task<List<ReturnProductDTO>> GetAll();
        Task<ReturnProductDTO> GetById(int id);
        Task<ReturnProductDTO> SoftDeleteAsync(int id);
        Task<Product> UpdateAsync(Product product);
    }
}
