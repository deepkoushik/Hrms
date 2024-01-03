using System;
using System.Collections.Generic;

namespace Kryptos.Hrms.API.Models;

public partial class Resignation
{
    public int Id { get; set; }

    public int? EmployeeId { get; set; }

    public string? ResignationNote { get; set; }

    public string? ReasonForExit { get; set; }

    public DateTime? NoticePeriodStartDate { get; set; }

    public DateTime? LastWorkingDateConfirmation { get; set; }

    public bool? StatusInitiated { get; set; }

    public bool? StatusApproved { get; set; }

    public string? StatusApprovedBy { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedTime { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? UpdateTime { get; set; }

    public virtual Employee? Employee { get; set; }
}
