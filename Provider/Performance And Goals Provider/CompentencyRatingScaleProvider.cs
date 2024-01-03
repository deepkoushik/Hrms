using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Kryptos.Hrms.API.Models;
using Kryptos.Hrms.API.Repositories;

namespace Kryptos.Hrms.API.Provider
{
    public class CompentencyRatingScaleProvider : ICompentencyRatingScaleProvider
    {
        private readonly ICompetencyRatingScaleRepository _repository;

        public CompentencyRatingScaleProvider(ICompetencyRatingScaleRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<CompetencyRatingScale>> GetAllCompetencyRatingScalesAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<CompetencyRatingScale> GetCompetencyRatingScaleByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task AddCompetencyRatingScaleAsync(CompetencyRatingScale competencyRatingScale)
        {
            await _repository.AddAsync(competencyRatingScale);
        }

        public async Task UpdateCompetencyRatingScaleAsync(CompetencyRatingScale competencyRatingScale)
        {
            await _repository.UpdateAsync(competencyRatingScale);
        }

        public async Task DeleteCompetencyRatingScaleAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}
