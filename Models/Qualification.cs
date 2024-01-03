using System;
using System.Collections.Generic;

namespace Kryptos.Hrms.API.Models;

public partial class Qualification
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<AcademicQualification> AcademicQualifications { get; } = new List<AcademicQualification>();
}
