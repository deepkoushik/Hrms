using Kryptos.Hrms.API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Kryptos.Hrms.API.Provider.WorkforceManagementproviderInterface;

public class AchievementProvider : IAchievementProvider
{
    private readonly IAchievementRepository _repository;

    public AchievementProvider(IAchievementRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<Achievement>> GetAllAchievementsAsync()
    {
        return await _repository.GetAllAchievementsAsync();
    }

    public async Task AddAchievementAsync(Achievement achievement)
    {
        await _repository.AddAchievementAsync(achievement);
    }
}
