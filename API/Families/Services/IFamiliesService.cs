using MonApi.API.Families.Models;

namespace MonApi.API.Families.Services
{
    public interface IFamiliesService
    {
        public Task<Family> AddAsync(Family family);
        public Task<Family> UpdateAsync(int id, Family family);
        public Task<Family> FindById(int id);
        public Task<List<Family>> GetAll();
        public Task<Family> SoftDeleteAsync(int id);
    }
}
