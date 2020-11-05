using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AnalysisWebApp.AnalysisDb;
using AnalysisWebApp.Models;

namespace AnalysisWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrgansController : ControllerBase
    {
        private readonly AnalysisDbContext _context;

        public OrgansController(AnalysisDbContext context)
        {
            _context = context;
        }

        // GET: api/Organs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Organ>>> GetOrgans()
        {
            return await _context.Organs.ToListAsync();
        }

        // GET: api/Organs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Organ>> GetOrgan(int id)
        {
            var organ = await _context.Organs.FindAsync(id);

            if (organ == null)
            {
                return NotFound();
            }

            return organ;
        }

        // PUT: api/Organs/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrgan(int id, Organ organ)
        {
            if (id != organ.ID)
            {
                return BadRequest();
            }

            _context.Entry(organ).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrganExists(id))
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

        // POST: api/Organs
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Organ>> PostOrgan(Organ organ)
        {
            organ.OrganDetails = null;
            
            _context.Organs.Add(organ);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrgan", new { id = organ.ID }, organ);
        }

        // DELETE: api/Organs/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Organ>> DeleteOrgan(int id)
        {
            var organ = await _context.Organs.FindAsync(id);
            if (organ == null)
            {
                return NotFound();
            }

            _context.Organs.Remove(organ);
            await _context.SaveChangesAsync();

            return organ;
        }

        private bool OrganExists(int id)
        {
            return _context.Organs.Any(e => e.ID == id);
        }
    }
}
