using MonApi.API.Suppliers.DTOs;
using MonApi.API.Suppliers.Models;

namespace MonApi.API.Suppliers.Services
{
    public interface ISuppliersService
    {
        public Task<Supplier> AddAsync(CreateSupplierDTO createSupplierDTO);
        public Task<Supplier> UpdateAsync(int id, Supplier supplier);
        public Task<Supplier> FindById(int id);
        public Task<List<Supplier>> GetAll();
        public Task<Supplier> SoftDeleteAsync(int id);
    }
}
