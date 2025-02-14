using MonApi.API.Families.Models;
using MonApi.API.Products.DTOs;
using MonApi.API.Products.Models;
using MonApi.API.Suppliers.DTOs;
using MonApi.API.Suppliers.Extensions;
using MonApi.API.Suppliers.Models;

namespace MonApi.API.Products.Extensions
{
    public static class ProductExtensions
    {
        public static Product MapToProductModel(this CreateProductDTO product)
        {
            return new Product()
            {
                Name = product.Name,
                Cuvee = product.Cuvee,
                Year = product.Year,
                ProducerName = product.ProducerName,
                IsBio = product.IsBio,
                UnitPrice = product.UnitPrice,
                CartonPrice = product.CartonPrice,
                Quantity = product.Quantity,
                AutoRestock = product.AutoRestock,
                AutoRestockTreshold = product.AutoRestockTreshold ?? 0,
                FamilyId = product.FamilyId,
                SupplierId = product.SupplierId
            };
        }

        public static Product MapToProductModel(this ReturnProductDTO product)
        {
            return new Product()
            {
                ProductId = product.ProductId,
                Name = product.Name,
                Cuvee = product.Cuvee,
                Year = product.Year,
                ProducerName = product.ProducerName,
                IsBio = product.IsBio,
                UnitPrice = product.UnitPrice,
                CartonPrice = product.CartonPrice,
                Quantity = product.Quantity,
                AutoRestock = product.AutoRestock,
                AutoRestockTreshold = product.AutoRestockTreshold,
                FamilyId = product.Family.FamilyId,
                SupplierId = product.Supplier.SupplierId,
                DeletionTime = product.DeletionTime
            };
        }

        public static Product MapToProductModel(this UpdateProductDTO product, int id)
        {
            return new Product()
            {
                ProductId = id,
                Name = product.Name,
                Cuvee = product.Cuvee,
                Year = product.Year,
                ProducerName = product.ProducerName,
                IsBio = product.IsBio,
                UnitPrice = product.UnitPrice,
                CartonPrice = product.CartonPrice,
                Quantity = product.Quantity,
                AutoRestock = product.AutoRestock,
                AutoRestockTreshold = product.AutoRestockTreshold,
                FamilyId = product.FamilyId,
                SupplierId = product.SupplierId
            };
        }

        public static ReturnProductRestockDTO MapToRestockDTO(this Product product)
        {
            return new ReturnProductRestockDTO()
            {
                ProductId = product.ProductId,
                AutoRestock = product.AutoRestock
            };
        }

        public static ReturnProductBioDTO MapToBioDTO(this Product product)
        {
            return new ReturnProductBioDTO()
            {
                ProductId = product.ProductId,
                IsBio = product.IsBio
            };
        }
    }
}