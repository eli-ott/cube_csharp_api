using System;
using System.Collections.Generic;

namespace MonApi.Models;

public partial class Family
{
    public int FamilyId { get; set; }

    public string Name { get; set; } = null!;

    public DateTime? DeletionTime { get; set; }

    public DateTime UpdateTime { get; set; }

    public DateTime CreationTime { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
