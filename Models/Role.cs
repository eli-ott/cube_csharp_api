using System;
using System.Collections.Generic;

namespace MonApi.Models;

public partial class Role
{
    public int RoleId { get; set; }

    public string Name { get; set; } = null!;

    public DateTime? DeletionTime { get; set; }

    public DateTime UpdateTime { get; set; }

    public DateTime CreationTime { get; set; }

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
