using Kryptos.Hrms.API.Models;
using Kryptos.Hrms.API;

namespace Kryptos.Hrms.API
{
    public class ReimbursementRepo : IReimbursementRepo
    {
        private readonly KryptosHrmsDbContext _dbContext;

        public ReimbursementRepo(KryptosHrmsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Reimbursement> GetAllReimbursements()
        {
            return _dbContext.Reimbursements.ToList();
        }
        public void AddReimbursement(Reimbursement reimbursement)
        {
            reimbursement.CreatedTime= DateTime.Now;
            reimbursement.UpdateTime = null;
            reimbursement.UpdatedBy = null;
            _dbContext.Reimbursements.Add(reimbursement);
            _dbContext.SaveChanges();
        }

        public void UpdateReimbursement(Reimbursement reimbursement)
        {
            //reimbursement.EmployeeId = reimbursement.EmployeeId;
            //existingReimbursement.ApprovedById = reimbursement.ApprovedById;
            //existingReimbursement.ReferenceCode = reimbursement.ReferenceCode;
            //existingReimbursement.BillNumber = reimbursement.BillNumber;
            //existingReimbursement.BillDate = reimbursement.BillDate;
            //existingReimbursement.BillPeriod = reimbursement.BillPeriod;
            //existingReimbursement.Amount = reimbursement.Amount;
            //existingReimbursement.Description = reimbursement.Description;
            //existingReimbursement.BillDocument = reimbursement.BillDocument;
            //existingReimbursement.ReimbutsementTypeId = reimbursement.ReimbutsementTypeId;
            //existingReimbursement.AppliedDate = reimbursement.AppliedDate;
            //existingReimbursement.Status = reimbursement.Status;
            //existingReimbursement.ApprovedDate = reimbursement.ApprovedDate;
            //existingReimbursement.UpdatedBy = reimbursement.UpdatedBy;
            reimbursement.UpdateTime = DateTime.Now;
            _dbContext.Reimbursements.Update(reimbursement);
                // Update other properties similarly
                _dbContext.SaveChanges();
            
        }
        public void DeleteReimbursement(int id)
        {
            var reimbursement = _dbContext.Reimbursements.Find(id);
            if (reimbursement == null)
            {
                throw new Exception("no such reimbursement found");
            }
            else
            {
                _dbContext.Reimbursements.Remove(reimbursement);
                _dbContext.SaveChanges();
            }
        }
    }
}
