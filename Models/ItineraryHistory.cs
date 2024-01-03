using System;
using System.Collections.Generic;

namespace Kryptos.Hrms.API.Models;

public partial class ItineraryHistory
{
    public int Id { get; set; }

    public int? EmployeeId { get; set; }

    public string? IternaryCode { get; set; }

    public int? TravelTypeId { get; set; }

    public string? Trip { get; set; }

    public DateTime? JourneyFromDate { get; set; }

    public DateTime? JourneyToDate { get; set; }

    public string? PurposeOfTravel { get; set; }

    public int? AmountRequest { get; set; }

    public byte[]? CarBookingDocument { get; set; }

    public byte[]? BusBookingDocument { get; set; }

    public byte[]? TrainBookingDocument { get; set; }

    public byte[]? HotelBookingDocument { get; set; }

    public byte[]? PassportDocument { get; set; }

    public byte[]? VisaDocument { get; set; }

    public byte[]? Attachment { get; set; }

    public bool? IsRequested { get; set; }

    public bool? IsApproved { get; set; }

    public int? ApprovedById { get; set; }

    public byte[]? MailApprovalDocument { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedTime { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? UpdateTime { get; set; }

    public virtual Employee? Employee { get; set; }

    public virtual TravelType? TravelType { get; set; }
}
