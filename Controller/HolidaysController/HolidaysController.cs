using System;
using System.Threading.Tasks;
using Kryptos.Hrms.API.Providers;
using Microsoft.AspNetCore.Mvc;

namespace Kryptos.Hrms.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HolidaysController : ControllerBase
    {
        private readonly IHolidaysProvider _holidaysProvider;

        public HolidaysController(IHolidaysProvider holidaysProvider)
        {
            _holidaysProvider = holidaysProvider;
        }

        [HttpPost]
        public async Task<IActionResult> PostHoliday(int employeeId, string name, DateTime date)
        {
            try
            {
                var holiday = await _holidaysProvider.PostTheHoldaysByTheHRAdmin(employeeId, name, date);
                return Ok(holiday);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetHolidaysByYear(int year)
        {
            try
            {
                var holidays = await _holidaysProvider.GetHolidaysByYear(year);
                return Ok(holidays);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
