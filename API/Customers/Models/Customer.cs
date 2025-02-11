using System;
using System.Collections.Generic;
using MonApi.API.Addresses.Models;
using MonApi.API.Passwords.Models;
using MonApi.Models;

namespace MonApi.API.Customers.Models;

public partial class Customer
{
    public int CustomerId { get; set; }

    public string LastName { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public DateTime? DeletionTime { get; set; }

    public bool Active { get; set; }

    public string ValidationId { get; set; } = null!;

    public DateTime CreationTime { get; set; }

    public DateTime UpdateTime { get; set; }

    public int PasswordId { get; set; }

    public int AddressId { get; set; }

    public virtual Address Address { get; set; } = null!;

    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual Password Password { get; set; } = null!;
}
