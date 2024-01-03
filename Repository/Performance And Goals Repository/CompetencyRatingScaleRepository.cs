using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Kryptos.Hrms.API.Models; // Make sure to include the correct namespace for your DbContext

namespace Kryptos.Hrms.API.Repositories
{
    public class CompetencyRatingScaleRepository : ICompetencyRatingScaleRepository
    {
        private readonly KryptosHrmsDbContext _dbContext;

        public CompetencyRatingScaleRepository(KryptosHrmsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<CompetencyRatingScale>> GetAllAsync()
        {
            return await _dbContext.CompetencyRatingScales.ToListAsync();
        }

        public async Task<CompetencyRatingScale> GetByIdAsync(int id)
        {
            return await _dbContext.CompetencyRatingScales.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task AddAsync(CompetencyRatingScale competencyRatingScale)
        {
            _dbContext.CompetencyRatingScales.Add(competencyRatingScale);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(CompetencyRatingScale competencyRatingScale)
        {
            _dbContext.Entry(competencyRatingScale).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var competencyRatingScale = await _dbContext.CompetencyRatingScales.FirstOrDefaultAsync(c => c.Id == id);
            if (competencyRatingScale != null)
            {
                _dbContext.CompetencyRatingScales.Remove(competencyRatingScale);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
