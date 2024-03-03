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

namespace CulturNary.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
        public async Task<ActionResult<PersonDto>> GetCurrentPerson()
        {
            try
            {
                // Get the user ID of the currently authenticated user
                var userId = _userManager.GetUserId(User);

               var person = await _context.People
                                    .Where(x => x.IdentityId == userId)
                                    .Select(x => new PersonDto
                                    {
                                        Id = x.Id,
                                        IdentityId = x.IdentityId
                                        // Map other properties as needed
                                    })
                                    .FirstOrDefaultAsync();

                if (person == null)
                {
                    // If the person is not found, return a 404 Not Found response
                    return NotFound();
                }

                // Return the person DTO
                return Ok(person); // Return a 200 OK response with the person DTO
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Error retrieving current person: {ex.Message}");

                // Return a 500 Internal Server Error response
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

        private bool PersonExists(int id)
        {
            return _context.People.Any(e => e.Id == id);
        }
    }
}
