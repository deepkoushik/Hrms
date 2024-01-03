using Kryptos.Hrms.API.Models;

namespace Kryptos.Hrms.API.Repository.CompensationAndBenefits
{
    public class SalaryDetailsRepo : ISalaryDetailsRepo
    {
        private readonly KryptosHrmsDbContext _dbContext;

        public SalaryDetailsRepo(KryptosHrmsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<SalaryDetail> GetAllSalaryDetails()
        {
            return _dbContext.SalaryDetails.ToList();
        }
        public void AddSalaryDetails(SalaryDetail salaryDetails)
        {
            _dbContext.SalaryDetails.Add(salaryDetails);
            _dbContext.SaveChanges();
        }

        public void UpdateSalaryDetails(int id, SalaryDetail salaryDetails)
        {
            var existingSalaryDetails = _dbContext.SalaryDetails.FirstOrDefault(r => r.Id == id);
            if (existingSalaryDetails != null)
            {
                existingSalaryDetails.EmployeeId = salaryDetails.EmployeeId;
                existingSalaryDetails.Ctcpy = salaryDetails.Ctcpy;
                existingSalaryDetails.Ctcpm = salaryDetails.Ctcpm;
                existingSalaryDetails.BasicEarnings = salaryDetails.BasicEarnings;
                existingSalaryDetails.HraEarnings = salaryDetails.HraEarnings;
                existingSalaryDetails.ConveyanceEarnings = salaryDetails.ConveyanceEarnings;
                existingSalaryDetails.MedicalEarnings = salaryDetails.MedicalEarnings;
                existingSalaryDetails.SpecialAllowance = salaryDetails.SpecialAllowance;
                existingSalaryDetails.EarnedBonus = salaryDetails.EarnedBonus;
                existingSalaryDetails.Arrear = salaryDetails.Arrear;
                existingSalaryDetails.ProvidentFundDeduction = salaryDetails.ProvidentFundDeduction;
                existingSalaryDetails.Mediclaim = salaryDetails.Mediclaim;
                existingSalaryDetails.Compensation = salaryDetails.Compensation;
                existingSalaryDetails.CreatedBy = salaryDetails.CreatedBy;
                existingSalaryDetails.CreatedTime = salaryDetails.CreatedTime;
                existingSalaryDetails.UpdatedBy = salaryDetails.UpdatedBy;
                existingSalaryDetails.UpdateTime = salaryDetails.UpdateTime;
                // Update other properties similarly
                _dbContext.SaveChanges();
            }
        }
        public void DeleteSalaryDetails(int id)
        {
            var salary = _dbContext.SalaryDetails.Find(id);
            if (salary != null)
            {
                _dbContext.SalaryDetails.Remove(salary);
                _dbContext.SaveChanges();
            }
        }
        public SalaryDetail GetEmployeeById(int? employeeId)
        {
            return _dbContext.SalaryDetails.FirstOrDefault(r => r.EmployeeId == employeeId);
        }
    }
}
