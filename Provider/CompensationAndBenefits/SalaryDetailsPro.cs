using Kryptos.Hrms.API.Models;
using Kryptos.Hrms.API.Provider.CompensationAndBenefits_Interface;
using Kryptos.Hrms.API.Repository.CompensationAndBenefits;
using Microsoft.EntityFrameworkCore;

namespace Kryptos.Hrms.API.Provider.CompensationAndBenefits
{
    public class SalaryDetailsPro : ISalaryDetailsPro
    {
        private readonly ISalaryDetailsRepo _provider;

        public SalaryDetailsPro(ISalaryDetailsRepo provider)
        {
            _provider = provider;
        }
        public IEnumerable<SalaryDetail> GetAllSalaryDetails()
        {
            return _provider.GetAllSalaryDetails();
        }

        public void AddSalaryDetails(SalaryDetail salaryDetails)
        {
            _provider.AddSalaryDetails(salaryDetails);
        }

        public void UpdateSalaryDetails(int id, SalaryDetail salaryDetails)
        {
            _provider.UpdateSalaryDetails(id, salaryDetails);
        }

        public void DeleteSalaryDetails(int id)
        {
            _provider.DeleteSalaryDetails(id);
        }
        public SalaryDetail GetEmployeeById(int? employeeId)
        {
           return _provider.GetEmployeeById(employeeId);
        }
    }
}
