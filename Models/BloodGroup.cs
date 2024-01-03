using System;
using System.Collections.Generic;

namespace Kryptos.Hrms.API.Models;

public partial class BloodGroup
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<MedicalDatum> MedicalData { get; } = new List<MedicalDatum>();
}
