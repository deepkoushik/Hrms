using System;
using System.Collections.Generic;

namespace Kryptos.Hrms.API.Models;

public partial class BankInfo
{
    public int Id { get; set; }

    public int? EmployeeId { get; set; }

    public DateTime? Date { get; set; }

    public string? Ifsccode { get; set; }

    public string? BranchName { get; set; }

    public string? BankAccountName { get; set; }

    public string? BankAccountNumber { get; set; }

    public string? AccountName { get; set; }

    public byte[]? PassbookAttachment { get; set; }

    public string? Status { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedTime { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? UpdateTime { get; set; }

    public virtual Employee? Employee { get; set; }
}
