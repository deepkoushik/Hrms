using System;
using System.Collections.Generic;

namespace Kryptos.Hrms.API.Models;

public partial class Q3history
{
    public int Id { get; set; }

    public int? EmployeeId { get; set; }

    public DateTime? StartPeriod { get; set; }

    public DateTime? EndPeriod { get; set; }

    public string? Kratitle { get; set; }

    public int? Kraweightage { get; set; }

    public string? GoalComments { get; set; }

    public int? GoalCompetencyRatingScaleId { get; set; }

    public bool? GoalIsSubmited { get; set; }

    public int? ReviewedBy { get; set; }

    public int? ReviewedByCompetencyRatingScaleId { get; set; }

    public string? ReviewedByComments { get; set; }

    public bool? ReviewedByStatus { get; set; }

    public bool? IsActive { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? CreatedTime { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime? UpdateTime { get; set; }

    public virtual Employee? Employee { get; set; }

    public virtual CompetencyRatingScale? GoalCompetencyRatingScale { get; set; }
}
