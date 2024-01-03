using Kryptos.Hrms.API.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kryptos.Hrms.API.Repositories
{
    public interface IReferenceRepository
    {
        Task<Reference> GetReferenceByIdAsync(int id);
        Task<List<Reference>> GetAllReferencesAsync();
        Task AddReferenceAsync(Reference reference);
        Task UpdateReferenceAsync(Reference reference);
        Task DeleteReferenceAsync(int id);
    }
}
