using Kryptos.Hrms.API.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kryptos.Hrms.API.Repositories
{
    public interface ICompetencyRatingScaleRepository
    {
        Task<IEnumerable<CompetencyRatingScale>> GetAllAsync();
        Task<CompetencyRatingScale> GetByIdAsync(int id);
        Task AddAsync(CompetencyRatingScale competencyRatingScale);
        Task UpdateAsync(CompetencyRatingScale competencyRatingScale);
        Task DeleteAsync(int id);
    }
}
