﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Kryptos.Hrms.API.Models;


namespace Kryptos.Hrms.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QualificationsController : ControllerBase
    {
        private readonly KryptosHrmsDbContext _context;

        public QualificationsController(KryptosHrmsDbContext context)
        {
            _context = context;
        }

        // GET: api/Qualifications
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Qualification>>> GetQualifications()
        {
            if (_context.Qualifications == null)
            {
                return NotFound();
            }
            return await _context.Qualifications.ToListAsync();
        }

        // GET: api/Qualifications/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Qualification>> GetQualification(int id)
        {
            if (_context.Qualifications == null)
            {
                return NotFound();
            }
            var qualification = await _context.Qualifications.FindAsync(id);

            if (qualification == null)
            {
                return NotFound();
            }

            return qualification;
        }

        // PUT: api/Qualifications/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutQualification(int id, Qualification qualification)
        {
            if (id != qualification.Id)
            {
                return BadRequest();
            }

            _context.Entry(qualification).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!QualificationExists(id))
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

        // POST: api/Qualifications
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Qualification>> PostQualification(Qualification qualification)
        {
            if (_context.Qualifications == null)
            {
                return Problem("Entity set 'HrmsDbContext.Qualifications'  is null.");
            }
            _context.Qualifications.Add(qualification);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetQualification", new { id = qualification.Id }, qualification);
        }

        // DELETE: api/Qualifications/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQualification(int id)
        {
            if (_context.Qualifications == null)
            {
                return NotFound();
            }
            var qualification = await _context.Qualifications.FindAsync(id);
            if (qualification == null)
            {
                return NotFound();
            }

            _context.Qualifications.Remove(qualification);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool QualificationExists(int id)
        {
            return (_context.Qualifications?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
