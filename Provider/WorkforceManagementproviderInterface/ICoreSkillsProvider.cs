using Kryptos.Hrms.API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kryptos.Hrms.API.Provider.WorkforceManagementproviderInterface
{
    public interface ICoreSkillProvider
    {
        Task<IEnumerable<CoreSkill>> GetAllCoreSkillsAsync();
        Task AddCoreSkillAsync(CoreSkill coreSkill);
    }
}
