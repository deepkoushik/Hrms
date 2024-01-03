using Kryptos.Hrms.API.Models;

namespace Kryptos.Hrms.API.Repository.CompensationAndBenefits
{
    public interface ISalaryDetailsRepo
    {
        IEnumerable<SalaryDetail> GetAllSalaryDetails();
        SalaryDetail GetEmployeeById(int? employeeId);
        void AddSalaryDetails(SalaryDetail salaryDetails);
        void UpdateSalaryDetails(int id, SalaryDetail salaryDetails);
        void DeleteSalaryDetails(int id);
    }
}
