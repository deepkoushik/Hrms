using Kryptos.Hrms.API.Models;


using Kryptos.Hrms.API.Provider;
using Kryptos.Hrms.API.Provider.WorkforceManagementproviderInterface;
using Kryptos.Hrms.API.Repositories;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kryptos.Hrms.API.Providers
{
    public class CoreSkillProvider : ICoreSkillProvider
    {
        private readonly ICoreSkillRepository _repository;

        public CoreSkillProvider(ICoreSkillRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<CoreSkill>> GetAllCoreSkillsAsync()
        {
            return await _repository.GetAllCoreSkillsAsync();
        }

        public async Task AddCoreSkillAsync(CoreSkill coreSkill)
        {
            await _repository.AddCoreSkillAsync(coreSkill);
        }
    }
}
