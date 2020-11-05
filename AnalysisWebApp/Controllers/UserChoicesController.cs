using AnalysisWebApp.AnalysisDb;
using AnalysisWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnalysisWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserChoicesController : ControllerBase
    {
        private readonly AnalysisDbContext _context;

        public UserChoicesController(AnalysisDbContext context)
        {
            _context = context;
        }

        // GET: api/UserChoices
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserChoice>>> GetUserChoices()
        {
            return await _context.UserChoices.ToListAsync();
        }



        // PUT: api/UserChoices/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
     /*   [HttpPut("{id}")]
        public async Task<IActionResult> PutUserChoice(int id, UserChoice userChoice)
        {
            if (id != userChoice.ID)
            {
                return BadRequest();
            }

            _context.Entry(userChoice).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserChoiceExists(id))
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
        */

        // DELETE: api/UserChoices/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<UserChoice>> DeleteUserChoice(int id)
        {
            var userChoice = await _context.UserChoices.FindAsync(id);
            if (userChoice == null)
            {
                return NotFound();
            }

            _context.UserChoices.Remove(userChoice);
            await _context.SaveChangesAsync();

            return userChoice;
        }

        private bool UserChoiceExists(int id, int userid)
        {
       
     
                   return _context.UserChoices.Any(e => e.BasicOrganID== id && e.UserID==userid) ;
        }
        [HttpPut]
        [Route("PutUserFavourite")]
        public async Task<UserChoice> PutUserChoice(UserFavMsgModel userFavMsgModel)

        {
            var userCh = _context.UserChoices.FirstOrDefault(obj => obj.organDetail.MessageID == userFavMsgModel.MessageId);
            if (userCh.IsFav == true)
            {
                userCh.IsFav = false;
            }
            else userCh.IsFav = true;


            _context.Entry(userCh).State = EntityState.Modified;
            await _context.SaveChangesAsync();


            // var s = _context.UserChoices.Where(p => p.UserID == id).FirstOrDefault();

            /* var userchoiceid = (from b in _context.UserChoices.Where(p => p.UserID == id)
                                 join d in (from x in _context.OrganDetails
                                            join c in _context.Messages.Where(msg => msg.ID == MsgID)
                                            on x.MessageID equals c.ID
                                            select x)
                                 on b.OrganDetailID equals d.ID
                                 select b).ToList();


             // userChoice.IsFav = true;


             UserChoice userChoice = new UserChoice();
             foreach (var item in userchoiceid)
             {
                 item.IsFav = true;
                 _context.Entry(item).State = EntityState.Modified;
                 await _context.SaveChangesAsync();
                 userChoice = item;
             }*/



            return userCh;




        }


        /// <param name="userChoice"></param>
        /// <returns></returns>
        // POST: api/UserChoices
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<UserChoice>> PostUserChoice(UserChoice userChoice)
        {
            if (!UserChoiceExists(userChoice.BasicOrganID ,userChoice.UserID))

            {
              
                userChoice.IsFav = false;

                userChoice.organDetail = null;
              
                _context.UserChoices.Add(userChoice);
                await _context.SaveChangesAsync();
             return  CreatedAtAction("GetUserChoice", new { id = userChoice.ID }, userChoice);
            }
             var sd = await _context.UserChoices.Where(c => c.UserID == userChoice.UserID).FirstOrDefaultAsync();
            sd.OrganDetailID = userChoice.OrganDetailID;
            sd.organDetail = null;
            sd.BasicOrganID = userChoice.BasicOrganID;
            
            
            _context.Entry(sd).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return sd;
           
        }
        // GET: api/UserChoices/5
        [HttpGet("{id}")]
        public Task<List<OrganDetail>> GetUserChoice(int id)
        {
            //  var userChoice = await _context.UserChoices.Where(c=>c.UserID==id).FirstOrDefaultAsync();
            var OrganResult = (from x in _context.UserChoices.Where(c => c.UserID == id)
                               join v in _context.OrganDetails
                               on x.OrganDetailID equals v.ID
                               select v).ToListAsync();

            if (OrganResult == null)
            {

            }

            return OrganResult;
        }
        [HttpPut]
        [Route("EditUserChoice")]
        public async Task<ActionResult<UserChoice>> EditUserChoice(EditQuiz editQuiz)
        {
            var sd = await _context.UserChoices.Where(c => c.UserID == editQuiz.userId).FirstOrDefaultAsync();
            sd.OrganDetailID = editQuiz.organDetailId;
            _context.Entry(sd).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return sd;
        }
    }
}
