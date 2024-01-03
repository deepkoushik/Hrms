using Kryptos.Hrms.API.Models;
using Kryptos.Hrms.API.Provider.WorkforceManagementproviderInterface;
using Kryptos.Hrms.API.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace Kryptos.Hrms.API.Providers
{
    public class BankInfoProvider : IBankInfoProvider
    {
        private readonly IBankInfoRepository _repository;

        public BankInfoProvider(IBankInfoRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<BankInfo>> GetAllBankInfoAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task AddBankInfoAsync(BankInfo bankInfo)
        {
            await _repository.CreateAsync(bankInfo);
        }
    }
}
