using System;
using System.Collections.Generic;
using MonApi.API.Customers.Models;

namespace MonApi.Models;

public partial class Review
{
    public int UserId { get; set; }

    public int ProductId { get; set; }

    public float Rating { get; set; }

    public string? Comment{ get; set; }

    public DateTime? UpdateTime { get; set; }

    public DateTime CreationTime { get; set; }

    public virtual Product Product { get; set; } = null!;

    public virtual Customer User { get; set; } = null!;
}
