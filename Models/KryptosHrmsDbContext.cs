using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Kryptos.Hrms.API.Models;

public partial class KryptosHrmsDbContext : DbContext
{
    public KryptosHrmsDbContext()
    {
    }

    public KryptosHrmsDbContext(DbContextOptions<KryptosHrmsDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AcademicQualification> AcademicQualifications { get; set; }

    public virtual DbSet<Achievement> Achievements { get; set; }

    public virtual DbSet<Announcement> Announcements { get; set; }

    public virtual DbSet<Attendance> Attendances { get; set; }

    public virtual DbSet<BankInfo> BankInfos { get; set; }

    public virtual DbSet<BloodGroup> BloodGroups { get; set; }

    public virtual DbSet<CertificationCourse> CertificationCourses { get; set; }

    public virtual DbSet<CompetencyRatingScale> CompetencyRatingScales { get; set; }

    public virtual DbSet<ContactInformation> ContactInformations { get; set; }

    public virtual DbSet<CoreSkill> CoreSkills { get; set; }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<EarningsPercentageAllocation> EarningsPercentageAllocations { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<EventType> EventTypes { get; set; }

    public virtual DbSet<Gallery> Galleries { get; set; }

    public virtual DbSet<GeneralShift> GeneralShifts { get; set; }

    public virtual DbSet<Greeting> Greetings { get; set; }

    public virtual DbSet<GreetingCard> GreetingCards { get; set; }

    public virtual DbSet<GreetingType> GreetingTypes { get; set; }

    public virtual DbSet<Holiday> Holidays { get; set; }

    public virtual DbSet<IdentificationDetail> IdentificationDetails { get; set; }

    public virtual DbSet<Institute> Institutes { get; set; }

    public virtual DbSet<ItineraryHistory> ItineraryHistories { get; set; }

    public virtual DbSet<Job> Jobs { get; set; }

    public virtual DbSet<Language> Languages { get; set; }

    public virtual DbSet<LeaveApplication> LeaveApplications { get; set; }

    public virtual DbSet<LeaveAvailability> LeaveAvailabilities { get; set; }

    public virtual DbSet<LeaveHistory> LeaveHistories { get; set; }

    public virtual DbSet<LeaveType> LeaveTypes { get; set; }

    public virtual DbSet<MedicalDatum> MedicalData { get; set; }

    public virtual DbSet<Payslip> Payslips { get; set; }

    public virtual DbSet<PerformanceHistory> PerformanceHistories { get; set; }

    public virtual DbSet<PersonalInfo> PersonalInfos { get; set; }

    public virtual DbSet<Q1history> Q1histories { get; set; }

    public virtual DbSet<Q2history> Q2histories { get; set; }

    public virtual DbSet<Q3history> Q3histories { get; set; }

    public virtual DbSet<Q4history> Q4histories { get; set; }

    public virtual DbSet<Qacategory> Qacategories { get; set; }

    public virtual DbSet<Qagroup> Qagroups { get; set; }

    public virtual DbSet<Qualification> Qualifications { get; set; }

    public virtual DbSet<Reference> References { get; set; }

    public virtual DbSet<Reimbursement> Reimbursements { get; set; }

    public virtual DbSet<ReimbursementType> ReimbursementTypes { get; set; }

    public virtual DbSet<RelationName> RelationNames { get; set; }

    public virtual DbSet<Resignation> Resignations { get; set; }

    public virtual DbSet<SalaryDetail> SalaryDetails { get; set; }

    public virtual DbSet<ServiceDesk> ServiceDesks { get; set; }

    public virtual DbSet<ShiftType> ShiftTypes { get; set; }

    public virtual DbSet<Specialization> Specializations { get; set; }

    public virtual DbSet<SubDepartment> SubDepartments { get; set; }

    public virtual DbSet<TaxAssessmentYear> TaxAssessmentYears { get; set; }

    public virtual DbSet<TaxComputation> TaxComputations { get; set; }

    public virtual DbSet<TaxDeductionMonth> TaxDeductionMonths { get; set; }

    public virtual DbSet<TravelType> TravelTypes { get; set; }

    public virtual DbSet<University> Universities { get; set; }

    public virtual DbSet<WeekOff> WeekOffs { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server = KTP-LT-INTERN33;Database = Kryptos.Hrms.Db;User ID=sa;Password=Password@123;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AcademicQualification>(entity =>
        {
            entity.HasIndex(e => e.EmployeeId, "IX_AcademicQualifications_EmployeeId");

            entity.HasIndex(e => e.InstituteId, "IX_AcademicQualifications_InstituteId");

            entity.HasIndex(e => e.LanguageId, "IX_AcademicQualifications_LanguageId");

            entity.HasIndex(e => e.QualificationId, "IX_AcademicQualifications_QualificationId");

            entity.HasIndex(e => e.SpecializationId, "IX_AcademicQualifications_SpecializationId");

            entity.HasIndex(e => e.UniversityId, "IX_AcademicQualifications_UniversityId");

            entity.Property(e => e.HscmarkSheet).HasColumnName("HSCMarkSheet");
            entity.Property(e => e.PgmarkSheets).HasColumnName("PGMarkSheets");
            entity.Property(e => e.PgprovisionalCertificate).HasColumnName("PGProvisionalCertificate");
            entity.Property(e => e.UgmarkSheets).HasColumnName("UGMarkSheets");
            entity.Property(e => e.UgprovisionalCertificate).HasColumnName("UGProvisionalCertificate");

            entity.HasOne(d => d.Employee).WithMany(p => p.AcademicQualifications).HasForeignKey(d => d.EmployeeId);

            entity.HasOne(d => d.Institute).WithMany(p => p.AcademicQualifications).HasForeignKey(d => d.InstituteId);

            entity.HasOne(d => d.Language).WithMany(p => p.AcademicQualifications).HasForeignKey(d => d.LanguageId);

            entity.HasOne(d => d.Qualification).WithMany(p => p.AcademicQualifications).HasForeignKey(d => d.QualificationId);

            entity.HasOne(d => d.Specialization).WithMany(p => p.AcademicQualifications).HasForeignKey(d => d.SpecializationId);

            entity.HasOne(d => d.University).WithMany(p => p.AcademicQualifications).HasForeignKey(d => d.UniversityId);
        });

        modelBuilder.Entity<Achievement>(entity =>
        {
            entity.HasIndex(e => e.EmployeeId, "IX_Achievements_EmployeeId");

            entity.HasOne(d => d.Employee).WithMany(p => p.Achievements).HasForeignKey(d => d.EmployeeId);
        });

        modelBuilder.Entity<Announcement>(entity =>
        {
            entity.HasIndex(e => e.EmployeeId, "IX_Announcements_EmployeeId");

            entity.HasOne(d => d.Employee).WithMany(p => p.Announcements).HasForeignKey(d => d.EmployeeId);
        });

        modelBuilder.Entity<Attendance>(entity =>
        {
            entity.HasIndex(e => e.EmployeeId, "IX_Attendances_EmployeeId");

            entity.HasIndex(e => e.ShiftTypeId, "IX_Attendances_ShiftTypeId");

            entity.Property(e => e.LineManagerRejectionReason)
                .HasMaxLength(450)
                .IsUnicode(false);

            entity.HasOne(d => d.Employee).WithMany(p => p.Attendances).HasForeignKey(d => d.EmployeeId);

            entity.HasOne(d => d.ShiftType).WithMany(p => p.Attendances).HasForeignKey(d => d.ShiftTypeId);
        });

        modelBuilder.Entity<BankInfo>(entity =>
        {
            entity.HasIndex(e => e.EmployeeId, "IX_BankInfos_EmployeeId");

            entity.Property(e => e.Ifsccode).HasColumnName("IFSCCode");

            entity.HasOne(d => d.Employee).WithMany(p => p.BankInfos).HasForeignKey(d => d.EmployeeId);
        });

        modelBuilder.Entity<CertificationCourse>(entity =>
        {
            entity.HasIndex(e => e.EmployeeId, "IX_CertificationCourses_EmployeeId");

            entity.HasOne(d => d.Employee).WithMany(p => p.CertificationCourses).HasForeignKey(d => d.EmployeeId);
        });

        modelBuilder.Entity<ContactInformation>(entity =>
        {
            entity.HasIndex(e => e.EmployeeId, "IX_ContactInformations_EmployeeId");

            entity.Property(e => e.Address1).HasColumnName("Address_1");
            entity.Property(e => e.Address2).HasColumnName("Address_2");
            entity.Property(e => e.Address3).HasColumnName("Address_3");
            entity.Property(e => e.PermanentAddress1).HasColumnName("PermanentAddress_1");
            entity.Property(e => e.PermanentAddress2).HasColumnName("PermanentAddress_2");
            entity.Property(e => e.PermanentAddress3).HasColumnName("PermanentAddress_3");

            entity.HasOne(d => d.Employee).WithMany(p => p.ContactInformations).HasForeignKey(d => d.EmployeeId);
        });

        modelBuilder.Entity<CoreSkill>(entity =>
        {
            entity.HasIndex(e => e.EmployeeId, "IX_CoreSkills_EmployeeId");

            entity.HasIndex(e => e.LanguageId, "IX_CoreSkills_LanguageId");

            entity.HasOne(d => d.Employee).WithMany(p => p.CoreSkills).HasForeignKey(d => d.EmployeeId);

            entity.HasOne(d => d.Language).WithMany(p => p.CoreSkills).HasForeignKey(d => d.LanguageId);
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasIndex(e => e.SubDepartmentId, "IX_Departments_SubDepartmentId");

            entity.HasOne(d => d.SubDepartment).WithMany(p => p.Departments).HasForeignKey(d => d.SubDepartmentId);
        });

        modelBuilder.Entity<EarningsPercentageAllocation>(entity =>
        {
            entity.HasIndex(e => e.UpdatedById, "IX_EarningsPercentageAllocations_UpdatedById");

            entity.HasOne(d => d.UpdatedBy).WithMany(p => p.EarningsPercentageAllocations).HasForeignKey(d => d.UpdatedById);
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasIndex(e => e.DepartmentId, "IX_Employees_DepartmentID");

            entity.HasIndex(e => e.JobId, "IX_Employees_JobId");

            entity.Property(e => e.DepartmentId).HasColumnName("DepartmentID");
            entity.Property(e => e.EmailId).HasColumnName("EmailID");
            entity.Property(e => e.ManagerId).HasColumnName("ManagerID");

            entity.HasOne(d => d.Department).WithMany(p => p.Employees).HasForeignKey(d => d.DepartmentId);

            entity.HasOne(d => d.Job).WithMany(p => p.Employees).HasForeignKey(d => d.JobId);
        });

        modelBuilder.Entity<Gallery>(entity =>
        {
            entity.HasIndex(e => e.EmployeeId, "IX_Galleries_EmployeeId");

            entity.HasIndex(e => e.EventTypeId, "IX_Galleries_EventTypeId");

            entity.HasOne(d => d.Employee).WithMany(p => p.Galleries).HasForeignKey(d => d.EmployeeId);

            entity.HasOne(d => d.EventType).WithMany(p => p.Galleries).HasForeignKey(d => d.EventTypeId);
        });

        modelBuilder.Entity<Greeting>(entity =>
        {
            entity.HasIndex(e => e.EmployeeId, "IX_Greetings_EmployeeId");

            entity.HasIndex(e => e.GreetingCardId, "IX_Greetings_GreetingCardId");

            entity.HasIndex(e => e.GreetingTypeId, "IX_Greetings_GreetingTypeId");

            entity.HasOne(d => d.Employee).WithMany(p => p.Greetings).HasForeignKey(d => d.EmployeeId);

            entity.HasOne(d => d.GreetingCard).WithMany(p => p.Greetings).HasForeignKey(d => d.GreetingCardId);

            entity.HasOne(d => d.GreetingType).WithMany(p => p.Greetings).HasForeignKey(d => d.GreetingTypeId);
        });

        modelBuilder.Entity<Holiday>(entity =>
        {
            entity.Property(e => e.CreatedDateTime).HasColumnType("datetime");
            entity.Property(e => e.UpdateDateTime).HasColumnType("datetime");

            entity.HasOne(d => d.Employee).WithMany(p => p.Holidays)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK__Holidays__Employ__6166761E");
        });

        modelBuilder.Entity<IdentificationDetail>(entity =>
        {
            entity.HasIndex(e => e.EmployeeId, "IX_IdentificationDetails_EmployeeId");

            entity.Property(e => e.Esinumber).HasColumnName("ESINumber");
            entity.Property(e => e.NameAsPerPan).HasColumnName("NameAsPerPAN");
            entity.Property(e => e.Panattachment).HasColumnName("PANAttachment");
            entity.Property(e => e.Pannumber).HasColumnName("PANNumber");
            entity.Property(e => e.PassportEcntstatus).HasColumnName("PassportECNTStatus");
            entity.Property(e => e.Pfnumber).HasColumnName("PFNumber");

            entity.HasOne(d => d.Employee).WithMany(p => p.IdentificationDetails).HasForeignKey(d => d.EmployeeId);
        });

        modelBuilder.Entity<ItineraryHistory>(entity =>
        {
            entity.HasIndex(e => e.EmployeeId, "IX_ItineraryHistories_EmployeeId");

            entity.HasIndex(e => e.TravelTypeId, "IX_ItineraryHistories_TravelTypeId");

            entity.HasOne(d => d.Employee).WithMany(p => p.ItineraryHistories).HasForeignKey(d => d.EmployeeId);

            entity.HasOne(d => d.TravelType).WithMany(p => p.ItineraryHistories).HasForeignKey(d => d.TravelTypeId);
        });

        modelBuilder.Entity<LeaveApplication>(entity =>
        {
            entity.HasIndex(e => e.EmployeeId, "IX_LeaveApplications_EmployeeId");

            entity.HasIndex(e => e.LeaveTypeId, "IX_LeaveApplications_LeaveTypeId");

            entity.Property(e => e.AlternateMobileNo)
                .HasMaxLength(12)
                .IsUnicode(false);
            entity.Property(e => e.EndDate).HasColumnType("date");
            entity.Property(e => e.LeaveRejectionReason)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.LeaveType)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.StartDate).HasColumnType("date");

            entity.HasOne(d => d.Employee).WithMany(p => p.LeaveApplications).HasForeignKey(d => d.EmployeeId);

            entity.HasOne(d => d.LeaveAvailabilities).WithMany(p => p.LeaveApplications)
                .HasForeignKey(d => d.LeaveAvailabilitiesId)
                .HasConstraintName("FK_LeaveApplications_LeaveAvailabilities");

            entity.HasOne(d => d.LeaveTypeNavigation).WithMany(p => p.LeaveApplications).HasForeignKey(d => d.LeaveTypeId);
        });

        modelBuilder.Entity<LeaveAvailability>(entity =>
        {
            entity.HasIndex(e => e.EmployeeId, "IX_LeaveAvailabilities_EmployeeId");

            entity.HasOne(d => d.Employee).WithMany(p => p.LeaveAvailabilities).HasForeignKey(d => d.EmployeeId);
        });

        modelBuilder.Entity<LeaveHistory>(entity =>
        {
            entity.HasIndex(e => e.EmployeeId, "IX_LeaveHistories_EmployeeId");

            entity.HasIndex(e => e.LeaveTypeId, "IX_LeaveHistories_LeaveTypeId");

            entity.HasOne(d => d.Employee).WithMany(p => p.LeaveHistories).HasForeignKey(d => d.EmployeeId);

            entity.HasOne(d => d.LeaveType).WithMany(p => p.LeaveHistories).HasForeignKey(d => d.LeaveTypeId);
        });

        modelBuilder.Entity<LeaveType>(entity =>
        {
            entity.HasOne(d => d.Employee).WithMany(p => p.LeaveTypes)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("FK_LeaveType_Employee");
        });

        modelBuilder.Entity<MedicalDatum>(entity =>
        {
            entity.HasIndex(e => e.BloodGroupId, "IX_MedicalData_BloodGroupId");

            entity.HasIndex(e => e.EmployeeId, "IX_MedicalData_EmployeeId");

            entity.HasOne(d => d.BloodGroup).WithMany(p => p.MedicalData).HasForeignKey(d => d.BloodGroupId);

            entity.HasOne(d => d.Employee).WithMany(p => p.MedicalData).HasForeignKey(d => d.EmployeeId);
        });

        modelBuilder.Entity<Payslip>(entity =>
        {
            entity.HasIndex(e => e.EmployeeId, "IX_Payslips_EmployeeId");

            entity.HasOne(d => d.Employee).WithMany(p => p.Payslips).HasForeignKey(d => d.EmployeeId);
        });

        modelBuilder.Entity<PerformanceHistory>(entity =>
        {
            entity.HasIndex(e => e.EmployeeId, "IX_PerformanceHistories_EmployeeId");

            entity.HasOne(d => d.Employee).WithMany(p => p.PerformanceHistories).HasForeignKey(d => d.EmployeeId);
        });

        modelBuilder.Entity<PersonalInfo>(entity =>
        {
            entity.HasIndex(e => e.EmployeeId, "IX_PersonalInfos_EmployeeId")
                .IsUnique()
                .HasFilter("([EmployeeId] IS NOT NULL)");

            entity.Property(e => e.Dob).HasColumnName("DOB");

            entity.HasOne(d => d.Employee).WithOne(p => p.PersonalInfo).HasForeignKey<PersonalInfo>(d => d.EmployeeId);
        });

        modelBuilder.Entity<Q1history>(entity =>
        {
            entity.ToTable("Q1Histories");

            entity.HasIndex(e => e.EmployeeId, "IX_Q1Histories_EmployeeId");

            entity.HasIndex(e => e.GoalCompetencyRatingScaleId, "IX_Q1Histories_GoalCompetencyRatingScaleID");

            entity.Property(e => e.GoalCompetencyRatingScaleId).HasColumnName("GoalCompetencyRatingScaleID");
            entity.Property(e => e.Kratitle).HasColumnName("KRATitle");
            entity.Property(e => e.Kraweightage).HasColumnName("KRAWeightage");
            entity.Property(e => e.ReviewedByCompetencyRatingScaleId).HasColumnName("ReviewedByCompetencyRatingScaleID");

            entity.HasOne(d => d.Employee).WithMany(p => p.Q1histories).HasForeignKey(d => d.EmployeeId);

            entity.HasOne(d => d.GoalCompetencyRatingScale).WithMany(p => p.Q1histories).HasForeignKey(d => d.GoalCompetencyRatingScaleId);
        });

        modelBuilder.Entity<Q2history>(entity =>
        {
            entity.ToTable("Q2Histories");

            entity.HasIndex(e => e.EmployeeId, "IX_Q2Histories_EmployeeId");

            entity.HasIndex(e => e.GoalCompetencyRatingScaleId, "IX_Q2Histories_GoalCompetencyRatingScaleID");

            entity.Property(e => e.GoalCompetencyRatingScaleId).HasColumnName("GoalCompetencyRatingScaleID");
            entity.Property(e => e.Kratitle).HasColumnName("KRATitle");
            entity.Property(e => e.Kraweightage).HasColumnName("KRAWeightage");
            entity.Property(e => e.ReviewedByCompetencyRatingScaleId).HasColumnName("ReviewedByCompetencyRatingScaleID");

            entity.HasOne(d => d.Employee).WithMany(p => p.Q2histories).HasForeignKey(d => d.EmployeeId);

            entity.HasOne(d => d.GoalCompetencyRatingScale).WithMany(p => p.Q2histories).HasForeignKey(d => d.GoalCompetencyRatingScaleId);
        });

        modelBuilder.Entity<Q3history>(entity =>
        {
            entity.ToTable("Q3Histories");

            entity.HasIndex(e => e.EmployeeId, "IX_Q3Histories_EmployeeId");

            entity.HasIndex(e => e.GoalCompetencyRatingScaleId, "IX_Q3Histories_GoalCompetencyRatingScaleID");

            entity.Property(e => e.GoalCompetencyRatingScaleId).HasColumnName("GoalCompetencyRatingScaleID");
            entity.Property(e => e.Kratitle).HasColumnName("KRATitle");
            entity.Property(e => e.Kraweightage).HasColumnName("KRAWeightage");
            entity.Property(e => e.ReviewedByCompetencyRatingScaleId).HasColumnName("ReviewedByCompetencyRatingScaleID");

            entity.HasOne(d => d.Employee).WithMany(p => p.Q3histories).HasForeignKey(d => d.EmployeeId);

            entity.HasOne(d => d.GoalCompetencyRatingScale).WithMany(p => p.Q3histories).HasForeignKey(d => d.GoalCompetencyRatingScaleId);
        });

        modelBuilder.Entity<Q4history>(entity =>
        {
            entity.ToTable("Q4Histories");

            entity.HasIndex(e => e.EmployeeId, "IX_Q4Histories_EmployeeId");

            entity.HasIndex(e => e.GoalCompetencyRatingScaleId, "IX_Q4Histories_GoalCompetencyRatingScaleID");

            entity.Property(e => e.GoalCompetencyRatingScaleId).HasColumnName("GoalCompetencyRatingScaleID");
            entity.Property(e => e.Kratitle).HasColumnName("KRATitle");
            entity.Property(e => e.Kraweightage).HasColumnName("KRAWeightage");
            entity.Property(e => e.ReviewedByCompetencyRatingScaleId).HasColumnName("ReviewedByCompetencyRatingScaleID");

            entity.HasOne(d => d.Employee).WithMany(p => p.Q4histories).HasForeignKey(d => d.EmployeeId);

            entity.HasOne(d => d.GoalCompetencyRatingScale).WithMany(p => p.Q4histories).HasForeignKey(d => d.GoalCompetencyRatingScaleId);
        });

        modelBuilder.Entity<Qacategory>(entity =>
        {
            entity.ToTable("QACategories");

            entity.HasIndex(e => e.QagroupId, "IX_QACategories_QAGroupId");

            entity.Property(e => e.QagroupId).HasColumnName("QAGroupId");

            entity.HasOne(d => d.Qagroup).WithMany(p => p.Qacategories).HasForeignKey(d => d.QagroupId);
        });

        modelBuilder.Entity<Qagroup>(entity =>
        {
            entity.ToTable("QAGroups");
        });

        modelBuilder.Entity<Reference>(entity =>
        {
            entity.HasIndex(e => e.EmployeeId, "IX_References_EmployeeId");

            entity.HasOne(d => d.Employee).WithMany(p => p.References).HasForeignKey(d => d.EmployeeId);
        });

        modelBuilder.Entity<Reimbursement>(entity =>
        {
            entity.HasIndex(e => e.EmployeeId, "IX_Reimbursements_EmployeeId");

            entity.HasIndex(e => e.ReimbutsementTypeId, "IX_Reimbursements_ReimbutsementTypeId");

            entity.Property(e => e.IsApprovedByFinanceTeam).HasColumnName("isApprovedByFinanceTeam");
            entity.Property(e => e.IsApprovedByHr).HasColumnName("isApprovedByHR");
            entity.Property(e => e.IsApprovedByManager).HasColumnName("isApprovedByManager");
            entity.Property(e => e.IsRequested).HasColumnName("isRequested");
            entity.Property(e => e.Reason).IsUnicode(false);

            entity.HasOne(d => d.Employee).WithMany(p => p.Reimbursements).HasForeignKey(d => d.EmployeeId);

            entity.HasOne(d => d.ReimbutsementType).WithMany(p => p.Reimbursements).HasForeignKey(d => d.ReimbutsementTypeId);
        });

        modelBuilder.Entity<Resignation>(entity =>
        {
            entity.HasIndex(e => e.EmployeeId, "IX_Resignations_EmployeeId");

            entity.HasOne(d => d.Employee).WithMany(p => p.Resignations).HasForeignKey(d => d.EmployeeId);
        });

        modelBuilder.Entity<SalaryDetail>(entity =>
        {
            entity.HasIndex(e => e.EmployeeId, "IX_SalaryDetails_EmployeeId");

            entity.Property(e => e.Ctcpm).HasColumnName("CTCPM");
            entity.Property(e => e.Ctcpy).HasColumnName("CTCPY");

            entity.HasOne(d => d.Employee).WithMany(p => p.SalaryDetails).HasForeignKey(d => d.EmployeeId);
        });

        modelBuilder.Entity<ServiceDesk>(entity =>
        {
            entity.HasIndex(e => e.EmployeeId, "IX_ServiceDesks_EmployeeId");

            entity.HasIndex(e => e.GroupId, "IX_ServiceDesks_GroupId");

            entity.HasIndex(e => e.QacategoryId, "IX_ServiceDesks_QACategoryId");

            entity.Property(e => e.QacategoryId).HasColumnName("QACategoryId");
            entity.Property(e => e.ReviewedById).HasColumnName("ReviewedByID");

            entity.HasOne(d => d.Employee).WithMany(p => p.ServiceDesks).HasForeignKey(d => d.EmployeeId);

            entity.HasOne(d => d.Group).WithMany(p => p.ServiceDesks).HasForeignKey(d => d.GroupId);

            entity.HasOne(d => d.Qacategory).WithMany(p => p.ServiceDesks).HasForeignKey(d => d.QacategoryId);
        });

        modelBuilder.Entity<ShiftType>(entity =>
        {
            entity.HasIndex(e => e.GeneralShiftId, "IX_ShiftTypes_GeneralShiftId");

            entity.HasIndex(e => e.HolidayId, "IX_ShiftTypes_HolidayId");

            entity.HasIndex(e => e.WeekOffId, "IX_ShiftTypes_WeekOffId");

            entity.HasOne(d => d.GeneralShift).WithMany(p => p.ShiftTypes).HasForeignKey(d => d.GeneralShiftId);

            entity.HasOne(d => d.Holiday).WithMany(p => p.ShiftTypes).HasForeignKey(d => d.HolidayId);

            entity.HasOne(d => d.WeekOff).WithMany(p => p.ShiftTypes).HasForeignKey(d => d.WeekOffId);
        });

        modelBuilder.Entity<TaxComputation>(entity =>
        {
            entity.HasIndex(e => e.EmployeeId, "IX_TaxComputations_EmployeeId");

            entity.HasIndex(e => e.TaxDeductionMonthId, "IX_TaxComputations_TaxDeductionMonthId");

            entity.Property(e => e.Hra).HasColumnName("HRA");
            entity.Property(e => e.Hraexemption).HasColumnName("HRAExemption");
            entity.Property(e => e.MaximumDeductionUnder80C80ccc80ccd).HasColumnName("MaximumDeductionUnder80C80CCC80CCD");
            entity.Property(e => e.TotalDeductionsUnderVia).HasColumnName("TotalDeductionsUnderVIA");

            entity.HasOne(d => d.Employee).WithMany(p => p.TaxComputations).HasForeignKey(d => d.EmployeeId);

            entity.HasOne(d => d.TaxDeductionMonth).WithMany(p => p.TaxComputations).HasForeignKey(d => d.TaxDeductionMonthId);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
