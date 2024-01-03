using Kryptos.Hrms.API.Models;
using Kryptos.Hrms.API.Providers.InterfaceAttendance_LeaveProvider;
using Kryptos.Hrms.API.Repository.InterfaceAttendance_LeaveRepository;
using NuGet.Protocol.Core.Types;
using System;
using System.Threading.Tasks;

namespace Kryptos.Hrms.API.Providers.Attendance_LeaveClassProvider
{
    public class LeaveApplicationProvider : ILeaveApplicationProvider
    {
        private readonly ILeaveApplicationRepository _leaveApplicationRepository;

        public LeaveApplicationProvider(ILeaveApplicationRepository leaveApplicationRepository)
        {
            _leaveApplicationRepository = leaveApplicationRepository;
        }

        public async Task PostLeaveApplicationAsync(int employeeId, string leaveType, DateTime startDate, DateTime endDate, string leaveReason, bool fromFirstHalf, bool fromSecondHalf, bool toFirstHalfDay, bool toSecondHalfDay, bool fromFullDay, bool medical, string alternateMobileNo)
        {
            await _leaveApplicationRepository.PostLeaveApplicationAsync(employeeId, leaveType, startDate, endDate, leaveReason, fromFirstHalf, fromSecondHalf, toFirstHalfDay, toSecondHalfDay, fromFullDay, medical,alternateMobileNo);
        }

        public async Task<IEnumerable<LeaveApplication>> GetLeaveApplicationsAsync(int employeeId)
        {
            return await _leaveApplicationRepository.GetLeaveApplicationsAsync(employeeId);
        }
        public async Task UpdateLeaveApplicationAsync(int leaveApplicationId, string leaveType, DateTime startDate, DateTime endDate, string leaveReason, bool fromFirstHalf, bool fromSecondHalf, bool toFirstHalfDay, bool toSecondHalfDay, bool fromFullDay, bool medical, string alternateMobileNo)
        {
            await _leaveApplicationRepository.UpdateLeaveApplicationAsync(leaveApplicationId, leaveType, startDate, endDate, leaveReason, fromFirstHalf, fromSecondHalf, toFirstHalfDay, toSecondHalfDay, fromFullDay, medical, alternateMobileNo);
        }

        public async Task UpdateLeaveApplicationStatusAsRejectionBySeniorManager(int employeeId, int leaveApplicationId, string leaveRejectionReason)
        {
            await _leaveApplicationRepository.UpdateLeaveApplicationStatusAsRejectionBySeniorManager(employeeId, leaveApplicationId, leaveRejectionReason);
        }

        public async Task UpdateLeaveApplicationStatusBySeniorManager(int employeeId, int leaveApplicationId) 
        {
            await _leaveApplicationRepository.UpdateLeaveApplicationStatusBySeniorManager(employeeId, leaveApplicationId);
        }

        public async Task<List<LeaveApplication>> GetLeaveApplicationsOfEmployeeAsync(int employeeId)
        {
            return await _leaveApplicationRepository.GetLeaveApplicationsOfEmployeeAsync(employeeId);
        }


    }
}
