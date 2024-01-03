using System;
using System.Collections.Generic;

namespace Kryptos.Hrms.API.Models;

public partial class Holiday
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public DateTime? Date { get; set; }

    public int? EmployeeId { get; set; }

    public int? UpdatedBy { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? UpdateDateTime { get; set; }

    public DateTime? CreatedDateTime { get; set; }

    public virtual Employee? Employee { get; set; }

    public virtual ICollection<ShiftType> ShiftTypes { get; } = new List<ShiftType>();
}
