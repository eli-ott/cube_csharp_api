﻿using MonApi.API.Families.Models;
using MonApi.API.Suppliers.DTOs;
using MonApi.API.Suppliers.Models;
using System.ComponentModel.DataAnnotations;

namespace MonApi.API.Products.DTOs
{
    public class CreateProductDTO
    {
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
        public string Description { get; set; } = null!;

        [Required]
        public required bool IsBio { get; set; }

        public float? UnitPrice { get; set; }

        public float? BoxPrice { get; set; }

        [Required]
        public required int Quantity { get; set; }

        [Required]
        public required bool AutoRestock { get; set; }

        public int? AutoRestockTreshold { get; set; }

        [Required]
        public required int FamilyId { get; set; }

        [Required]
        public required int SupplierId { get; set; }
        public List<IFormFile>? Images { get; set; }
    }
}
