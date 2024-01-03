using Kryptos.Hrms.API.Models;

namespace Kryptos.Hrms.API
{
    public interface IReimbursementTypeRepo
    {
        ReimbursementType GetReimbursementTypeById(int id);
        IEnumerable<ReimbursementType> GetAllReimbursementTypes();
        void AddReimbursementType(ReimbursementType reimbursementType);
        void UpdateReimbursementType(ReimbursementType reimbursementType);
        void DeleteReimbursementType(int id);
    }
}
