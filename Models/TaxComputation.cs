using System;
using System.Collections.Generic;

namespace Kryptos.Hrms.API.Models;

public partial class TaxComputation
{
    public int Id { get; set; }

    public int EmployeeId { get; set; }

    public double? GrossIncomeBeforePerk { get; set; }

    public double? GrossIncome { get; set; }

    public double? RentPaid { get; set; }

    public double? RentNetOf10Percent { get; set; }

    public double? Hra { get; set; }

    public double? Salary40Or50Percent { get; set; }

    public double? Hraexemption { get; set; }

    public double? SalaryAfterSection10Exemption { get; set; }

    public double? PreviousEmploymentIncome { get; set; }

    public double? SalaryBeforeDeductionUnder16 { get; set; }

    public double? ProfessionTax { get; set; }

    public double? PreviousEmploymentProfTax { get; set; }

    public double? StandardDeductionExemption { get; set; }

    public double? SalaryAfterDeductionUnder16 { get; set; }

    public double? InterestPaidOnHousingLoan { get; set; }

    public double? LetOutLossOnHouseProperty { get; set; }

    public double? NetIncome { get; set; }

    public double? MaximumDeductionUnder80C80ccc80ccd { get; set; }

    public double? TotalDeductionsUnderVia { get; set; }

    public double? TotalTaxableIncome { get; set; }

    public double? IncomeTaxOnTotalIncome { get; set; }

    public double? TaxRebateUnder87A { get; set; }

    public double? TaxOnTaxableIncome { get; set; }

    public double? Surcharge { get; set; }

    public double? EducationCess { get; set; }

    public double? GrossTaxPayable { get; set; }

    public double? TaxDeductedTillDate { get; set; }

    public double? PreviousEmploymentDeductedIncomeTax { get; set; }

    public double? BalanceTaxPayableOrRefund { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedTime { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? UpdateTime { get; set; }

    public int? TaxDeductionMonthId { get; set; }

    public virtual Employee Employee { get; set; } = null!;

    public virtual TaxDeductionMonth? TaxDeductionMonth { get; set; }
}
