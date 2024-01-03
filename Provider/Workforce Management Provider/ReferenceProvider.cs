using Kryptos.Hrms.API.Models;
using Kryptos.Hrms.API.Provider.WorkforceManagementproviderInterface;
using Kryptos.Hrms.API.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kryptos.Hrms.API
{
    public class ReferenceProvider : IReferenceProvider
    {
        private readonly IReferenceRepository _repository;

        public ReferenceProvider(IReferenceRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Reference>> GetAllReferencesAsync()
        {
            return await _repository.GetAllReferencesAsync();
        }

        public async Task AddReferenceAsync(Reference reference)
        {
            await _repository.AddReferenceAsync(reference);
        }

        public async Task UpdateReferenceAsync(Reference reference)
        {
            await _repository.UpdateReferenceAsync(reference);
        }

        public async Task DeleteReferenceAsync(int id)
        {
            await _repository.DeleteReferenceAsync(id);
        }

        public async Task<Reference> GetReferenceByIdAsync(int id)
        {
            return await _repository.GetReferenceByIdAsync(id);
        }
    }
}
