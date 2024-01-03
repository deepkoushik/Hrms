using Kryptos.Hrms.API.Input_Models.CompensationAndBenefits;
using Kryptos.Hrms.API.Models;

namespace Kryptos.Hrms.API.Repository.CompensationAndBenefits_Interface
{
    public interface IEarningsPercentageRepo
    {
        public Task<List<EarningsPercentageInput>> GetAllEarningsPercentageAllocations();

        public Task UpdateEarningsPercentageAllocationAsync(EarningsPercentageInput allocation);
    }


}
