using Kryptos.Hrms.API.Models;

namespace Kryptos.Hrms.API.Providers
{
    public interface ILeaveAvailabilityProvider
    {
        Task<LeaveAvailability> GetLeaveAvailabilityByEmployeeIdAsync(int employeeId);
    }
}
