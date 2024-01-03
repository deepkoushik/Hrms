using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Kryptos.Hrms.API.Provider;
using Kryptos.Hrms.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace HRMSDB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResignationController : ControllerBase
    {
        private readonly IResignationProvider _resignationProvider;

        public ResignationController(IResignationProvider resignationProvider)
        {
            _resignationProvider = resignationProvider;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Resignation>>> GetAllResignationsAsync()
        {
            var resignations = await _resignationProvider.GetAllResignationsAsync();
            return Ok(resignations);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Resignation>> GetResignationByIdAsync(int id)
        {
            var resignation = await _resignationProvider.GetResignationByIdAsync(id);
            if (resignation == null)
            {
                return NotFound();
            }
            return Ok(resignation);
        }

        [HttpPost]
        public async Task<ActionResult<Resignation>> CreateResignationAsync([FromBody] Resignation resignation)
        {
            await _resignationProvider.CreateResignationAsync(resignation);
            return CreatedAtAction(nameof(GetResignationByIdAsync), new { id = resignation.Id }, resignation);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateResignationAsync(int id, [FromBody] Resignation resignation)
        {
            if (id != resignation.Id)
            {
                return BadRequest();
            }

            var updatedResignation = await _resignationProvider.GetResignationByIdAsync(id);
            if (updatedResignation == null)
            {
                return NotFound();
            }

            await _resignationProvider.UpdateResignationAsync(resignation);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteResignationAsync(int id)
        {
            var resignationToDelete = await _resignationProvider.GetResignationByIdAsync(id);
            if (resignationToDelete == null)
            {
                return NotFound();
            }

            await _resignationProvider.DeleteResignationAsync(id);

            return NoContent();
        }
    }
}
