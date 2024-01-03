using global::Kryptos.Hrms.API.Models;
using System.Threading.Tasks;



namespace Kryptos.Hrms.API.Repository.InterfaceAttendance_LeaveRepository
{
   
    
        public interface ILeaveRepository
        {
        Task<LeaveType> CreateLeaveTypeByHRAdminAsync(int employeeId, string leaveTypeName);
    }
    

}
