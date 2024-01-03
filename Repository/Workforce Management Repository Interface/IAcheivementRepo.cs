using Kryptos.Hrms.API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IAchievementRepository
{
    Task<List<Achievement>> GetAllAchievementsAsync();

    Task AddAchievementAsync(Achievement achievement);


}
