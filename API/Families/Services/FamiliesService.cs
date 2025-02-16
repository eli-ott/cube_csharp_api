using MonApi.API.Families.DTOs;
using MonApi.API.Families.Filters;
using MonApi.API.Families.Models;
using MonApi.API.Families.Repositories;
using MonApi.Shared.Exceptions;
using MonApi.Shared.Pagination;

namespace MonApi.API.Families.Services
{
    public class FamiliesService : IFamiliesService
    {
        private readonly IFamiliesRepository _familiesRepository;
        public FamiliesService(IFamiliesRepository familiesRepository)
        {
            _familiesRepository = familiesRepository;
        }

        public async Task<Family> AddAsync(Family family)
        {
            return await _familiesRepository.AddAsync(family);
        }

        public async Task<Family> UpdateAsync(int id, Family renamedfamily)
        {
            var family = await _familiesRepository.FindAsync(id);
            if (family == null) throw new NullReferenceException("Family does not exist");
            if (family.DeletionTime != null) throw new SoftDeletedException("This family has been deleted.");

            family.Name = renamedfamily.Name;

            await _familiesRepository.UpdateAsync(family);
            return family;
        }


        public async Task<Family> FindById(int id)
        {
            Family family = await _familiesRepository.FindAsync(id) ?? throw new KeyNotFoundException("Id not found");

            if (family.DeletionTime != null) throw new SoftDeletedException("This family has been deleted.");

            return family;
        }

        public async Task<PagedResult<ReturnFamilyDTO>> GetAll(FamilyQueryParameters familyQueryParameters)
        {
            PagedResult<ReturnFamilyDTO> families = await _familiesRepository.GetPagedFamiliesAsync(familyQueryParameters);
            return families;
        }


        public async Task<Family> SoftDeleteAsync(int id)
        {
            Family model = await _familiesRepository.FindAsync(id) ?? throw new KeyNotFoundException("Id not found");

            if (model.DeletionTime != null) throw new SoftDeletedException("This customer has been deleted already.");

            model.DeletionTime = DateTime.UtcNow;
            await _familiesRepository.UpdateAsync(model);
            return model;
        }
    }
}
