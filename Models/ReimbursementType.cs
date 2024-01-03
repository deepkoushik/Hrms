using System;
using System.Collections.Generic;

namespace Kryptos.Hrms.API.Models;

public partial class ReimbursementType
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Reimbursement> Reimbursements { get; } = new List<Reimbursement>();
}
