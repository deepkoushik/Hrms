using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Kryptos.Hrms.API.Models;
using Kryptos.Hrms.API.Provider.WorkforceManagementproviderInterface;

namespace Kryptos.Hrms.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BankInfoController : ControllerBase
    {
        private readonly IBankInfoProvider _bankInfoProvider;

        public BankInfoController(IBankInfoProvider bankInfoProvider)
        {
            _bankInfoProvider = bankInfoProvider;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBankInfo()
        {
            var bankInfos = await _bankInfoProvider.GetAllBankInfoAsync();
            return Ok(bankInfos);
        }

        [HttpPost]
        public async Task<IActionResult> AddBankInfo(BankInfo bankInfo)
        {
            try
            {
                if (bankInfo == null)
                {
                    return BadRequest("Bank information is null.");
                }

                await _bankInfoProvider.AddBankInfoAsync(bankInfo);
                return CreatedAtAction(nameof(GetAllBankInfo), new { id = bankInfo.Id }, bankInfo);
            }
            catch (Exception ex)
            {
                // Log the exception and return an error response
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
    }
}
