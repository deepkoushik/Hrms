using Kryptos.Hrms.API.Providers;
using Microsoft.AspNetCore.Mvc;

namespace Kryptos.Hrms.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LeaveAvailabilityController : ControllerBase
    {
        private readonly ILeaveAvailabilityProvider _leaveAvailabilityProvider;

        public LeaveAvailabilityController(ILeaveAvailabilityProvider leaveAvailabilityProvider)
        {
            _leaveAvailabilityProvider = leaveAvailabilityProvider;
        }

        [HttpGet("{employeeId}")]
        public async Task<IActionResult> GetLeaveAvailability(int employeeId)
        {
            var leaveAvailability = await _leaveAvailabilityProvider.GetLeaveAvailabilityByEmployeeIdAsync(employeeId);

            if (leaveAvailability == null)
            {
                return NotFound();
            }

            return Ok(leaveAvailability);
        }
    }
}
