using Kryptos.Hrms.API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kryptos.Hrms.API.Provider.WorkforceManagementproviderInterface
{
    public interface IBankInfoProvider
    {
        Task<IEnumerable<BankInfo>> GetAllBankInfoAsync();
        Task AddBankInfoAsync(BankInfo bankInfo);
    }
}
