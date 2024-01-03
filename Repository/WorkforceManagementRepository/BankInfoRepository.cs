using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kryptos.Hrms.API.Models;
using Microsoft.EntityFrameworkCore;
using Kryptos.Hrms.API.Repositories;

namespace Kryptos.Hrms.API.Repository.WorkforceManagementRepository
{
    public class BankInfoRepository : IBankInfoRepository
    {
        private readonly KryptosHrmsDbContext _dbContext;

        public BankInfoRepository(KryptosHrmsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<BankInfo>> GetAllAsync()
        {
            return await _dbContext.BankInfos.ToListAsync();
        }


        public async Task CreateAsync(BankInfo bankAccount)
        {
            _dbContext.BankInfos.Add(bankAccount);
            await _dbContext.SaveChangesAsync();
        }



    }
}
