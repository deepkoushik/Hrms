using Kryptos.Hrms.API.Models;

namespace Kryptos.Hrms.API;

    public interface IReimbursementRepo
    {
        IEnumerable<Reimbursement> GetAllReimbursements();
        void AddReimbursement(Reimbursement reimbursement);
        void UpdateReimbursement(Reimbursement reimbursement);
        void DeleteReimbursement(int id);
    }

