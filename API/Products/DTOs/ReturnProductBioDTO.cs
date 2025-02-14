using System.ComponentModel.DataAnnotations;

namespace MonApi.API.Products.DTOs
{
    public class ReturnProductBioDTO
    {
        [Required]
        public required int ProductId { get; set; }
        [Required]
        public required bool IsBio { get; set; }
    }
}
