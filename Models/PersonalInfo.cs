using System;
using System.Collections.Generic;

namespace Kryptos.Hrms.API.Models;

public partial class PersonalInfo
{
    public int Id { get; set; }

    public int? EmployeeId { get; set; }

    public string? Name { get; set; }

    public string? KnownAs { get; set; }

    public DateTime? Dob { get; set; }

    public string? Gender { get; set; }

    public string? FatherName { get; set; }

    public string? MotherName { get; set; }

    public string? OfficialEmail { get; set; }

    public string? PersonalEmail { get; set; }

    public string? CountryOfBirth { get; set; }

    public string? State { get; set; }

    public string? PlaceOfBirth { get; set; }

    public string? Nationality { get; set; }

    public string? MotherTongue { get; set; }

    public string? Religion { get; set; }

    public string? HighQualificationLevel { get; set; }

    public bool? IsSpeciallyAbled { get; set; }

    public string? Description { get; set; }

    public bool? IsMarried { get; set; }

    public DateTime? WeddingDate { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedTime { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? UpdateTime { get; set; }

    public virtual Employee? Employee { get; set; }
}
