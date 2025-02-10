﻿using System;
using System.Collections.Generic;

namespace MonApi.Models;

public partial class Cart
{
    public int CartId { get; set; }

    public int CustomerId { get; set; }

    public DateTime? UpdateTime { get; set; }

    public DateTime? CreationTime { get; set; }

    public virtual Customer Customer { get; set; } = null!;
}
