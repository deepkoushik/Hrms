using System;
using System.Collections.Generic;

namespace Kryptos.Hrms.API.Models;

public partial class Language
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<AcademicQualification> AcademicQualifications { get; } = new List<AcademicQualification>();

    public virtual ICollection<CoreSkill> CoreSkills { get; } = new List<CoreSkill>();
}
