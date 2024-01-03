using Kryptos.Hrms.API.Models;

namespace Kryptos.Hrms.API.Provider.ExpensesManagement
{
    public interface IReimbursementPro
    {
        IEnumerable<Reimbursement> GetAllReimbursements();
        void AddReimbursement(Reimbursement reimbursement);
        void UpdateReimbursement(Reimbursement reimbursement);
        void DeleteReimbursement(int id);
    }
}
