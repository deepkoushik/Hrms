using Kryptos.Hrms.API.Input_Models.CompensationAndBenefits;
using Kryptos.Hrms.API.Models;

namespace Kryptos.Hrms.API.Provider.CompensationAndBenefits_Interface
{
    public interface IEarningsPercentagePro
    {
        public Task<List<EarningsPercentageInput>> GetAllEarningsPercentageAllocations();
        public Task UpdateEarningsPercentageAllocationAsync(EarningsPercentageInput allocation);
    }
}
