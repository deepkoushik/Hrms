using System;
using System.Collections.Generic;

namespace Kryptos.Hrms.API.Models;

public partial class ServiceDesk
{
    public int Id { get; set; }

    public int? EmployeeId { get; set; }

    public int? GroupId { get; set; }

    public int? QacategoryId { get; set; }

    public string? Query { get; set; }

    public byte[]? Attachment1 { get; set; }

    public byte[]? Attachment2 { get; set; }

    public byte[]? Attachment3 { get; set; }

    public bool? IsInitiated { get; set; }

    public int? ReviewedById { get; set; }

    public bool? IsManaged { get; set; }

    public bool? IsActive { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedTime { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? UpdateTime { get; set; }

    public virtual Employee? Employee { get; set; }

    public virtual Qagroup? Group { get; set; }

    public virtual Qacategory? Qacategory { get; set; }
}
