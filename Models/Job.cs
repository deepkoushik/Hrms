using System;
using System.Collections.Generic;

namespace Kryptos.Hrms.API.Models;

public partial class Job
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Location { get; set; }

    public string? Level { get; set; }

    public virtual ICollection<Employee> Employees { get; } = new List<Employee>();
}
