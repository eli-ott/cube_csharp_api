using MonApi.API.Addresses.Extensions;
using MonApi.API.Addresses.Repositories;
using MonApi.API.Suppliers.DTOs;
using MonApi.API.Suppliers.Extensions;
using MonApi.API.Suppliers.Models;
using MonApi.API.Suppliers.Repositories;

namespace MonApi.API.Suppliers.Services
{
    public class SuppliersService : ISuppliersService
    {
        private readonly ISuppliersRepository _suppliersRepository;
        private readonly IAddressRepository _addressesRepository;

        public SuppliersService(ISuppliersRepository suppliersRepository, IAddressRepository addressRepository)
        {
            _addressesRepository = addressRepository;
            _suppliersRepository = suppliersRepository;
        }

        public async Task<Supplier> AddAsync(CreateSupplierDTO createSupplierDTO)
        {
            if (await _suppliersRepository.AnyAsync(s => s.Siret == createSupplierDTO.Siret)) throw new ArgumentException("Supplier already exist");

            var adress = createSupplierDTO.Address.MapToAddressModel();
            var addedAdress = await _addressesRepository.AddAsync(adress);

            var supplier = createSupplierDTO.MapToSupplierModel(addedAdress);

            var newSupplier = await _suppliersRepository.AddAsync(supplier);

            return newSupplier;
        }

        public async Task<Supplier> UpdateAsync(int id, Supplier modifiedSupplier)
        {
            modifiedSupplier.SupplierId = id;
            Supplier model = await _suppliersRepository.FindAsync(id) ?? throw new KeyNotFoundException("Id not found");

            if (model.DeletionTime != null) throw new Exception("Supplier deleted");

            await _suppliersRepository.UpdateAsync(modifiedSupplier);

            return modifiedSupplier;
        }

        public async Task<Supplier> FindById(int id)
        {
            Supplier supplier = await _suppliersRepository.FindAsync(id) ?? throw new KeyNotFoundException("Id not found");

            if (supplier.DeletionTime != null) throw new Exception("Supplier deleted");

            return supplier;
        }


        public async Task<List<Supplier>> GetAll()
        {
            List<Supplier> suppliers = await _suppliersRepository.ListAsync();
            return suppliers;
        }

        public async Task<Supplier> SoftDeleteAsync(int id)
        {
            Supplier model = await _suppliersRepository.FindAsync(id) ?? throw new KeyNotFoundException("Id not found");
            if (model.DeletionTime != null) throw new Exception("Supplier already deleted");

            model.DeletionTime = DateTime.UtcNow;
            await _suppliersRepository.UpdateAsync(model);
            return model;
        }



    }
}
