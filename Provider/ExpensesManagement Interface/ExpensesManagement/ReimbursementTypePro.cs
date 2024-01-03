using Kryptos.Hrms.API.Models;
using Kryptos.Hrms.API.Repository.ExpenseManagement;
using Kryptos.Hrms.API;

namespace Kryptos.Hrms.API.Provider.ExpensesManagement
{
    public class ReimbursementTypePro : IReimbursementTypePro
    {
        private readonly IReimbursementTypeRepo _repository;

        public ReimbursementTypePro(IReimbursementTypeRepo repository)
        {
            _repository = repository;
        }

        public ReimbursementType GetReimbursementTypeById(int id)
        {
            return _repository.GetReimbursementTypeById(id);
        }

        public IEnumerable<ReimbursementType> GetAllReimbursementTypes()
        {
            return _repository.GetAllReimbursementTypes();
        }

        public void AddReimbursementType(ReimbursementType reimbursementType)
        {
            // You can add custom business logic here if needed.
            _repository.AddReimbursementType(reimbursementType);
        }

        public void UpdateReimbursementType(int id, ReimbursementType reimbursementType)
        {
            // You can add custom business logic here if needed.
            _repository.UpdateReimbursementType(reimbursementType);
        }

        public void DeleteReimbursementType(int id)
        {
            // You can add custom business logic here if needed.
            _repository.DeleteReimbursementType(id);
        }

    }
}
