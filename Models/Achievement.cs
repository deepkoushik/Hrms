using System;
using System.Collections.Generic;

namespace Kryptos.Hrms.API.Models;

public partial class Achievement
{
    public int Id { get; set; }

    public int? EmployeeId { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }

    public DateTime? AchievementDate { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedTime { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? UpdateTime { get; set; }

    public virtual Employee? Employee { get; set; }
}
