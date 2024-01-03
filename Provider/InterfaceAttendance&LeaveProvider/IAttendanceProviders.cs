using System;
using System.Threading.Tasks;
using Kryptos.Hrms.API.Models;

public interface IAttendanceProvider
{
    Task<Attendance> CheckIn(int employeeId);
    Task<Attendance> CheckOut(int employeeId);
    List<Attendance> GetAttendanceDetails(int employeeId);
    List<Attendance> GetAttendanceDetailsByEmployeeId(int employeeId);
    Task UpdateRegularizationByAttendanceId(int attendanceId, bool firstHalfAttendance, bool secondHalfAttendance, string reasonForRegularization);
    Task<List<Attendance>> GetTheAttendanceListByEmployeeId(int employeeId);
    Task UpdateRegularizationStatusAsRejectionBySeniorManager(int employeeId, int attendanceId, string lineManagerRejectionReason);
    Task UpdateRegularizationStatusBySeniorManager(int employeeId, int attendanceId);
    //Task UpdateRegularizationStatusByHRAdmin(int employeeId, int attendanceId);

    //Task UpdateRegularizationRejectionStatusByHRAdmin(int employeeId, int attendanceId, string ReasonForRegularizeRejection);
    List<Attendance> GetCheckInTimesOfTheCurrentDate(int employeeId);
}



//using Kryptos.Hrms.API.Models;


//namespace Kryptos.Hrms.API.Provider.InterfaceAttendanceProvider;


//    public interface IAttendanceProvider
//    {
//        Attendance CheckIn(int employeeId);
//        Attendance CheckOut(int employeeId);
//        List<Attendance> GetAttendanceDetails(int employeeId);

//    }

