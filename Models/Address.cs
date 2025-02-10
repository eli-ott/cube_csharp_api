using System;
using System.Collections.Generic;

namespace MonApi.Models;

public partial class Address
{
    public int AddressId { get; set; }

    public string AddressLine { get; set; } = null!;

    public string City { get; set; } = null!;

    public string ZipCode { get; set; } = null!;

    public string Country { get; set; } = null!;

    public string? Complement { get; set; }

    public DateTime? DeletionTime { get; set; }

    public DateTime UpdateTime { get; set; }

    public DateTime CreationTime { get; set; }

    public virtual ICollection<Customer> Customers { get; set; } = new List<Customer>();

    public virtual ICollection<Supplier> Suppliers { get; set; } = new List<Supplier>();
}
