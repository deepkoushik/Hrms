using Kryptos.Hrms.API.Input_Models.CompensationAndBenefits;
using Kryptos.Hrms.API.Models;
using Kryptos.Hrms.API.Provider.CompensationAndBenefits_Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Kryptos.Hrms.API.Controller.CompensationAndBenefits
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EarningsPercentageController : ControllerBase
    {
        private readonly IEarningsPercentagePro _pro;

        public EarningsPercentageController(IEarningsPercentagePro pro)
        {
            _pro = pro;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEarningsPercentageAllocations()
        {
            try
            {
                var allocations = await _pro.GetAllEarningsPercentageAllocations();
                return Ok(allocations);
            }
            catch (Exception ex)
            {
                // Handle exceptions appropriately, e.g., log the error.
                return StatusCode(400,ex);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateEarningsPercentageAllocation(EarningsPercentageInput allocation)
        { 

            try
            {
                await _pro.UpdateEarningsPercentageAllocationAsync(allocation);
                return Ok("Successfully Updated"); 
            }
            catch (Exception ex)
            {
                // Handle exceptions appropriately, e.g., log the error.
                return BadRequest("Unable to update");
            }
        }
    }
}
