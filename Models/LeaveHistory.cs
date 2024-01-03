using System;
using System.Collections.Generic;

namespace Kryptos.Hrms.API.Models;

public partial class LeaveHistory
{
    public int Id { get; set; }

    public int? EmployeeId { get; set; }

    public int? ApprovedById { get; set; }

    public int? LeaveTypeId { get; set; }

    public DateTime? FromDate { get; set; }

    public DateTime? ToDate { get; set; }

    public int? UnpaidLeave { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedTime { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? UpdateTime { get; set; }

    public virtual Employee? Employee { get; set; }

    public virtual LeaveType? LeaveType { get; set; }
}
