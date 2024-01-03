using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Kryptos.Hrms.API.Models;
using Kryptos.Hrms.API.Provider.WorkforceManagementproviderInterface;

namespace Kryptos.Hrms.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CoreSkillController : ControllerBase
    {
        private readonly ICoreSkillProvider _coreSkillProvider;

        public CoreSkillController(ICoreSkillProvider coreSkillProvider)
        {
            _coreSkillProvider = coreSkillProvider;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CoreSkill>>> GetAllCoreSkills()
        {
            try
            {
                var coreSkills = await _coreSkillProvider.GetAllCoreSkillsAsync();
                return Ok(coreSkills);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult> AddCoreSkill([FromBody] CoreSkill coreSkill)
        {
            try
            {
                await _coreSkillProvider.AddCoreSkillAsync(coreSkill);
                return CreatedAtAction(nameof(GetAllCoreSkills), new { }, coreSkill);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
    }
}
