using System;
using System.Collections.Generic;

namespace Kryptos.Hrms.API.Models;

public partial class LeaveAvailability
{
    public int Id { get; set; }

    public int? EmployeeId { get; set; }

    public int? CasualLeaveBalance { get; set; }

    public int? SickLeaveBalance { get; set; }

    public int? ComponsetoryLeaveBalance { get; set; }

    public int? TotalLeaveBalance { get; set; }

    public DateTime? LastUpdated { get; set; }

    public virtual Employee? Employee { get; set; }

    public virtual ICollection<LeaveApplication> LeaveApplications { get; } = new List<LeaveApplication>();
}
