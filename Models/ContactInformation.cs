using System;
using System.Collections.Generic;

namespace Kryptos.Hrms.API.Models;

public partial class ContactInformation
{
    public int Id { get; set; }

    public int? EmployeeId { get; set; }

    public string? Address1 { get; set; }

    public string? Address2 { get; set; }

    public string? Address3 { get; set; }

    public string? Country { get; set; }

    public string? State { get; set; }

    public string? Pincode { get; set; }

    public string? PhoneNumber { get; set; }

    public string? PermanentAddress1 { get; set; }

    public string? PermanentAddress2 { get; set; }

    public string? PermanentAddress3 { get; set; }

    public string? PermanentCountry { get; set; }

    public string? PermanentState { get; set; }

    public string? PermanentPincode { get; set; }

    public string? PermanentPhoneNumber { get; set; }

    public string? Name { get; set; }

    public string? Relationship { get; set; }

    public int? LandLineNo { get; set; }

    public int? MobileNo { get; set; }

    public string? Email { get; set; }

    public int? OfficeNo { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedTime { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? UpdateTime { get; set; }

    public virtual Employee? Employee { get; set; }
}
