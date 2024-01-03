using System;
using System.Collections.Generic;

namespace Kryptos.Hrms.API.Models;

public partial class Department
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public int? SubDepartmentId { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedTime { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? UpdateTime { get; set; }

    public virtual ICollection<Employee> Employees { get; } = new List<Employee>();

    public virtual SubDepartment? SubDepartment { get; set; }
}
