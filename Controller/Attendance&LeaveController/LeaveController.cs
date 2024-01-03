using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Kryptos.Hrms.API.Providers;
using Kryptos.Hrms.API.Provider.InterfaceAttendance_LeaveProvider;

namespace Kryptos.Hrms.API.Controller.Attendance_LeaveController
{
        [Route("api/[controller]")]
        [ApiController]
        public class LeaveController : ControllerBase
        {
            private readonly ILeaveProvider _leaveProvider;

            public LeaveController(ILeaveProvider leaveProvider)
            {
                _leaveProvider = leaveProvider;
            }

        [HttpPost("CreateALeaveTypeByHRAdmin")]
        public async Task<IActionResult> CreateALeaveTypeByHRAdmin(int employeeId, string leaveTypeName)
        {
            var createdLeaveType = await _leaveProvider.CreateALeaveTypeByHRAdminAsync(employeeId, leaveTypeName);

            if (createdLeaveType == null)
            {
                return BadRequest("Employee is not an HR Admin or does not exist.");
            }

            return Ok(createdLeaveType);
        }
    }
    





}
