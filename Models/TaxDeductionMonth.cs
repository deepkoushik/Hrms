using System;
using System.Collections.Generic;

namespace Kryptos.Hrms.API.Models;

public partial class TaxDeductionMonth
{
    public int Id { get; set; }

    public DateTime? CreatedTime { get; set; }

    public string? Month { get; set; }

    public double? Amount { get; set; }

    public virtual ICollection<TaxComputation> TaxComputations { get; } = new List<TaxComputation>();
}
