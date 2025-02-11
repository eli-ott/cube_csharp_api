using System;
using System.Collections.Generic;
using MonApi.API.Customers.Models;
using MonApi.API.Suppliers.Models;

namespace MonApi.API.Addresses.Models;

public partial class Address
{
    public int AddressId { get; set; }
    public required string AddressLine { get; set; }
    public required string City { get; set; }
    public required string ZipCode { get; set; }
    public required string Country { get; set; }
    public string? Complement { get; set; }
    public DateTime? DeletionTime { get; set; }
    public DateTime UpdateTime { get; set; }
    public DateTime CreationTime { get; set; }
    public virtual ICollection<Customer> Customers { get; set; } = new List<Customer>();
    public virtual ICollection<Supplier> Suppliers { get; set; } = new List<Supplier>();
}