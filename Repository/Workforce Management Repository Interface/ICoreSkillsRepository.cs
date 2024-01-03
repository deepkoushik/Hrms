using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Kryptos.Hrms.API.Models;

namespace Kryptos.Hrms.API.Repositories
{
    public interface ICoreSkillRepository
    {
        Task<IEnumerable<CoreSkill>> GetAllCoreSkillsAsync();
        Task AddCoreSkillAsync(CoreSkill coreSkill);

    }
}
