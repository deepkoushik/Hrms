using System;
using System.Collections.Generic;

namespace Kryptos.Hrms.API.Models;

public partial class MedicalDatum
{
    public int Id { get; set; }

    public int EmployeeId { get; set; }

    public double? Height { get; set; }

    public double? Weight { get; set; }

    public bool? Hospitalisation { get; set; }

    public bool? AnySurgeriesInThePast { get; set; }

    public string? AnySurgeriesInTheDescription { get; set; }

    public int? BloodGroupId { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedTime { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? UpdateTime { get; set; }

    public virtual BloodGroup? BloodGroup { get; set; }

    public virtual Employee Employee { get; set; } = null!;
}
