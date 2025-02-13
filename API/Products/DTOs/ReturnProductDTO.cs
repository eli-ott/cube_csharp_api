using MonApi.API.Families.Models;
using MonApi.API.Suppliers.DTOs;
using MonApi.API.Suppliers.Models;
using System.ComponentModel.DataAnnotations;
using MonApi.API.Images.DTOs;

namespace MonApi.API.Products.DTOs
{
    public class ReturnProductDTO
    {
        [Required]
        public required int ProductId { get; set; }

        [Required]
        [StringLength(255)]
        public required string Name { get; set; } = null!;

        [Required]
        [StringLength(255)]
        public required string Cuvee { get; set; } = null!;

        [Required]
        public required int Year { get; set; }

        [Required]
        [StringLength(255)]
        public required string ProducerName { get; set; } = null!;

        [Required]
        public required bool IsBio { get; set; }

        public float? UnitPrice { get; set; } //pas obligatoire mais doit avoir au moins un des deux type de prix

        public float? CartonPrice { get; set; }

        [Required]
        public required int Quantity { get; set; }

        public bool AutoRestock { get; set; }

        public int AutoRestockTreshold { get; set; } //optionnel seulement si l'auto restock est à false (quantité mini pour restock automatiquement)

        public DateTime? DeletionTime { get; set; }

        public DateTime UpdateTime { get; set; }

        public DateTime CreationTime { get; set; }

        [Required]
        public required Family Family { get; set; } = null!;

        [Required]
        public required ReturnSupplierDTO Supplier { get; set; }
        [Required]
        public required List<ReturnImageDto> Images { get; set; }
    }
}
