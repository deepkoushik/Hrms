using Microsoft.AspNetCore.Mvc;
using Kryptos.Hrms.API.Models;
using Kryptos.Hrms.API.Repositories;
using System;
using System.Collections.Generic;

namespace Kryptos.Hrms.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompetencyRatingScaleController : ControllerBase
    {
        private readonly ICompetencyRatingScaleProvider _provider;

        public CompetencyRatingScaleController(ICompetencyRatingScaleProvider provider)
        {
            _provider = provider ?? throw new ArgumentNullException(nameof(provider));
        }

        // GET api/competencyratingscale
        [HttpGet]
        public IActionResult Get()
        {
            IEnumerable<CompetencyRatingScale> competencyRatingScales = _provider.GetAllCompetencyRatingScales();
            return Ok(competencyRatingScales);
        }

        // GET api/competencyratingscale/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            CompetencyRatingScale competencyRatingScale = _provider.GetCompetencyRatingScaleById(id);
            if (competencyRatingScale == null)
            {
                return NotFound();
            }
            return Ok(competencyRatingScale);
        }

        // POST api/competencyratingscale
        [HttpPost]
        public IActionResult Post([FromBody] CompetencyRatingScale competencyRatingScale)
        {
            if (competencyRatingScale == null)
            {
                return BadRequest();
            }

            _provider.AddCompetencyRatingScale(competencyRatingScale);
            _provider.SaveCompetencyRatingScale();

            return CreatedAtAction(nameof(Get), new { id = competencyRatingScale.Id }, competencyRatingScale);
        }

        // PUT api/competencyratingscale/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] CompetencyRatingScale competencyRatingScale)
        {
            if (competencyRatingScale == null || id != competencyRatingScale.Id)
            {
                return BadRequest();
            }

            CompetencyRatingScale existingCompetencyRatingScale = _provider.GetCompetencyRatingScaleById(id);
            if (existingCompetencyRatingScale == null)
            {
                return NotFound();
            }

            _provider.UpdateCompetencyRatingScale(competencyRatingScale);
            _provider.SaveCompetencyRatingScale();

            return NoContent();
        }

        // DELETE api/competencyratingscale/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            CompetencyRatingScale competencyRatingScale = _provider.GetCompetencyRatingScaleById(id);
            if (competencyRatingScale == null)
            {
                return NotFound();
            }

            _provider.DeleteCompetencyRatingScale(id);
            _provider.SaveCompetencyRatingScale();

            return NoContent();
        }
    }
}
