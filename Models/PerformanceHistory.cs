using System;
using System.Collections.Generic;

namespace Kryptos.Hrms.API.Models;

public partial class PerformanceHistory
{
    public int Id { get; set; }

    public int? EmployeeId { get; set; }

    public DateTime? StartPeriod { get; set; }

    public DateTime? EndPeriod { get; set; }

    public double? FinalScore { get; set; }

    public string? Comments { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedTime { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? UpdateTime { get; set; }

    public virtual Employee? Employee { get; set; }
}
