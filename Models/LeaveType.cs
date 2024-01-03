using System;
using System.Collections.Generic;

namespace Kryptos.Hrms.API.Models;

public partial class LeaveType
{
    public int Id { get; set; }

    public string? LeaveTypeName { get; set; }

    public string? IsActive { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedTime { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? UpdateTime { get; set; }

    public int? EmployeeId { get; set; }

    public virtual Employee? Employee { get; set; }

    public virtual ICollection<LeaveApplication> LeaveApplications { get; } = new List<LeaveApplication>();

    public virtual ICollection<LeaveHistory> LeaveHistories { get; } = new List<LeaveHistory>();
}
