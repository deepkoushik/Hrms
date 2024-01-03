using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Kryptos.Hrms.API.Models;


namespace SampleProject1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RelationNamesController : ControllerBase
    {
        private readonly KryptosHrmsDbContext _context;

        public RelationNamesController(KryptosHrmsDbContext context)
        {
            _context = context;
        }

        // GET: api/RelationNames
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RelationName>>> GetRelationNames()
        {
            if (_context.RelationNames == null)
            {
                return NotFound();
            }
            return await _context.RelationNames.ToListAsync();
        }                                                        

        // GET: api/RelationNames/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RelationName>> GetRelationName(int id)
        {
            if (_context.RelationNames == null)
            {
                return NotFound();
            }
            var relationName = await _context.RelationNames.FindAsync(id);

            if (relationName == null)
            {
                return NotFound();
            }

            return relationName;
        }

        // PUT: api/RelationNames/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRelationName(int id, RelationName relationName)
        {
            if (id != relationName.Id)
            {
                return BadRequest();
            }

            _context.Entry(relationName).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RelationNameExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/RelationNames
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<RelationName>> PostRelationName(RelationName relationName)
        {
            if (_context.RelationNames == null)
            {
                return Problem("Entity set 'HrmsDbContext.RelationNames'  is null.");
            }
            _context.RelationNames.Add(relationName);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRelationName", new { id = relationName.Id }, relationName);
        }

        // DELETE: api/RelationNames/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRelationName(int id)
        {
            if (_context.RelationNames == null)
            {
                return NotFound();
            }
            var relationName = await _context.RelationNames.FindAsync(id);
            if (relationName == null)
            {
                return NotFound();
            }

            _context.RelationNames.Remove(relationName);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RelationNameExists(int id)
        {
            return (_context.RelationNames?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
