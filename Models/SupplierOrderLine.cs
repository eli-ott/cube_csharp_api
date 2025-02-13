using System;
using System.Collections.Generic;
using MonApi.API.Products.Models;

namespace MonApi.Models;

public partial class SupplierOrderLine
{
    public int OrderId { get; set; }

    public int ProductId { get; set; }

    public int Quantity { get; set; }

    public float UnitPrice { get; set; }

    public DateTime UpdateTime { get; set; }

    public DateTime CreationTime { get; set; }

    public virtual SupplierOrder Order { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}
