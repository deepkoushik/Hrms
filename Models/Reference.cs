using System;
using System.Collections.Generic;

namespace Kryptos.Hrms.API.Models;

public partial class Reference
{
    public int Id { get; set; }

    public int? EmployeeId { get; set; }

    public string? Name { get; set; }

    public string? CollegeName { get; set; }

    public string? Designation { get; set; }

    public string? Phone { get; set; }

    public string? Status { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedTime { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? UpdateTime { get; set; }

    public virtual Employee? Employee { get; set; }
}
