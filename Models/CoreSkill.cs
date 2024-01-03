using System;
using System.Collections.Generic;

namespace Kryptos.Hrms.API.Models;

public partial class CoreSkill
{
    public int Id { get; set; }

    public int? EmployeeId { get; set; }

    public string? Name { get; set; }

    public string? LevelOfProficiency { get; set; }

    public string? Notes { get; set; }

    public bool? Status { get; set; }

    public int? LanguageId { get; set; }

    public string? LanguageLevelOfProficiency { get; set; }

    public bool? LanguageStatus { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedTime { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? UpdateTime { get; set; }

    public virtual Employee? Employee { get; set; }

    public virtual Language? Language { get; set; }
}
