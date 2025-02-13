using System.ComponentModel.DataAnnotations;

namespace MonApi.API.Products.DTOs
{
    public class UpdateProductDTO
    {
        [Required] [StringLength(255)] public required string Name { get; set; } = null!;

        [Required] [StringLength(255)] public required string Cuvee { get; set; } = null!;

        [Required] public required int Year { get; set; }

        [Required] [StringLength(255)] public required string ProducerName { get; set; } = null!;

        [Required] public required bool IsBio { get; set; }

        public float? UnitPrice { get; set; }

        public float? CartonPrice { get; set; }

        [Required] public required int Quantity { get; set; }

        [Required] public required bool AutoRestock { get; set; }

        public int AutoRestockTreshold { get; set; }

        [Required] public required int FamilyId { get; set; }

        [Required] public required int SupplierId { get; set; }
        public List<IFormFile>? Images { get; set; }
    }
}