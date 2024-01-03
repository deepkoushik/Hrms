using System;
using System.Collections.Generic;

namespace Kryptos.Hrms.API.Models;

public partial class ShiftType
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public int? GeneralShiftId { get; set; }

    public int? WeekOffId { get; set; }

    public int? HolidayId { get; set; }

    public virtual ICollection<Attendance> Attendances { get; } = new List<Attendance>();

    public virtual GeneralShift? GeneralShift { get; set; }

    public virtual Holiday? Holiday { get; set; }

    public virtual WeekOff? WeekOff { get; set; }
}
