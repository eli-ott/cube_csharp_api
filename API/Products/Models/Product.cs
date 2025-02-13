using MonApi.API.Families.Models;
using MonApi.API.Suppliers.Models;
using MonApi.Models;
using System;
using System.Collections.Generic;

namespace MonApi.API.Products.Models;

public partial class Product
{
    public int ProductId { get; set; }

    public string Name { get; set; } = null!;

    public string Cuvee { get; set; } = null!;

    public int Year { get; set; }

    public string ProducerName { get; set; } = null!;

    public bool IsBio { get; set; }

    public float? UnitPrice { get; set; }

    public float? CartonPrice { get; set; }

    public int Quantity { get; set; }

    public bool AutoRestock { get; set; }

    public int AutoRestockTreshold { get; set; }

    public DateTime? DeletionTime { get; set; }

    public DateTime UpdateTime { get; set; }

    public DateTime CreationTime { get; set; }

    public int FamilyId { get; set; }

    public int SupplierId { get; set; }

    public virtual ICollection<Discount> Discounts { get; set; } = new List<Discount>();

    public virtual Family Family { get; set; } = null!;

    public virtual ICollection<Image> Images { get; set; } = new List<Image>();

    public virtual Supplier Supplier { get; set; } = null!;
}
