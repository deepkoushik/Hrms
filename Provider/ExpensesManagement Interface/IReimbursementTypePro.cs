using Kryptos.Hrms.API.Models;

namespace Kryptos.Hrms.API.Provider.ExpensesManagement
{
    public interface IReimbursementTypePro
    {
        ReimbursementType GetReimbursementTypeById(int id);
        IEnumerable<ReimbursementType> GetAllReimbursementTypes();
        void AddReimbursementType(ReimbursementType reimbursementType);
        void UpdateReimbursementType(int id, ReimbursementType reimbursementType);
        void DeleteReimbursementType(int id);
    }
}
