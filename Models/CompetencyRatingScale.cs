using System;
using System.Collections.Generic;

namespace Kryptos.Hrms.API.Models;

public partial class CompetencyRatingScale
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public int? Score { get; set; }

    public virtual ICollection<Q1history> Q1histories { get; } = new List<Q1history>();

    public virtual ICollection<Q2history> Q2histories { get; } = new List<Q2history>();

    public virtual ICollection<Q3history> Q3histories { get; } = new List<Q3history>();

    public virtual ICollection<Q4history> Q4histories { get; } = new List<Q4history>();
}
