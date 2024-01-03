using Kryptos.Hrms.API.Models;
using Kryptos.Hrms.API.Repository.ExpenseManagement;
using Kryptos.Hrms.API;

namespace Kryptos.Hrms.API.Provider.ExpensesManagement
{
    public class ReimbursementPro : IReimbursementPro
    {
        private readonly IReimbursementRepo _provider;

        public ReimbursementPro(IReimbursementRepo provider)
        {
            _provider = provider;
        }

        public void AddReimbursement(Reimbursement reimbursement) => _provider.AddReimbursement(reimbursement);

        public void DeleteReimbursement(int id) => _provider.DeleteReimbursement(id);

        public IEnumerable<Reimbursement> GetAllReimbursements()
        {
            return _provider.GetAllReimbursements();
        }

        public void UpdateReimbursement(Reimbursement reimbursement) => _provider.UpdateReimbursement( reimbursement);
    }
}
