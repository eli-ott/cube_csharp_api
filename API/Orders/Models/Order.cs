﻿using MonApi.API.Customers.Models;
using MonApi.API.Statuses.Models;
using System;
using System.Collections.Generic;

namespace MonApi.API.Orders.Models;

public partial class Order
{
    public int OrderId { get; set; }

    public DateTime? DeliveryDate { get; set; }

    public int StatusId { get; set; }

    public int CustomerId { get; set; }

    public DateTime? DeletionTime { get; set; }

    public DateTime UpdateTime { get; set; }

    public DateTime CreationTime { get; set; }

    public virtual Customer Customer { get; set; } = null!;

    public virtual Status Status { get; set; } = null!;
}
