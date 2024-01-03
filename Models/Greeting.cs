using System;
using System.Collections.Generic;

namespace Kryptos.Hrms.API.Models;

public partial class Greeting
{
    public int Id { get; set; }

    public int? EmployeeId { get; set; }

    public DateTime? FromDateTime { get; set; }

    public DateTime? ToDateTime { get; set; }

    public bool? IsInitiated { get; set; }

    public int? GreetingSentId { get; set; }

    public string? Message { get; set; }

    public bool? IsActive { get; set; }

    public int? GreetingCardId { get; set; }

    public int? GreetingTypeId { get; set; }

    public int? CreatedBy { get; set; }

    public DateTime? CreatedTime { get; set; }

    public int? UpdatedBy { get; set; }

    public DateTime? UpdateTime { get; set; }

    public virtual Employee? Employee { get; set; }

    public virtual GreetingCard? GreetingCard { get; set; }

    public virtual GreetingType? GreetingType { get; set; }
}
