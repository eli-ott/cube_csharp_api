﻿using MonApi.API.Statuses.Models;
using System;
using System.Collections.Generic;

namespace MonApi.Models;

public partial class SupplierOrder
{
    public int OrderId { get; set; }

    public DateTime? DeliveryDate { get; set; }

    public DateTime? DeletionTime { get; set; }

    public DateTime CreationTime { get; set; }

    public DateTime UpdateTime { get; set; }

    public int EmployeeId { get; set; }

    public int StatusId { get; set; }

    public virtual Employee Employee { get; set; } = null!;

    public virtual Status Status { get; set; } = null!;
}
