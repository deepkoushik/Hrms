using System;
using System.Collections.Generic;

namespace Kryptos.Hrms.API.Models;

public partial class Attendance
{
    public int Id { get; set; }

    public int? EmployeeId { get; set; }

    public int? ApprovedById { get; set; }

    public DateTime? CheckInTime { get; set; }

    public DateTime? CheckOutTime { get; set; }

    public double? WorkingHours { get; set; }

    public bool? IsPresent { get; set; }

    public bool? IsAbsent { get; set; }

    public int? ShiftTypeId { get; set; }

    public DateTime? AttendanceDate { get; set; }

    public bool? IsRequestForRegularization { get; set; }

    public string? ReasonForRegularization { get; set; }

    public bool? RegularizationStatus { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedTime { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? UpdateTime { get; set; }

    public bool? LineManager { get; set; }

    public bool? FirstHalfAttendance { get; set; }

    public bool? SecondHalfAttendance { get; set; }

    public string? LineManagerRejectionReason { get; set; }

    public virtual Employee? Employee { get; set; }

    public virtual ShiftType? ShiftType { get; set; }
}
