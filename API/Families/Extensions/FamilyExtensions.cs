using MonApi.API.Families.Models;
using MonApi.API.Families.DTOs;
using MonApi.Shared.Data;

namespace MonApi.API.Families.Extensions
{
    public static class FamilyExtensions
    {
        public static Family MapToFamilyModel(this CreateFamilyDTO createFamiliesDTO)
        {
            return new Family()
            {
                Name = createFamiliesDTO.Name
            };
        }

        public static Family MapToFamilyModel(this UpdateFamilyDTO updateFamilyDTO)
        {
            return new Family()
            {
                Name = updateFamilyDTO.Name
            };
        }

        public static ReturnFamilyDTO MapToReturnDTO(this Family family)
        {
            return new ReturnFamilyDTO()
            {
                FamilyId = family.FamilyId,
                Name = family.Name,
                DeletionTime = family.DeletionTime,
            };
        }
    }
}
