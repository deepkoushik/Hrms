﻿using System;
using System.Collections.Generic;

namespace Kryptos.Hrms.API.Models;

public partial class EventType
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Gallery> Galleries { get; } = new List<Gallery>();
}
