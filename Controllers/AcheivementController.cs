using Microsoft.AspNetCore.Mvc;
using Kryptos.Hrms.API.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Kryptos.Hrms.API;

namespace Kryptos.Hrms.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AchievementController : ControllerBase
    {
        private readonly IAchievementProvider _achievementProvider;

        public AchievementController(IAchievementProvider achievementProvider)
        {
            _achievementProvider = achievementProvider;
        }

        [HttpGet]
        public async Task<ActionResult<List<Achievement>>> GetAllAchievementsAsync()
        {
            try
            {
                var achievements = await _achievementProvider.GetAllAchievementsAsync();
                return Ok(achievements);
            }
            catch (Exception ex)
            {
                // Handle error and return appropriate response
                return StatusCode(500, "An error occurred while fetching achievements.");
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddAchievementAsync([FromBody] Achievement achievement)
        {
            try
            {
                await _achievementProvider.AddAchievementAsync(achievement);
                return Ok("Achievement added successfully.");
            }
            catch (Exception ex)
            {
                // Handle error and return appropriate response
                return StatusCode(500, "An error occurred while adding an achievement.");
            }
        }
    }
}
