using System.ComponentModel.DataAnnotations;

namespace MonApi.API.Products.DTOs
{
    public class ReturnProductRestockDTO
    {
        [Required]
        public required int ProductId { get; set; }
        [Required]
        public required bool AutoRestock { get; set; }
    }
}
