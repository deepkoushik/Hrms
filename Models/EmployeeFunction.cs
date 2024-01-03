using Kryptos.Hrms.API.Models;
using System;

namespace Kryptos.Hrms.API.Functions
{
    public static class EmployeeFunction
    {
        public static void SetLeaveAvailabilities(Employee employee, int sickLeaveBalance, int casualLeaveBalance)
        {
            if (employee.Role == "Software Engineer Trainee")
            {
                employee.LeaveAvailabilities.Add(new LeaveAvailability
                {
                    SickLeaveBalance = sickLeaveBalance,
                    CasualLeaveBalance = 0, // Set CasualLeaveBalance to 0 for Software Engineer Trainee
                    TotalLeaveBalance = sickLeaveBalance,
                    LastUpdated = DateTime.Now
                });
            }
            else
            {
                employee.LeaveAvailabilities.Add(new LeaveAvailability
                {
                    SickLeaveBalance = sickLeaveBalance,
                    CasualLeaveBalance = 1, // Set CasualLeaveBalance to 1 for other roles
                    TotalLeaveBalance = sickLeaveBalance + 1,
                    LastUpdated = DateTime.Now
                });
            }
        }
    }
}
