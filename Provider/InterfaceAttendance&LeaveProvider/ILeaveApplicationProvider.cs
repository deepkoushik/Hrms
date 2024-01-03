using Kryptos.Hrms.API.Models;
using System;
using System.Threading.Tasks;

namespace Kryptos.Hrms.API.Providers.InterfaceAttendance_LeaveProvider
{
    public interface ILeaveApplicationProvider
    {
        Task PostLeaveApplicationAsync(int employeeId, string leaveType, DateTime startDate, DateTime endDate, string leaveReason, bool fromFirstHalf, bool fromSecondHalf, bool toFirstHalfDay, bool toSecondHalfDay, bool fromFullDay, bool medical , string alternateMobileNo);
        Task<IEnumerable<LeaveApplication>> GetLeaveApplicationsAsync(int employeeId);
        Task UpdateLeaveApplicationAsync(int leaveApplicationId, string leaveType, DateTime startDate, DateTime endDate, string leaveReason, bool fromFirstHalf, bool fromSecondHalf, bool toFirstHalfDay, bool toSecondHalfDay, bool fromFullDay, bool medical, string alternateMobileNo);
        Task UpdateLeaveApplicationStatusAsRejectionBySeniorManager(int employeeId, int leaveApplicationId, string leaveRejectionReason);
        Task UpdateLeaveApplicationStatusBySeniorManager(int employeeId, int leaveApplicationId);
        Task<List<LeaveApplication>> GetLeaveApplicationsOfEmployeeAsync(int employeeId);
    }
}
