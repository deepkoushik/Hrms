using Kryptos.Hrms.API.Models;
using Microsoft.EntityFrameworkCore;

using Kryptos.Hrms.API.Provider;
using Kryptos.Hrms.API.Repositories;
using Kryptos.Hrms.API.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Kryptos.Hrms.API.Repository.WorkforceManagementRepository
{
    public class CoreSkillRepository : ICoreSkillRepository
    {
        private readonly KryptosHrmsDbContext _dbContext;

        public CoreSkillRepository(KryptosHrmsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<CoreSkill>> GetAllCoreSkillsAsync()
        {
            return await _dbContext.CoreSkills.ToListAsync();
        }



        public async Task AddCoreSkillAsync(CoreSkill coreSkill)
        {
            _dbContext.CoreSkills.Add(coreSkill);
            await _dbContext.SaveChangesAsync();
        }



    }
}
