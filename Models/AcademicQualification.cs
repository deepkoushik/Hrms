using System;
using System.Collections.Generic;

namespace Kryptos.Hrms.API.Models;

public partial class AcademicQualification
{
    public int Id { get; set; }

    public int? EmployeeId { get; set; }

    public int? QualificationId { get; set; }

    public int? SpecializationId { get; set; }

    public int? UniversityId { get; set; }

    public int? InstituteId { get; set; }

    public int? LanguageId { get; set; }

    public DateTime? YearOfPassing { get; set; }

    public string? CourseOfAppraisal { get; set; }

    public string? CourseType { get; set; }

    public byte[]? HscmarkSheet { get; set; }

    public byte[]? UgmarkSheets { get; set; }

    public byte[]? UgprovisionalCertificate { get; set; }

    public byte[]? PgmarkSheets { get; set; }

    public byte[]? PgprovisionalCertificate { get; set; }

    public string? Status { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedTime { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? UpdateTime { get; set; }

    public virtual Employee? Employee { get; set; }

    public virtual Institute? Institute { get; set; }

    public virtual Language? Language { get; set; }

    public virtual Qualification? Qualification { get; set; }

    public virtual Specialization? Specialization { get; set; }

    public virtual University? University { get; set; }
}
