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
    public class MessagesController : ControllerBase
    {
        private readonly AnalysisDbContext _context;

        public MessagesController(AnalysisDbContext context)
        {
            _context = context;
        }

        // GET: api/Messages
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Message>>> GetMessages()
        {
            return await _context.Messages.ToListAsync();
        }

        // GET: api/Messages/5
        [HttpGet("{id}")]
        public  Task<List<Message>> GetMessage(int id)
        {
          
            var messages = (from a in (from x in _context.UserChoices.Where(c => c.UserID == id)
                                       join v in _context.OrganDetails
                                       on x.OrganDetailID equals v.ID
                                       select v)
                            join r in _context.Messages
                            on a.MessageID equals r.ID
                            select r).ToListAsync(); 
                           

            

            if (messages == null)
            {
              
            }

            return messages;
        }








        [HttpGet]
        [Route("GetFavMessage")]
        public Task<List<Message>> GetFavMessage(int id)
        {
            
            var messages = (from a in (from x in _context.UserChoices.Where(c => c.UserID == id && c.IsFav==true)
                                       join v in _context.OrganDetails
                                       on x.OrganDetailID equals v.ID
                                       select v)
                            join r in _context.Messages
                            on a.MessageID equals r.ID
                            select r).ToListAsync();




            if (messages == null)
            {

            }

            return messages;
        }







        // PUT: api/Messages/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMessage(int id, Message message)
        {
            if (id != message.ID)
            {
                return BadRequest();
            }

            _context.Entry(message).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MessageExists(id))
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

        // POST: api/Messages
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Message>> PostMessage(Message message)
        {
            _context.Messages.Add(message);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMessage", new { id = message.ID }, message);
        }

        // DELETE: api/Messages/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Message>> DeleteMessage(int id)
        {
            var message = await _context.Messages.FindAsync(id);
            if (message == null)
            {
                return NotFound();
            }

            _context.Messages.Remove(message);
            await _context.SaveChangesAsync();

            return message;
        }

        private bool MessageExists(int id)
        {
            return _context.Messages.Any(e => e.ID == id);
        }
    }
}
