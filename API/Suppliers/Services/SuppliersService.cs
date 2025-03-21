﻿using MonApi.API.Addresses.Extensions;
using MonApi.API.Addresses.Models;
using MonApi.API.Addresses.Repositories;
using MonApi.API.Products.Extensions;
using MonApi.API.Products.Repositories;
using MonApi.API.Reviews.Repositories;
using MonApi.API.Suppliers.DTOs;
using MonApi.API.Suppliers.Extensions;
using MonApi.API.Suppliers.Filters;
using MonApi.API.Suppliers.Repositories;
using MonApi.Shared.Exceptions;
using MonApi.Shared.Pagination;

namespace MonApi.API.Suppliers.Services
{
    public class SuppliersService : ISuppliersService
    {
        private readonly ISuppliersRepository _suppliersRepository;
        private readonly IAddressRepository _addressesRepository;
        private readonly IProductsRepository _productsRepository;

        public SuppliersService(ISuppliersRepository suppliersRepository, IAddressRepository addressRepository,
            IProductsRepository productsRepository)
        {
            _addressesRepository = addressRepository;
            _suppliersRepository = suppliersRepository;
            _productsRepository = productsRepository;
        }

        public async Task<ReturnSupplierDTO> AddAsync(CreateSupplierDTO createSupplierDTO)
        {
            if (await _suppliersRepository.AnyAsync(s => s.Siret == createSupplierDTO.Siret))
                throw new ArgumentException("Supplier already exist");

            var adress = createSupplierDTO.Address.MapToAddressModel();
            var addedAdress = await _addressesRepository.AddAsync(adress);

            var supplier = createSupplierDTO.MapToSupplierModel(addedAdress);

            supplier.Email = supplier.Email.ToLower();

            var newSupplier = await _suppliersRepository.AddAsync(supplier);
            var newSupplierDetails = await _suppliersRepository.FindAsync(newSupplier.SupplierId) ??
                                     throw new KeyNotFoundException("Id not found");


            return newSupplierDetails;
        }

        public async Task<ReturnSupplierDTO> UpdateAsync(int id, UpdateSupplierDTO modifiedSupplier)
        {
            Address address = modifiedSupplier.Address.MapToAddressModel();


            var model = await _suppliersRepository.FindAsync(id) ?? throw new KeyNotFoundException("Id not found");

            if (model.DeletionTime != null) throw new SoftDeletedException("This supplier has been deleted.");

            if (model.Address.AddressId != modifiedSupplier.Address.AddressId)
                throw new Exception("Address does not correspond to supplier address");

            await _addressesRepository.UpdateAsync(address);
            var supplier = modifiedSupplier.MapToSupplierModel(id, address);

            await _suppliersRepository.UpdateAsync(supplier);
            ReturnSupplierDTO newModifiedSupplierDetails = await _suppliersRepository.FindAsync(id) ??
                                                           throw new KeyNotFoundException("Id not found");


            return newModifiedSupplierDetails;
        }


        public async Task<ReturnSupplierDTO> FindById(int id)
        {
            ReturnSupplierDTO supplier = await _suppliersRepository.FindAsync(id) ??
                                         throw new KeyNotFoundException("Id not found");

            if (supplier.DeletionTime != null) throw new SoftDeletedException("This supplier has been deleted.");

            return supplier;
        }


        public async Task<PagedResult<ReturnSupplierDTO>> GetAll(SupplierQueryParameters queryParameters)
        {
            PagedResult<ReturnSupplierDTO> suppliers = await _suppliersRepository.GetAll(queryParameters);
            return suppliers;
        }

        public async Task<ReturnSupplierDTO> SoftDeleteAsync(int id)
        {
            ReturnSupplierDTO supplierDto = await _suppliersRepository.FindAsync(id)
                                            ?? throw new KeyNotFoundException("Id not found");

            if (supplierDto.DeletionTime != null)
                throw new SoftDeletedException("This supplier has been deleted.");

            Address addressToMap = await _addressesRepository.FindAsync(supplierDto.Address.AddressId)
                                   ?? throw new KeyNotFoundException("Address not found");

            var supplierToDelete = supplierDto.MapToSupplierModel(addressToMap);

            //Get the products associated with this supplier
            if (supplierDto.Products != null && supplierDto.Products.Count > 0)
            {
                var mappedProducts = supplierDto.Products.Select(x =>
                {
                    x.Supplier = supplierDto;
                    x.AutoRestock = false;
                    return x.MapToProductModel();
                }).ToList();
                await _productsRepository.UpdateRange(mappedProducts);
            }

            // Set the autorestock to false so that it does not restock when the supplier is deleted

            supplierToDelete.DeletionTime = DateTime.UtcNow;
            supplierDto.DeletionTime = DateTime.UtcNow;
            addressToMap.DeletionTime = DateTime.UtcNow;

            await _suppliersRepository.UpdateAsync(supplierToDelete);
            await _addressesRepository.UpdateAsync(addressToMap);

            return supplierDto;
        }
    }
}