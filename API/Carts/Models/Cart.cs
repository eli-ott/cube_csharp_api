using MonApi.API.Customers.Models;
using System;
using System.Collections.Generic;

namespace MonApi.API.Carts.Models;

public partial class Cart
{
    public int CartId { get; set; }

    public int CustomerId { get; set; }

    public DateTime? UpdateTime { get; set; }

    public DateTime? CreationTime { get; set; }

    public virtual Customer Customer { get; set; } = null!;
}
