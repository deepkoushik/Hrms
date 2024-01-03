using System;
using System.Collections.Generic;

namespace Kryptos.Hrms.API.Models;

public partial class IdentificationDetail
{
    public int Id { get; set; }

    public int? EmployeeId { get; set; }

    public byte[]? Image { get; set; }

    public string? Pannumber { get; set; }

    public string? NameAsPerPan { get; set; }

    public byte[]? Panattachment { get; set; }

    public string? AadharCardNumber { get; set; }

    public string? AadharCardName { get; set; }

    public byte[]? AadharAttachment { get; set; }

    public string? Pfnumber { get; set; }

    public string? Esinumber { get; set; }

    public string? ElectionCard { get; set; }

    public string? RationCard { get; set; }

    public string? NationalPopularRegister { get; set; }

    public string? LicenseType { get; set; }

    public string? LicenseValidity { get; set; }

    public string? LicenseNo { get; set; }

    public string? LicensePlaceOfIssue { get; set; }

    public DateTime? LicenseIssuedOn { get; set; }

    public DateTime? LicenseExpirationDate { get; set; }

    public byte[]? LicenseAttachment { get; set; }

    public string? PassportNumber { get; set; }

    public string? NameInPassport { get; set; }

    public string? PassportPlaceOfIssue { get; set; }

    public string? PassportEcntstatus { get; set; }

    public DateTime? PassportIssuedOn { get; set; }

    public string? PassportValidity { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedTime { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? UpdateTime { get; set; }

    public virtual Employee? Employee { get; set; }
}
