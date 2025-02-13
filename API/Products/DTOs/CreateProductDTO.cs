using MonApi.API.Families.Models;
using MonApi.API.Suppliers.DTOs;
using MonApi.API.Suppliers.Models;
using MonApi.Models;
using System.ComponentModel.DataAnnotations;

namespace MonApi.API.Products.DTOs
{
    public class CreateProductDTO
    {
        [Required]
        [StringLength(255)]
        public string Name { get; set; } = null!;

        [Required]
        [StringLength(255)]
        public string Cuvee { get; set; } = null!;

        [Required]
        public int Year { get; set; }

        [Required]
        [StringLength(255)]
        public string ProducerName { get; set; } = null!;

        [Required]
        public bool IsBio { get; set; }

        public float? UnitPrice { get; set; } //pas obligatoire mais doit avoir au moins un des deux type de prix

        public float? CartonPrice { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public bool AutoRestock { get; set; }

        public int AutoRestockTreshold { get; set; } //optionnel seulement si l'auto restock est à false (quantité mini pour restock automatiquement)

        [Required]
        public int FamilyId { get; set; }

        [Required]
        public int SupplierId { get; set; }
    }
}
