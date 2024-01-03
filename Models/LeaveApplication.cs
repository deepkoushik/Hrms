using System;
using System.Collections.Generic;

namespace Kryptos.Hrms.API.Models;

public partial class LeaveApplication
{
    public int Id { get; set; }

    public int? EmployeeId { get; set; }

    public int? LeaveTypeId { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public bool? LeaveStatus { get; set; }

    public string? LeaveReason { get; set; }

    public bool? IsRequested { get; set; }

    public bool? IsApproved { get; set; }

    public int? ApproverId { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedTime { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? UpdateTime { get; set; }

    public bool? LineManager { get; set; }

    public bool? ToSecondHalfDay { get; set; }

    public bool? ToFirstHalfDay { get; set; }

    public bool? FromFirstHalf { get; set; }

    public bool? FromSecondHalf { get; set; }

    public string? AlternateMobileNo { get; set; }

    public bool? Medical { get; set; }

    public bool? FromFullDay { get; set; }

    public int? LeaveAvailabilitiesId { get; set; }

    public string? LeaveType { get; set; }

    public string? LeaveRejectionReason { get; set; }

    public virtual Employee? Employee { get; set; }

    public virtual LeaveAvailability? LeaveAvailabilities { get; set; }

    public virtual LeaveType? LeaveTypeNavigation { get; set; }
}
