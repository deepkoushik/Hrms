using System;
using System.Collections.Generic;

namespace Kryptos.Hrms.API.Models;

public partial class Reimbursement
{
    public int Id { get; set; }

    public int? EmployeeId { get; set; }

    public int? ApprovedById { get; set; }

    public string? ReferenceCode { get; set; }

    public string? BillNumber { get; set; }

    public DateTime? BillDate { get; set; }

    public string? BillPeriod { get; set; }

    public double? Amount { get; set; }

    public string? Description { get; set; }

    public byte[]? BillDocument { get; set; }

    public int? ReimbutsementTypeId { get; set; }

    public DateTime? AppliedDate { get; set; }

    public string? Reason { get; set; }

    public DateTime? ApprovedDate { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedTime { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? UpdateTime { get; set; }

    public bool? IsApprovedByManager { get; set; }

    public bool? IsApprovedByHr { get; set; }

    public bool? IsApprovedByFinanceTeam { get; set; }

    public bool? IsRequested { get; set; }

    public virtual Employee? Employee { get; set; }

    public virtual ReimbursementType? ReimbutsementType { get; set; }
}
