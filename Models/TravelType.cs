using System;
using System.Collections.Generic;

namespace Kryptos.Hrms.API.Models;

public partial class TravelType
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<ItineraryHistory> ItineraryHistories { get; } = new List<ItineraryHistory>();
}
