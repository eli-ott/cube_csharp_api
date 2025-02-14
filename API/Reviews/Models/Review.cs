using MonApi.API.Customers.Models;
using MonApi.API.Products.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MonApi.Models;

namespace MonApi.API.Reviews.Models;

public partial class Review
{
    public int UserId { get; set; }
    public int ProductId { get; set; }

    public float Rating { get; set; }

    public string? Comment { get; set; }

    public DateTime? UpdateTime { get; set; }

    public DateTime CreationTime { get; set; }

    public virtual Product Product { get; set; } = null!;

    public virtual Customer User { get; set; } = null!;
}