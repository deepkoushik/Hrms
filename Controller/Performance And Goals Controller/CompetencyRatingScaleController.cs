using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Kryptos.Hrms.API.Models;
using Kryptos.Hrms.API.Provider;

namespace Kryptos.Hrms.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompetencyRatingScaleController : ControllerBase
    {
        private readonly ICompentencyRatingScaleProvider _provider;

        public CompetencyRatingScaleController(ICompentencyRatingScaleProvider provider)
        {
            _provider = provider;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CompetencyRatingScale>>> Get()
        {
            var competencyRatingScales = await _provider.GetAllCompetencyRatingScalesAsync();
            return Ok(competencyRatingScales);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CompetencyRatingScale>> Get(int id)
        {
            var competencyRatingScale = await _provider.GetCompetencyRatingScaleByIdAsync(id);
            if (competencyRatingScale == null)
            {
                return NotFound();
            }
            return Ok(competencyRatingScale);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CompetencyRatingScale competencyRatingScale)
        {
            await _provider.AddCompetencyRatingScaleAsync(competencyRatingScale);
            return CreatedAtAction(nameof(Get), new { id = competencyRatingScale.Id }, competencyRatingScale);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] CompetencyRatingScale competencyRatingScale)
        {
            if (id != competencyRatingScale.Id)
            {
                return BadRequest();
            }

            await _provider.UpdateCompetencyRatingScaleAsync(competencyRatingScale);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _provider.DeleteCompetencyRatingScaleAsync(id);
            return NoContent();
        }
    }
}
