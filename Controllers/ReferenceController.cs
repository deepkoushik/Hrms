using Kryptos.Hrms.API.Models;
using Kryptos.Hrms.API.Providers;
using Kryptos.Hrms.API.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kryptos.Hrms.API.Controllers
{
    [Route("api/references")]
    [ApiController]
    public class ReferenceController : ControllerBase
    {
        private readonly IReferenceProvider _referenceProvider;

        public ReferenceController(IReferenceProvider referenceProvider)
        {
            _referenceProvider = referenceProvider;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Reference>>> GetAllReferences()
        {
            var references = await _referenceProvider.GetAllReferencesAsync();
            return Ok(references);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Reference>> GetReferenceById(int id)
        {
            var reference = await _referenceProvider.GetReferenceByIdAsync(id);
            if (reference == null)
            {
                return NotFound();
            }
            return Ok(reference);
        }

        [HttpPost]
        public async Task<ActionResult<Reference>> AddReference(Reference reference)
        {
            if (reference == null)
            {
                return BadRequest();
            }

            await _referenceProvider.AddReferenceAsync(reference);

            return CreatedAtAction(nameof(GetReferenceById), new { id = reference.Id }, reference);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateReference(int id, Reference reference)
        {
            if (id != reference.Id)
            {
                return BadRequest();
            }

            await _referenceProvider.UpdateReferenceAsync(reference);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReference(int id)
        {
            var existingReference = await _referenceProvider.GetReferenceByIdAsync(id);
            if (existingReference == null)
            {
                return NotFound();
            }

            await _referenceProvider.DeleteReferenceAsync(id);

            return NoContent();
        }
    }
}
