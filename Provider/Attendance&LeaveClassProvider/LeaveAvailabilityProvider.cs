using Kryptos.Hrms.API.Models;
using Kryptos.Hrms.API.Repository;

namespace Kryptos.Hrms.API.Providers.Attendance_LeaveClassProvider
{
    public class LeaveAvailabilityProvider : ILeaveAvailabilityProvider  // Implement the interface
    {
        private readonly ILeaveAvailabilityRepository _leaveAvailabilityRepository;

        public LeaveAvailabilityProvider(ILeaveAvailabilityRepository leaveAvailabilityRepository)
        {
            _leaveAvailabilityRepository = leaveAvailabilityRepository;
        }

        public async Task<LeaveAvailability> GetLeaveAvailabilityByEmployeeIdAsync(int employeeId)
        {
            return await _leaveAvailabilityRepository.GetLeaveAvailabilityByEmployeeIdAsync(employeeId);
        }
    }
}
