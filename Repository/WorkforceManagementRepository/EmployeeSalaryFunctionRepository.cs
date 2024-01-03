using Kryptos.Hrms.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Kryptos.Hrms.API.Repository.WorkforceManagementRepository
{
    public class EmployeeSalaryFunctionRepository
    {
        private readonly KryptosHrmsDbContext _context;

        public EmployeeSalaryFunctionRepository(KryptosHrmsDbContext context)
        {
            _context = context;
        }

        public void SetEmployeeSalary(Employee employee, double ctcpy)
        {
            if (employee == null)
            {
                throw new ArgumentNullException(nameof(employee), "Employee cannot be null.");
            }

            double ctcpm = ctcpy / 12;

            var salaryDetail = new SalaryDetail
            {
                EmployeeId = employee.Id,
                Ctcpy = ctcpy,
                Ctcpm = ctcpm,
                BasicEarnings = 0,  // Initialize to 0 for "Software Engineer Trainee"
                HraEarnings = 0,    // Initialize to 0 for "Software Engineer Trainee"
                ConveyanceEarnings = 0,  // Initialize to 0 for "Software Engineer Trainee"
                MedicalEarnings = 0,      // Initialize to 0 for "Software Engineer Trainee"
                SpecialAllowance = 0,     // Initialize to 0 for "Software Engineer Trainee"
                ProvidentFundDeduction = 0, // Initialize to 0 for "Software Engineer Trainee"
                Mediclaim = 0             // Initialize to 0 for "Software Engineer Trainee"
            };

            if (employee.Role != "Software Engineer Trainee")
            {
                // Fetch earnings percentage allocations from the database based on the given role
                var allocation = _context.EarningsPercentageAllocations
                    .FromSqlInterpolated($"SELECT * FROM EarningsPercentageAllocations")
                    .FirstOrDefault();

                if (allocation == null)
                {
                    throw new Exception("Earnings percentage allocations not found for the employee's role.");
                }

                // Calculate earnings based on the provided annual salary (ctcpy) and allocation percentages
                salaryDetail.BasicEarnings = ctcpy * allocation.BasicEarnings ?? 0;
                salaryDetail.HraEarnings = ctcpy * allocation.HraEarnings ?? 0;
                salaryDetail.ConveyanceEarnings = ctcpy * allocation.ConveyanceEarnings ?? 0;
                salaryDetail.MedicalEarnings = ctcpy * allocation.MedicalEarnings ?? 0;
                salaryDetail.SpecialAllowance = ctcpy * allocation.SpecialAllowance ?? 0;
                salaryDetail.ProvidentFundDeduction = ctcpy * allocation.ProvidentFundDeduction ?? 0;
                salaryDetail.Mediclaim = ctcpy * allocation.Mediclaim ?? 0;
            }
            employee.SalaryDetails.Add(salaryDetail);
        }
    }
}