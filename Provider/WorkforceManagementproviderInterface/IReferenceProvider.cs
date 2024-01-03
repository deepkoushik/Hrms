using Kryptos.Hrms.API.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kryptos.Hrms.API.Provider.WorkforceManagementproviderInterface
{
    public interface IReferenceProvider
    {
        Task<Reference> GetReferenceByIdAsync(int id);
        Task<IEnumerable<Reference>> GetAllReferencesAsync();
        Task AddReferenceAsync(Reference reference);
        Task UpdateReferenceAsync(Reference reference);
        Task DeleteReferenceAsync(int id);
    }
}