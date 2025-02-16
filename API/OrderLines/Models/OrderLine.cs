using System;
using System.Collections.Generic;
using MonApi.API.Orders.Models;
using MonApi.API.Products.Models;

namespace MonApi.API.OrderLines.Models;

public partial class OrderLine
{
    public int OrderId { get; set; }

    public int ProductId { get; set; }

    public int Quantity { get; set; }

    public float UnitPrice { get; set; }

    public DateTime UpdateTime { get; set; }

    public DateTime CreationTime { get; set; }

    public DateTime? DeletionTime { get; set; }

    public virtual Order Order { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}
