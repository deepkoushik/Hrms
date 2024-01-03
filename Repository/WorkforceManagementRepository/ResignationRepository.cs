 // You may need to adjust the namespace depending on your project structure
using Kryptos.Hrms.API.Models;
using Kryptos.Hrms.API.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kryptos.Hrms.API.Repositories
{
    public class ResignationRepository : IResignationRepository
    {
        private readonly KryptosHrmsDbContext _context;

        public ResignationRepository(KryptosHrmsDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Resignation>> GetAllResignationsAsync()
        {
            return await _context.Resignations
                .Include(r => r.Employee) // Include related Employee data if needed
                .ToListAsync();
        }

        public async Task<Resignation> GetResignationByIdAsync(int id)
        {
            return await _context.Resignations
                .Include(r => r.Employee) // Include related Employee data if needed
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task CreateResignationAsync(Resignation resignation)
        {
            _context.Resignations.Add(resignation);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateResignationAsync(Resignation resignation)
        {
            _context.Entry(resignation).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteResignationAsync(int id)
        {
            var resignation = await _context.Resignations.FindAsync(id);
            if (resignation != null)
            {
                _context.Resignations.Remove(resignation);
                await _context.SaveChangesAsync();
            }
        }
    }
}
