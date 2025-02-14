using System;
using System.Collections.Generic;
using MonApi.API.Products.Models;

namespace MonApi.API.Discounts.Models;

public partial class Discount
{
    public int DiscountId { get; set; }

    public string Name { get; set; } = null!;

    public int Value { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public DateTime CreationTime { get; set; }

    public DateTime UpdateTime { get; set; }

    public int ProductId { get; set; }

    public virtual Product Product { get; set; } = null!;
}
