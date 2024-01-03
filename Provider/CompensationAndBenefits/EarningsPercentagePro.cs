using Kryptos.Hrms.API.Input_Models.CompensationAndBenefits;
using Kryptos.Hrms.API.Models;
using Kryptos.Hrms.API.Provider.CompensationAndBenefits_Interface;
using Kryptos.Hrms.API.Repository.CompensationAndBenefits_Interface;

namespace Kryptos.Hrms.API.Provider.CompensationAndBenefits
{
    public class EarningsPercentagePro : IEarningsPercentagePro
    {
        private readonly IEarningsPercentageRepo _Repo;

        public EarningsPercentagePro(IEarningsPercentageRepo repo)
        {
            _Repo = repo;
        }

        public async Task<List<EarningsPercentageInput>> GetAllEarningsPercentageAllocations()
        {
            return await _Repo.GetAllEarningsPercentageAllocations();
        }
        public async Task UpdateEarningsPercentageAllocationAsync(EarningsPercentageInput allocation)
        {
           await _Repo.UpdateEarningsPercentageAllocationAsync(allocation);
        }
    }
}
