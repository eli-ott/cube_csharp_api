using System.ComponentModel.DataAnnotations;

namespace MonApi.API.Families.DTOs
{
    public class ReturnFamilyDTO
    {
        [Required]
        public required int FamilyId { get; set; }

        [Required]
        [StringLength(50)]
        public required string Name { get; set; }

        
        public DateTime? DeletionTime { get; set; }

    }

}
