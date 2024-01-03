using System;
using System.Collections.Generic;

namespace Kryptos.Hrms.API.Models;

public partial class TaxAssessmentYear
{
    public int Id { get; set; }

    public DateTime? FromTaxAssessmentYear { get; set; }

    public DateTime? ToTaxAssessmentYear { get; set; }

    public DateTime? FromFinancialYear { get; set; }

    public DateTime? ToFinancialYear { get; set; }

    public int? Year { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedTime { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? UpdateTime { get; set; }
}
