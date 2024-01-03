using Kryptos.Hrms.API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace Kryptos.Hrms.API.Provider.WorkforceManagementproviderInterface
{
    public interface IAchievementProvider
    {
        Task<List<Achievement>> GetAllAchievementsAsync();
        Task AddAchievementAsync(Achievement achievement);
    }

}
