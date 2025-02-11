using System;
using System.Collections.Generic;
using MonApi.API.Passwords.Models;

namespace MonApi.Models;

public partial class Employee
{
    public int EmployeeId { get; set; }

    public string LastName { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public DateTime? DeletionTime { get; set; }

    public DateTime CreationTime { get; set; }

    public DateTime UpdateTime { get; set; }

    public int RoleId { get; set; }

    public int PasswordId { get; set; }

    public virtual Password Password { get; set; } = null!;

    public virtual Role Role { get; set; } = null!;

    public virtual ICollection<SupplierOrder> SupplierOrders { get; set; } = new List<SupplierOrder>();
}
