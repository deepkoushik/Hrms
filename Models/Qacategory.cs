using System;
using System.Collections.Generic;

namespace Kryptos.Hrms.API.Models;

public partial class Qacategory
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public int? QagroupId { get; set; }

    public virtual Qagroup? Qagroup { get; set; }

    public virtual ICollection<ServiceDesk> ServiceDesks { get; } = new List<ServiceDesk>();
}
