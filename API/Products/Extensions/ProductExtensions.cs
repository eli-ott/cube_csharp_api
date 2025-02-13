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
                AutoRestockTreshold = product.AutoRestockTreshold,
                FamilyId = product.FamilyId,
                SupplierId = product.SupplierId
            };
        }

        public static ReturnProductDTO MapToProductReturnDTO(this Product product, Family family, ReturnSupplierDTO supplier)
        {
            return new ReturnProductDTO()
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
                AutoRestockTreshold = product.AutoRestockTreshold,
                Family = family,
                Supplier = supplier
            };
        }
    }
}
