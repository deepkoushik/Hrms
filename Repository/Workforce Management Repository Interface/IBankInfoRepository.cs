using Kryptos.Hrms.API.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kryptos.Hrms.API.Repositories
{
    public interface IBankInfoRepository
    {
        Task<IEnumerable<BankInfo>> GetAllAsync();

        Task CreateAsync(BankInfo bankAccount);

    }
}
