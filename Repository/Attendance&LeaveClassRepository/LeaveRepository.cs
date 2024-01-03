using System;
using System.Threading.Tasks;
using global::Kryptos.Hrms.API.Repository.InterfaceAttendance_LeaveRepository;
using Kryptos.Hrms.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Kryptos.Hrms.API.Repository.Attendance_LeaveClassRepository
{


    public class LeaveRepository : ILeaveRepository
    {
        private readonly KryptosHrmsDbContext _dbContext;

        public LeaveRepository(KryptosHrmsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<LeaveType> CreateLeaveTypeByHRAdminAsync(int employeeId, string leaveTypeName)
        {
            // Check if the employee with the given ID has the "HR Admin" role
            var hrAdmin = await _dbContext.Employees.FirstOrDefaultAsync(e => e.Id == employeeId && e.Role == "HR Admin");

            if (hrAdmin == null)
            {
                // Employee is not an HR Admin
                return null;
            }

            // Create a new LeaveType instance
            var leaveType = new LeaveType
            {
                LeaveTypeName = leaveTypeName,
                IsActive = "Active",
                CreatedBy = employeeId,
                CreatedTime = DateTime.Now
            };

            // Add the new LeaveType to the context and save changes
            _dbContext.LeaveTypes.Add(leaveType);
            await _dbContext.SaveChangesAsync();

            return leaveType;
        }



    }
    }
