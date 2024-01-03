using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Kryptos.Hrms.API.Repositories;
using Kryptos.Hrms.API.Models;
using Kryptos.Hrms.API.Repositories;
using Kryptos.Hrms.API.Provider.WorkforceManagementproviderInterface;

namespace Kryptos.Hrms.API.Provider
{
    public class ResignationProvider : IResignationProvider
    {
        private readonly IResignationRepository _repository;

        public ResignationProvider(IResignationRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Resignation>> GetAllResignationsAsync()
        {
            return await _repository.GetAllResignationsAsync();
        }

        public async Task<Resignation> GetResignationByIdAsync(int id)
        {
            return await _repository.GetResignationByIdAsync(id);
        }

        public async Task CreateResignationAsync(Resignation resignation)
        {
            // You can add additional logic or validation here if needed
            await _repository.CreateResignationAsync(resignation);
        }

        public async Task UpdateResignationAsync(Resignation resignation)
        {
            // You can add additional logic or validation here if needed
            await _repository.UpdateResignationAsync(resignation);
        }

        public async Task DeleteResignationAsync(int id)
        {
            // You can add additional logic or validation here if needed
            await _repository.DeleteResignationAsync(id);
        }
    }
}
