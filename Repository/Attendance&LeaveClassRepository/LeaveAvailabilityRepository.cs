using System;
using System.Linq;
using System.Threading.Tasks;
using Kryptos.Hrms.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Kryptos.Hrms.API.Repository
{
    public class LeaveAvailabilityRepository : ILeaveAvailabilityRepository
    {
        private readonly KryptosHrmsDbContext _dbContext;

        public LeaveAvailabilityRepository(KryptosHrmsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<LeaveAvailability> GetLeaveAvailabilityByEmployeeIdAsync(int employeeId)
        {
            return await _dbContext.LeaveAvailabilities
                .Where(la => la.EmployeeId == employeeId)
                .FirstOrDefaultAsync();
        }
    }
}
