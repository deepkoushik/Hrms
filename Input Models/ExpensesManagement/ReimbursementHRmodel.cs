using Kryptos.Hrms.API.Models;

namespace Kryptos.Hrms.API.Input_Models.ExpensesManagement
{
    public partial class ReimbursementHRmodel
    {
        public int Id { get; set; }

        public int? ApprovedById { get; set; }

        public string? Reason { get; set; }

        public DateTime? ApprovedDate { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? UpdateTime { get; set; }


        public bool? IsApprovedByHr { get; set; }

    }

}
