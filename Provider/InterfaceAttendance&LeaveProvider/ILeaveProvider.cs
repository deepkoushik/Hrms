using System.Threading.Tasks;
using global::Kryptos.Hrms.API.Models;


namespace Kryptos.Hrms.API.Provider.InterfaceAttendance_LeaveProvider
{


    public interface ILeaveProvider
    {
        Task<LeaveType> CreateALeaveTypeByHRAdminAsync(int employeeId, string leaveTypeName);
    }


}
