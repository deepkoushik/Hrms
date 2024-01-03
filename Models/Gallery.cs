using System;
using System.Collections.Generic;

namespace Kryptos.Hrms.API.Models;

public partial class Gallery
{
    public int Id { get; set; }

    public int? EmployeeId { get; set; }

    public int? EventTypeId { get; set; }

    public byte[]? Image { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedTime { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? UpdateTime { get; set; }

    public bool? IsActive { get; set; }

    public virtual Employee? Employee { get; set; }

    public virtual EventType? EventType { get; set; }
}
