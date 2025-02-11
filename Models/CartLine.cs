using System;
using System.Collections.Generic;

namespace MonApi.Models;

public partial class CartLine
{
    public int ProductId { get; set; }

    public int CartId { get; set; }

    public int Quantity { get; set; }

    public bool IsSetAside { get; set; }

    public DateTime? UpdateTime { get; set; }

    public DateTime? CreationTime { get; set; }

    public virtual Cart Cart { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}
