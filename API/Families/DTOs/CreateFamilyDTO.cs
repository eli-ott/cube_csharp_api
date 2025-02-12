using System.ComponentModel.DataAnnotations;

namespace MonApi.API.Families.DTOs
{
    public class CreateFamilyDTO
    {

        [Required]
        [StringLength(50)]
        public required string Name { get; set; }

    }

}
