using Kryptos.Hrms.API.Input_Models.CompensationAndBenefits;
using Kryptos.Hrms.API.Models;
using Kryptos.Hrms.API.Repository.CompensationAndBenefits_Interface;
using Microsoft.EntityFrameworkCore;

namespace Kryptos.Hrms.API.Repository.CompensationAndBenefits
{
    public class EarningsPercentageRepo : IEarningsPercentageRepo
    {
        private readonly KryptosHrmsDbContext _context;

        public EarningsPercentageRepo(KryptosHrmsDbContext context)
        {
            _context = context;
        }

        public async Task<List<EarningsPercentageInput>> GetAllEarningsPercentageAllocations()
        {
            var allocations = await _context.EarningsPercentageAllocations.ToListAsync();
            var inputModels = allocations.Select(allocation => new EarningsPercentageInput
            {
                // Map properties from the allocation model to the input model
                Id=allocation.Id,
                BasicEarnings = allocation.BasicEarnings,
                HraEarnings = allocation.HraEarnings,
                ConveyanceEarnings= allocation.ConveyanceEarnings,
                MedicalEarnings=allocation.MedicalEarnings,
                SpecialAllowance=allocation.SpecialAllowance,
                ProvidentFundDeduction=allocation.ProvidentFundDeduction,
                Mediclaim=allocation.Mediclaim,
                UpdatedById=allocation.UpdatedById,
                UpdateTime=allocation.UpdateTime
                // Add more properties as needed
            }).ToList();

            return inputModels;
        }



        public async Task UpdateEarningsPercentageAllocationAsync(EarningsPercentageInput updatedAllocation)
        {
            try
            {
                // Find the existing allocation by its ID
                var existingAllocation = await _context.EarningsPercentageAllocations
                    .FirstOrDefaultAsync(a => a.Id == updatedAllocation.Id);

                if (existingAllocation == null)
                {
                    // Allocation not found
                    throw new Exception("EarningsPercentageAllocation not found.");
                }

                // Update the properties of the existing allocation with the values from updatedAllocation
                existingAllocation.BasicEarnings = updatedAllocation.BasicEarnings;
                existingAllocation.HraEarnings = updatedAllocation.HraEarnings;
                existingAllocation.ConveyanceEarnings = updatedAllocation.ConveyanceEarnings;
                existingAllocation.MedicalEarnings = updatedAllocation.MedicalEarnings;
                existingAllocation.SpecialAllowance = updatedAllocation.SpecialAllowance;
                existingAllocation.ProvidentFundDeduction = updatedAllocation.ProvidentFundDeduction;
                existingAllocation.Mediclaim = updatedAllocation.Mediclaim;
                existingAllocation.UpdatedById = updatedAllocation.UpdatedById;
                existingAllocation.UpdateTime = DateTime.Now; // You can set the update time as needed

                // Mark the entity as modified and save changes
                _context.Entry(existingAllocation).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Handle exceptions appropriately, e.g., log the error.
                throw new Exception("Error updating EarningsPercentageAllocation.", ex);
            }
        }
    }
}
