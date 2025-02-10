using System.ComponentModel.DataAnnotations;

namespace MonApi.API.Families.DTOs
{
    public class UpdateFamilyDTO
    {
        [Required]
        [StringLength(50)]
        public required string Name { get; set; }

    }
}
