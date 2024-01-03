using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Kryptos.Hrms.API.Models;

namespace Kryptos.Hrms.API.Provider
{
    public interface ICompentencyRatingScaleProvider
    {
        Task<IEnumerable<CompetencyRatingScale>> GetAllCompetencyRatingScalesAsync();
        Task<CompetencyRatingScale> GetCompetencyRatingScaleByIdAsync(int id);
        Task AddCompetencyRatingScaleAsync(CompetencyRatingScale competencyRatingScale);
        Task UpdateCompetencyRatingScaleAsync(CompetencyRatingScale competencyRatingScale);
        Task DeleteCompetencyRatingScaleAsync(int id);
    }
}
