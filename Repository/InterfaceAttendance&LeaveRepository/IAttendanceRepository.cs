using System;
using System.Threading.Tasks;
using Kryptos.Hrms.API.Models;

public interface IAttendanceRepository
{
    Task<Attendance> CheckIn(int employeeId);
    Task<Attendance> CheckOut(int employeeId);
    List<Attendance> GetAttendanceDetails(int employeeId);
    List<Attendance> GetAttendanceDetailsByEmployeeId(int employeeId);
    Task UpdateRegularizationByAttendanceId(int attendanceId, bool firstHalfAttendance, bool secondHalfAttendance, string reasonForRegularization);
    Task<List<Attendance>> GetTheAttendanceListByEmployeeId(int employeeId);
    Task UpdateRegularizationStatusBySeniorManager(int employeeId, int attendanceId);
    Task UpdateRegularizationStatusAsRejectionBySeniorManager(int employeeId, int attendanceId, string lineManagerRejectionReason);

    //Task UpdateRegularizationRejectionStatusByHRAdmin(int employeeId, int attendanceId, string ReasonForRegularizeRejection);
    //Task UpdateRegularizationStatusByHRAdmin(int employeeId, int attendanceId);
    List<Attendance> GetCheckInTimesOfTheCurrentDate(int employeeId);

}








//using Kryptos.Hrms.API.Models;
//using System.Collections.Generic;

//namespace Kryptos.Hrms.API.Repository.InterfaceAttendanceRepository
//{
//    public interface InterfaceAttendanceRepository
//    {
//        Attendance CheckIn(int employeeId);
//        Attendance CheckOut(int employeeId);
//        List<Attendance> GetAttendanceDetails(int employeeId);

//    }
//}
