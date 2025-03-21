﻿using MonApi.API.Addresses.DTOs;
using MonApi.API.Addresses.Models;
using System.ComponentModel.DataAnnotations;
using MonApi.API.Products.DTOs;

namespace MonApi.API.Suppliers.DTOs
{
    public class ReturnSupplierDTO
    {
        [Required] public int SupplierId { get; set; }

        [Required] [StringLength(255)] public required string LastName { get; set; }

        [Required] [StringLength(255)] public required string FirstName { get; set; }

        [Required] [StringLength(255)] public required string Contact { get; set; }


        [Required]
        [StringLength(255)]
        [EmailAddress]
        public required string Email { get; set; }

        [Required] [StringLength(15)] [Phone] public required string Phone { get; set; }

        [Required] [StringLength(255)] public required string Siret { get; set; }

        public DateTime? DeletionTime { get; set; }

        [Required] public required DateTime UpdateTime { get; set; }

        [Required] public required DateTime CreationTime { get; set; }

        [Required] public required ReturnAddressDto Address { get; set; }
        public List<ReturnProductDTO>? Products { get; set; }
    }
}