using MonApi.API.Suppliers.DTOs;
using MonApi.API.Suppliers.Models;

namespace MonApi.API.Suppliers.Services
{
    public interface ISuppliersService
    {
        public Task<ReturnSupplierDTO> AddAsync(CreateSupplierDTO createSupplierDTO);
        public Task<ReturnSupplierDTO> UpdateAsync(int id, UpdateSupplierDTO modifiedSupplier);
        public Task<ReturnSupplierDTO> FindById(int id);
        public Task<List<Supplier>> GetAll();
        public Task<ReturnSupplierDTO> SoftDeleteAsync(int id);
    }
}
