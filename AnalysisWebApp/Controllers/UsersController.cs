using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AnalysisWebApp.AnalysisDb;
using AnalysisWebApp.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace AnalysisWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly AnalysisDbContext _context;
        private readonly IHostingEnvironment hostingEnvironment;

        public UsersController(AnalysisDbContext context, IHostingEnvironment env)
        {
            _context = context;
            hostingEnvironment = env;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        [HttpGet]
        [Route("GetUserRelatives")]
        public async Task<List<User>> GetUserRelatives(int UserID)
        {
            var AllRelatives =  _context.Users.Where(x => x.AddedByUser.ID == UserID).ToList();
            return AllRelatives;
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<ActionResult<User>> PutUser(int id, User user)
        {
            if (id != user.ID)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return user;
        }


        // POST: api/Users
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        [Route("Register")]
        public async Task<ActionResult<User>> Register(User user)
        {
            user.Email = user.Email.ToLower();
            user.UserChoices = null;
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetUser", new { id = user.ID }, user);
        }

        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult<User>> Login(User user)
        {
            user.Email = user.Email.ToLower();
            var LoggedUser = _context.Users.Where(x => x.Email == user.Email && x.Password == user.Password).FirstOrDefault();

            if (user == null)
            {
                return NotFound();
            }

            return LoggedUser;
        }

        [HttpPost]
        [Route("CreateRelative")]
        public async Task<ActionResult<User>> CreateRelative(User Relative , int BasicUserID)
        {
            var BasicUser = _context.Users.Where(x => x.ID == BasicUserID).FirstOrDefault();
            Relative.Email = null;
            Relative.Password = null;
            Relative.UserChoices = null;
            Relative.PhoneNumber = null;
            Relative.AddedByUser = BasicUser ;
            _context.Users.Add(Relative);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetUser", new { id = Relative.ID }, Relative); ;
        }

       /* [HttpPost]
        [Route("AddRelative")]
        public async Task<ActionResult<User>> AddRelative(int RelativeID, int BasicUserID)
        {
            var BasicUser = _context.Users.Where(x => x.ID == BasicUserID).FirstOrDefault();
            var Relative = _context.Users.Where(x => x.ID == RelativeID).FirstOrDefault();

            if (BasicUser == null)
            {
                return NotFound();
            }

            BasicUser.AddedByUser = Relative;
            _context.Users.Add(BasicUser);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetUser", new { id = BasicUser.ID }, BasicUser);
                    
        }*/


        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<User>> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return user;
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.ID == id);
        }
        [HttpPost("UploadImage")]
        public async Task<string> UploadImage(IFormFile file)
        {
            string output = "";
            try
            {
                List<string> types = new List<string>();
                if (ModelState.IsValid)
                {
                    // Code to upload image if not null
                    if (file != null && file.Length != 0)
                    {
                        // Create a File Info
                        FileInfo fi = new FileInfo(file.FileName);

                        // This code creates a unique file name to prevent duplications
                        // stored at the file location
                        var newFilename = String.Format("{0:d}",
                                              (DateTime.Now.Ticks / 10) % 100000000) + fi.Extension;
                        var webPath = hostingEnvironment.WebRootPath;
                        var path = Path.Combine("", webPath + @"\uploads\" + newFilename);

                        // IMPORTANT: The pathToSave variable will be save on the column in the database
                        var pathToSave = @"/uploads/" + newFilename;

                        // This stream the physical file to the allocate wwwroot/ImageFiles folder
                        using (var stream = new FileStream(path, FileMode.CreateNew))
                        {
                            await file.CopyToAsync(stream);

                        }

                        // This save the path to the record
                        output = pathToSave;

                    }
                }

            }
            catch (Exception ex)
            {
                output = ex.Message;
            }
            return output;
        }

    }
}
