using Kryptos.Hrms.API.Models;
using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class AchievementRepository : IAchievementRepository
{
    private readonly KryptosHrmsDbContext _context;

    public AchievementRepository(KryptosHrmsDbContext context)
    {
        _context = context;
    }

    public async Task<List<Achievement>> GetAllAchievementsAsync()
    {
        return await _context.Achievements.ToListAsync();
    }


    public async Task AddAchievementAsync(Achievement achievement)
    {
        await _context.Achievements.AddAsync(achievement);
        await _context.SaveChangesAsync();
    }


}
