using System.Threading.Tasks;
using global::Kryptos.Hrms.API.Models;
using global::Kryptos.Hrms.API.Provider.InterfaceAttendance_LeaveProvider;
using global::Kryptos.Hrms.API.Repository.InterfaceAttendance_LeaveRepository;
using Kryptos.Hrms.API.Models;
using Kryptos.Hrms.API.Repositories;

namespace Kryptos.Hrms.API.Provider.Attendance_LeaveClassProvider
{
    

    
        public class LeaveProvider : ILeaveProvider
        {
            private readonly ILeaveRepository _leaveRepository;

            public LeaveProvider(ILeaveRepository leaveRepository)
            {
                _leaveRepository = leaveRepository;
            }

        public async Task<LeaveType> CreateALeaveTypeByHRAdminAsync(int employeeId, string leaveTypeName)
        {
            return await _leaveRepository.CreateLeaveTypeByHRAdminAsync(employeeId, leaveTypeName);
        }
    }
    

}
