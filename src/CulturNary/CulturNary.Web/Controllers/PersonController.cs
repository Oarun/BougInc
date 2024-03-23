using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CulturNary.Web.Models;
using Microsoft.AspNetCore.Identity;
using CulturNary.Web.Models.DTO;
using CulturNary.Web.Areas.Identity.Data;
using Microsoft.AspNetCore.Authorization;

namespace CulturNary.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Signed,Admin")]
    public class PersonController : Controller
    {
        private readonly CulturNaryDbContext _context;
        private readonly UserManager<SiteUser> _userManager;

        public PersonController(CulturNaryDbContext context,  UserManager<SiteUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: api/Person/GetCurrentPerson
        [HttpGet("GetCurrentPerson")]
        public async Task<ActionResult<Person>> GetCurrentPerson()
        {
            try
            {
                var userId = _userManager.GetUserId(User);

                var person = await _context.People
                                    .Include(p => p.Collections)
                                    .Include(p => p.Recipes)
                                    .FirstOrDefaultAsync(p => p.IdentityId == userId);

                if (person == null)
                {
                    return NotFound();
                }

                return Ok(person);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving current person: {ex.Message}");
                return StatusCode(500);
            }
        }

        // GET: api/Person/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Person>> GetPerson(int id)
        {
            var person = await _context.People.FindAsync(id);

            if (person == null)
            {
                return NotFound();
            }

            return person;
        }

        // PUT: api/Person/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPerson(int id, Person person)
        {
            if (id != person.Id)
            {
                return BadRequest();
            }

            _context.Entry(person).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonExists(id))
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

        // POST: api/Person
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Person>> PostPerson(Person person)
        {
            _context.People.Add(person);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPerson", new { id = person.Id }, person);
        }

        // DELETE: api/Person/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePerson(int id)
        {
            var person = await _context.People.FindAsync(id);
            if (person == null)
            {
                return NotFound();
            }

            _context.People.Remove(person);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        [HttpGet("Identity/{identityId}")]
        public async Task<ActionResult<Person>> GetPersonByIdentityId(Guid identityId)
        {
            var person = await _context.People.FirstOrDefaultAsync(p => p.IdentityId == identityId.ToString());

            if (person == null)
            {
                return NotFound();
            }

            return person;
        }
        private bool PersonExists(int id)
        {
            return _context.People.Any(e => e.Id == id);
        }
    }
}
