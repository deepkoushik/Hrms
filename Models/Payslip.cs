using System;
using System.Collections.Generic;

namespace Kryptos.Hrms.API.Models;

public partial class Payslip
{
    public int Id { get; set; }

    public int? EmployeeId { get; set; }

    public DateTime? YearMonth { get; set; }

    public int? Month { get; set; }

    public int? Year { get; set; }

    public double? BasicEarnings { get; set; }

    public double? HraEarnings { get; set; }

    public double? ConveyanceEarnings { get; set; }

    public double? MedicalEarnings { get; set; }

    public double? SpecialAllowance { get; set; }

    public double? EarnedBonus { get; set; }

    public double? Arrear { get; set; }

    public double? ProvidentFundDeduction { get; set; }

    public double? TotalEarnings { get; set; }

    public double? TotalDeductions { get; set; }

    public double? NetPayable { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedTime { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? UpdateTime { get; set; }

    public double? Mediclaim { get; set; }

    public virtual Employee? Employee { get; set; }
}
