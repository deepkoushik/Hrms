namespace Kryptos.Hrms.API.Input_Models.ExpensesManagement
{
    public class ReimbursementFTModel
    {
        public int Id { get; set; }

        public int? ApprovedById { get; set; }

        public string? Reason { get; set; }

        public DateTime? ApprovedDate { get; set; }

        public int? UpdatedBy { get; set; }

        public DateTime? UpdateTime { get; set; }


        public bool? IsApprovedByFinanceTeam { get; set; }

    }

}
