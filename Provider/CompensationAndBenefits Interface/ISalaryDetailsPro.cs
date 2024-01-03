using Kryptos.Hrms.API.Models;

namespace Kryptos.Hrms.API.Provider.CompensationAndBenefits_Interface
{
    public interface ISalaryDetailsPro
    {
        IEnumerable<SalaryDetail> GetAllSalaryDetails();
        public SalaryDetail GetEmployeeById(int? employeeId);
        void AddSalaryDetails(SalaryDetail salaryDetails);
        void UpdateSalaryDetails(int id, SalaryDetail salaryDetails);
        void DeleteSalaryDetails(int id);
    }
}
