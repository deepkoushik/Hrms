using System;
using System.Collections.Generic;

namespace Kryptos.Hrms.API.Models;

public partial class CertificationCourse
{
    public int Id { get; set; }

    public int? EmployeeId { get; set; }

    public string? CourseName { get; set; }

    public string? Provider { get; set; }

    public string? Description { get; set; }

    public DateTime? CompletionDate { get; set; }

    public DateTime? ValidUptoDate { get; set; }

    public byte[]? CertificationDocument { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedTime { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? UpdateTime { get; set; }

    public virtual Employee? Employee { get; set; }
}
