using Kryptos.Hrms.API.Input_Models.CompensationAndBenefits;
using Kryptos.Hrms.API.Models;
using Kryptos.Hrms.API.Provider.CompensationAndBenefits;
using Kryptos.Hrms.API.Provider.CompensationAndBenefits_Interface;
using Kryptos.Hrms.API.Provider.ExpensesManagement;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Kryptos.Hrms.API.Controller.CompensationAndBenefits
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SalaryDetailsController : ControllerBase
    {
        private readonly ISalaryDetailsPro _control;

        private readonly KryptosHrmsDbContext _dbcontext;
        public SalaryDetailsController(ISalaryDetailsPro control, KryptosHrmsDbContext dbcontext)
        {
            _control = control;
            _dbcontext = dbcontext;
        }

        [HttpGet("GetSalaryDetailsPerMonth")]
        public async Task<IActionResult> GetSalaryDetailsPerMonth()
        {
            try
            {
                // Retrieve all salary details from the database
                var salaryDetails = await _dbcontext.SalaryDetails.ToListAsync();

                if (salaryDetails == null || salaryDetails.Count == 0)
                {
                    return NotFound("No salary details found.");
                }

                // Calculate the monthly values based on the yearly data
                var monthlySalaryDetails = salaryDetails.Select(sd => new
                {
                    sd.Id,
                    sd.EmployeeId,
                    sd.Ctcpy,
                    Ctcpm = sd.Ctcpy / 12,
                    BasicEarnings = sd.BasicEarnings / 12,
                    HRAEarnings = sd.HraEarnings / 12,
                    MedicalEarnings = sd.MedicalEarnings / 12,
                    ConveyanceEarnings = sd.ConveyanceEarnings / 12,
                    SpecialAllowance = sd.SpecialAllowance / 12,
                    ProvidentFund = sd.ProvidentFundDeduction / 12,
                    MediClaim = sd.Mediclaim / 12,
                    Arrear = sd.Arrear / 12,
                    EarnedBonus = sd.EarnedBonus / 12,
                    Compensation = sd.Compensation / 12,
                    // Divide other columns by 12 as needed
                }).ToList();

                return Ok(monthlySalaryDetails);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }


        [HttpGet]
        [Route("GetAllSalaryDetailsPerYear")]
        public async Task<IActionResult> GetAllSalaryDetailsPerYear()
        {
            try
            {
                // Retrieve all salary details from the database
                var salaryDetails = await _dbcontext.SalaryDetails.ToListAsync();

                if (salaryDetails == null || salaryDetails.Count == 0)
                {
                    return NotFound("No salary details found.");
                }

                return Ok(salaryDetails);
            }                                                                                                                                              
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error: " + ex.Message);
            }
        }



        [HttpPost]
        public async Task<IActionResult> AddSalary([FromBody] SalaryDetailsInput input)
        {
            // Fetch the employee based on the EmployeeId from the input (e.g., from a database)
            var employee = FetchEmployeeFromDatabase(input.EmployeeId);

            if (employee == null)
            {
                return NotFound("Employee not found.");
            }

            if (employee.Role == "Trainee")
            {
                var salaryDetail = new SalaryDetail
                {
                    EmployeeId = input.EmployeeId,
                    Ctcpy = input.Ctcpy,
                    Ctcpm = input.Ctcpy / 12,
                    
                    // Set other properties as needed
                };
                _dbcontext.SalaryDetails.Add(salaryDetail);

                // Save changes to the database
                await _dbcontext.SaveChangesAsync();
                return Ok();
            }
            else
            {
                // Your existing code for calculating salary components
                var earningsData = await _dbcontext.EarningsPercentageAllocations
                    .Select(e => new
                    {
                        e.BasicEarnings,
                        e.HraEarnings,
                        e.ConveyanceEarnings,
                        e.MedicalEarnings,
                        e.SpecialAllowance,
                        e.ProvidentFundDeduction,
                        e.Mediclaim
                    })
                    .ToListAsync();

                double ctcpm = input.Ctcpy / 12;
                double? basicEarnings = input.Ctcpy * earningsData.Sum(e => e.BasicEarnings);
                double? hraEarnings = input.Ctcpy * earningsData.Sum(e => e.HraEarnings);
                double? conveyanceEarnings = input.Ctcpy * earningsData.Sum(e => e.ConveyanceEarnings);
                double? medicalEarnings = input.Ctcpy * earningsData.Sum(e => e.MedicalEarnings);
                double? specialAllowance = input.Ctcpy * earningsData.Sum(e => e.SpecialAllowance);
                double? providentFundDeduction = input.Ctcpy * earningsData.Sum(e => e.ProvidentFundDeduction);
                double? mediClaim = input.Ctcpy * earningsData.Sum(e => e.Mediclaim);
                
                var salaryDetail = new SalaryDetail
                {
                    EmployeeId = input.EmployeeId,
                    Ctcpy = input.Ctcpy,
                    Ctcpm = input.Ctcpy / 12,
                    BasicEarnings = basicEarnings,
                    HraEarnings = hraEarnings,
                    ConveyanceEarnings = conveyanceEarnings,
                    MedicalEarnings = medicalEarnings,
                    SpecialAllowance = specialAllowance,
                    ProvidentFundDeduction = providentFundDeduction,
                    Mediclaim = mediClaim
                    // Set other properties as needed
                };
                _dbcontext.SalaryDetails.Add(salaryDetail);

                // Save changes to the database
                await _dbcontext.SaveChangesAsync();
                return Ok();
            }
        }

    

    private Employee FetchEmployeeFromDatabase(int? employeeId)
        {
            using (var context = new KryptosHrmsDbContext())
            {
                // Assuming your Employee model has a property named EmployeeId
                var employee = context.Employees.SingleOrDefault(e => e.Id == employeeId);
                return employee;
            }
        }

    }
}
