using MonApi.API.Products.DTOs;
using MonApi.API.Products.Filters;
using MonApi.API.Products.Models;
using MonApi.Shared.Pagination;

namespace MonApi.API.Products.Services
{
    public interface IProductService
    {
        Task<ReturnProductDTO> AddAsync(CreateProductDTO product);
        Task<PagedResult<ReturnProductDTO>> GetAll(ProductQueryParameters queryParameters);
        Task<ReturnProductDTO> GetById(int id);
        Task<ReturnProductDTO> SoftDeleteAsync(int id);
        Task<ReturnProductDTO> UpdateAsync(int id, UpdateProductDTO updatedProduct);
        Task<ReturnProductRestockDTO> ToggleRestock(int id);
        Task<ReturnProductBioDTO> ToggleIsBio(int id);
    }
}
