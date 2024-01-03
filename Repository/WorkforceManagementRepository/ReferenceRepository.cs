using Kryptos.Hrms.API.Models;
using Kryptos.Hrms.API.Repositories;
using Kryptos.Hrms.API.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kryptos.Hrms.API.Repository.WorkforceManagementRepository
{
    public class ReferenceRepository : IReferenceRepository
    {
        private readonly KryptosHrmsDbContext _dbContext;

        public ReferenceRepository(KryptosHrmsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Reference> GetReferenceByIdAsync(int id)
        {
            return await _dbContext.References.FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<List<Reference>> GetAllReferencesAsync()
        {
            return await _dbContext.References.ToListAsync();
        }

        public async Task AddReferenceAsync(Reference reference)
        {
            _dbContext.References.Add(reference);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateReferenceAsync(Reference reference)
        {
            _dbContext.Entry(reference).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteReferenceAsync(int id)
        {
            var referenceToDelete = await _dbContext.References.FindAsync(id);
            if (referenceToDelete != null)
            {
                _dbContext.References.Remove(referenceToDelete);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
