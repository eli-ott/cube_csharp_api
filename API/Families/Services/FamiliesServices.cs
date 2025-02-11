using MonApi.API.Families.Models;
using MonApi.API.Families.Repositories;

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

        public async Task<Family> UpdateAsync(int id, Family renamedFamily)
        {
            renamedFamily.FamilyId = id;
            Family model = await _familiesRepository.FindAsync(id) ?? throw new KeyNotFoundException("Id not found");

            if (model.DeletionTime != null) throw new Exception("Family deleted");

            await _familiesRepository.UpdateAsync(renamedFamily);
            return renamedFamily;
        }

        public async Task<Family> FindById(int id)
        {
            Family family = await _familiesRepository.FindAsync(id) ?? throw new KeyNotFoundException("Id not found");

            if (family.DeletionTime != null) throw new Exception("Family deleted");

            return family;
        }

        public async Task<List<Family>> GetAll()
        {
            List<Family> families = await _familiesRepository.ListAsync();


            return families;
        }


        public async Task<Family> SoftDeleteAsync(int id)
        {
            Family model = await _familiesRepository.FindAsync(id) ?? throw new KeyNotFoundException("Id not found");

            if (model.DeletionTime != null) throw new Exception("Family already deleted");

            model.DeletionTime = DateTime.UtcNow;
            await _familiesRepository.UpdateAsync(model);
            return model;
        }
    }
}
