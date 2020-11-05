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
    public class OrganDetailsController : ControllerBase
    {
        private readonly AnalysisDbContext _context;

        public OrganDetailsController(AnalysisDbContext context)
        {
            _context = context;
        }

        // GET: api/OrganDetails
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrganDetail>>> GetOrganDetails()
        {
            return await _context.OrganDetails.ToListAsync();
        }

        // GET: api/OrganDetails/5
        [HttpGet("{id}")]
        public async Task<List<OrganDetail>> GetOrganDetail(int id)
        {
            var organDetail = await _context.OrganDetails.Where(c=>c.OrganID==id).ToListAsync();

            if (organDetail == null)
            {
              
            }

            return organDetail;
        }

        // PUT: api/OrganDetails/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrganDetail(int id, OrganDetail organDetail)
        {
            if (id != organDetail.ID)
            {
                return BadRequest();
            }

            _context.Entry(organDetail).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrganDetailExists(id))
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

        // POST: api/OrganDetails
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<OrganDetail>> PostOrganDetail(OrganDetail organDetail)
        {
            organDetail.UserChoices = null;
            _context.OrganDetails.Add(organDetail);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrganDetail", new { id = organDetail.ID }, organDetail);
        }

        // DELETE: api/OrganDetails/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<OrganDetail>> DeleteOrganDetail(int id)
        {
            var organDetail = await _context.OrganDetails.FindAsync(id);
            if (organDetail == null)
            {
                return NotFound();
            }

            _context.OrganDetails.Remove(organDetail);
            await _context.SaveChangesAsync();

            return organDetail;
        }

        private bool OrganDetailExists(int id)
        {
            return _context.OrganDetails.Any(e => e.ID == id);
        }
    }
}
