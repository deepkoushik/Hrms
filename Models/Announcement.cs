using System;
using System.Collections.Generic;

namespace Kryptos.Hrms.API.Models;

public partial class Announcement
{
    public int Id { get; set; }

    public int? EmployeeId { get; set; }

    public string? Title { get; set; }

    public string? Content { get; set; }

    public byte[]? Attachment { get; set; }

    public DateTime? FromDateTime { get; set; }

    public DateTime? ToDateTime { get; set; }

    public bool? IsActive { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedTime { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? UpdatedTime { get; set; }

    public virtual Employee? Employee { get; set; }
}
