using System;
using System.Collections.Generic;

namespace MonApi.Models;

public partial class Supplier
{
    public int SupplierId { get; set; }

    public string LastName { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string Contact { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string Siret { get; set; } = null!;

    public DateTime? DeletionTime { get; set; }

    public DateTime UpdateTime { get; set; }

    public DateTime CreationTime { get; set; }

    public int PasswordId { get; set; }

    public int AddressId { get; set; }

    public virtual Address Address { get; set; } = null!;

    public virtual Password Password { get; set; } = null!;

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
