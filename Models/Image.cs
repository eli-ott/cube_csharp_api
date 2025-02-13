using System;
using System.Collections.Generic;
using MonApi.API.Products.Models;

namespace MonApi.Models;

public partial class Image
{
    public string ImageId { get; set; } = null!;

    public string FormatType { get; set; } = null!;

    public int ProductId { get; set; }

    public DateTime UpdateTime { get; set; }

    public DateTime CreationTime { get; set; }

    public virtual Product Product { get; set; } = null!;
}
