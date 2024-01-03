using System;
using System.Threading.Tasks;
using Kryptos.Hrms.API.Models;

public class AttendanceProviders : IAttendanceProvider
{
    private readonly IAttendanceRepository _attendanceRepository;

    public AttendanceProviders(IAttendanceRepository attendanceRepository)
    {
        _attendanceRepository = attendanceRepository;
    }

    public async Task<Attendance> CheckIn(int employeeId)
    {
        return await _attendanceRepository.CheckIn(employeeId);
    }

    public async Task<Attendance> CheckOut(int employeeId)
    {
        return await _attendanceRepository.CheckOut(employeeId);
    }
    public List<Attendance> GetAttendanceDetails(int employeeId)
    {
        return _attendanceRepository.GetAttendanceDetails(employeeId);
    }
    public List<Attendance> GetCheckInTimesOfTheCurrentDate(int employeeId)
    {
        return _attendanceRepository.GetCheckInTimesOfTheCurrentDate(employeeId);
    }
    public List<Attendance> GetAttendanceDetailsByEmployeeId(int employeeId)
    {
        return _attendanceRepository.GetAttendanceDetailsByEmployeeId(employeeId);
    }
    public async Task UpdateRegularizationByAttendanceId(int attendanceId, bool firstHalfAttendance, bool secondHalfAttendance, string reasonForRegularization)
    {
        await _attendanceRepository.UpdateRegularizationByAttendanceId(attendanceId,firstHalfAttendance,secondHalfAttendance, reasonForRegularization);
    }


    public async Task<List<Attendance>> GetTheAttendanceListByEmployeeId(int employeeId)
    {
        return await _attendanceRepository.GetTheAttendanceListByEmployeeId(employeeId);
    }



    public async Task UpdateRegularizationStatusBySeniorManager(int employeeId, int attendanceId)
    {
        await _attendanceRepository.UpdateRegularizationStatusBySeniorManager(employeeId, attendanceId);
    }

    public async Task UpdateRegularizationStatusAsRejectionBySeniorManager(int employeeId, int attendanceId, string lineManagerRejectionReason)
    {
        await _attendanceRepository.UpdateRegularizationStatusAsRejectionBySeniorManager(employeeId, attendanceId, lineManagerRejectionReason);
    }

    //public async Task UpdateRegularizationStatusByHRAdmin(int employeeId, int attendanceId)
    //{ 
    //    await _attendanceRepository.UpdateRegularizationStatusByHRAdmin(employeeId, attendanceId);
    //}

    //public async Task UpdateRegularizationRejectionStatusByHRAdmin(int employeeId, int attendanceId, string ReasonForRegularizeRejection)
    //{
    //    await _attendanceRepository.UpdateRegularizationRejectionStatusByHRAdmin(employeeId, attendanceId, ReasonForRegularizeRejection);
    //}

}




//using System.Collections.Generic;
//using Kryptos.Hrms.API.Models;
//using Kryptos.Hrms.API.Provider.InterfaceAttendanceProvider;
//using Kryptos.Hrms.API.Repository.InterfaceAttendanceRepository;
//using Kryptos.Hrms.API.Provider.WorkforceManagementproviderInterface;
//using Kryptos.Hrms.API.Repositories;
//namespace Kryptos.Hrms.API.Provider.AttendanceClassProvider
//{
//    public class AttendanceProvider : IAttendanceProvider
//    {
//        private readonly IAttendanceRepository _attendanceRepositorys;

//        public AttendanceProvider(IAttendanceRepository attendanceRepository)
//        {
//            _attendanceRepositorys = attendanceRepository;
//        }



//        public Attendance CheckIn(int employeeId)
//        {
//            return _attendanceRepositorys.CheckIn(employeeId);
//        }

//        public Attendance CheckOut(int employeeId)
//        {
//            return _attendanceRepositorys.CheckOut(employeeId);
//        }

//        public List<Attendance> GetAttendanceDetails(int employeeId)
//        {
//            return _attendanceRepositorys.GetAttendanceDetails(employeeId);
//        }
//    }
//}
