using System;
using System.Collections.Generic;

namespace Kryptos.Hrms.API.Models;

public partial class Employee
{
    public int Id { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? EmailId { get; set; }

    public byte[]? PasswordHash { get; set; }

    public byte[]? PasswordSalt { get; set; }

    public string? PhoneNo { get; set; }

    public DateTime? HireDate { get; set; }

    public int? ManagerId { get; set; }

    public int? JobId { get; set; }

    public int? DepartmentId { get; set; }

    public string? Role { get; set; }

    public DateTime? PromationDate { get; set; }

    public bool? IsActive { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedTime { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? UpdateTime { get; set; }

    public virtual ICollection<AcademicQualification> AcademicQualifications { get; } = new List<AcademicQualification>();

    public virtual ICollection<Achievement> Achievements { get; } = new List<Achievement>();

    public virtual ICollection<Announcement> Announcements { get; } = new List<Announcement>();

    public virtual ICollection<Attendance> Attendances { get; } = new List<Attendance>();

    public virtual ICollection<BankInfo> BankInfos { get; } = new List<BankInfo>();

    public virtual ICollection<CertificationCourse> CertificationCourses { get; } = new List<CertificationCourse>();

    public virtual ICollection<ContactInformation> ContactInformations { get; } = new List<ContactInformation>();

    public virtual ICollection<CoreSkill> CoreSkills { get; } = new List<CoreSkill>();

    public virtual Department? Department { get; set; }

    public virtual ICollection<EarningsPercentageAllocation> EarningsPercentageAllocations { get; } = new List<EarningsPercentageAllocation>();

    public virtual ICollection<Gallery> Galleries { get; } = new List<Gallery>();

    public virtual ICollection<Greeting> Greetings { get; } = new List<Greeting>();

    public virtual ICollection<Holiday> Holidays { get; } = new List<Holiday>();

    public virtual ICollection<IdentificationDetail> IdentificationDetails { get; } = new List<IdentificationDetail>();

    public virtual ICollection<ItineraryHistory> ItineraryHistories { get; } = new List<ItineraryHistory>();

    public virtual Job? Job { get; set; }

    public virtual ICollection<LeaveApplication> LeaveApplications { get; } = new List<LeaveApplication>();

    public virtual ICollection<LeaveAvailability> LeaveAvailabilities { get; } = new List<LeaveAvailability>();

    public virtual ICollection<LeaveHistory> LeaveHistories { get; } = new List<LeaveHistory>();

    public virtual ICollection<LeaveType> LeaveTypes { get; } = new List<LeaveType>();

    public virtual ICollection<MedicalDatum> MedicalData { get; } = new List<MedicalDatum>();

    public virtual ICollection<Payslip> Payslips { get; } = new List<Payslip>();

    public virtual ICollection<PerformanceHistory> PerformanceHistories { get; } = new List<PerformanceHistory>();

    public virtual PersonalInfo? PersonalInfo { get; set; }

    public virtual ICollection<Q1history> Q1histories { get; } = new List<Q1history>();

    public virtual ICollection<Q2history> Q2histories { get; } = new List<Q2history>();

    public virtual ICollection<Q3history> Q3histories { get; } = new List<Q3history>();

    public virtual ICollection<Q4history> Q4histories { get; } = new List<Q4history>();

    public virtual ICollection<Reference> References { get; } = new List<Reference>();

    public virtual ICollection<Reimbursement> Reimbursements { get; } = new List<Reimbursement>();

    public virtual ICollection<Resignation> Resignations { get; } = new List<Resignation>();

    public virtual ICollection<SalaryDetail> SalaryDetails { get; } = new List<SalaryDetail>();

    public virtual ICollection<ServiceDesk> ServiceDesks { get; } = new List<ServiceDesk>();

    public virtual ICollection<TaxComputation> TaxComputations { get; } = new List<TaxComputation>();
}
