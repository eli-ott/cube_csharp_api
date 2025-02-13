using MonApi.API.Families.DTOs;
using MonApi.API.Families.Filters;
using MonApi.API.Families.Models;
using MonApi.Shared.Pagination;

namespace MonApi.API.Families.Services
{
    public interface IFamiliesService
    {
        public Task<Family> AddAsync(Family family);
        public Task<Family> UpdateAsync(int id, Family family);
        public Task<Family> FindById(int id);
        public Task<PagedResult<ReturnFamilyDTO>> GetAll(FamilyQueryParameters familyQueryParameters);
        public Task<Family> SoftDeleteAsync(int id);
    }
}
