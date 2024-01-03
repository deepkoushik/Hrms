using System;
using System.Threading.Tasks;
using Kryptos.Hrms.API.Models;


namespace Kryptos.Hrms.API.Repository  // Match the namespace with the class
{
    public interface ILeaveAvailabilityRepository
    {

        Task<LeaveAvailability> GetLeaveAvailabilityByEmployeeIdAsync(int employeeId);
    }

}