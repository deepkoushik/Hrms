using System;
using System.Collections.Generic;

namespace Kryptos.Hrms.API.Models;

public partial class GeneralShift
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public DateTime? DateTime { get; set; }

    public virtual ICollection<ShiftType> ShiftTypes { get; } = new List<ShiftType>();
}
