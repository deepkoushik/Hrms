using System;
using System.Collections.Generic;

namespace Kryptos.Hrms.API.Models;

public partial class Qagroup
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Qacategory> Qacategories { get; } = new List<Qacategory>();

    public virtual ICollection<ServiceDesk> ServiceDesks { get; } = new List<ServiceDesk>();
}
