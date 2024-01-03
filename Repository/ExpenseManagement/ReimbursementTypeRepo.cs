
using Kryptos.Hrms.API.Models;
using Kryptos.Hrms.API;
using Microsoft.EntityFrameworkCore;

namespace Kryptos.Hrms.API.Repository.ExpenseManagement
{
    public class ReimbursementTypeRepo : IReimbursementTypeRepo
    {
        private readonly KryptosHrmsDbContext _dbContext;

        public ReimbursementTypeRepo(KryptosHrmsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ReimbursementType GetReimbursementTypeById(int id)
        {
            return _dbContext.ReimbursementTypes.Find(id);
        }

        public IEnumerable<ReimbursementType> GetAllReimbursementTypes()
        {
            return _dbContext.ReimbursementTypes.ToList();
        }

        public void AddReimbursementType(ReimbursementType reimbursementType)
        {
            _dbContext.ReimbursementTypes.Add(reimbursementType);
            _dbContext.SaveChanges();
        }

        public void UpdateReimbursementType(ReimbursementType reimbursementType)
        {
            _dbContext.Entry(reimbursementType).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }

        public void DeleteReimbursementType(int id)
        {
            var reimbursementType = _dbContext.ReimbursementTypes.Find(id);
            if (reimbursementType != null)
            {
                _dbContext.ReimbursementTypes.Remove(reimbursementType);
                _dbContext.SaveChanges();
            }
        }
    }
}
