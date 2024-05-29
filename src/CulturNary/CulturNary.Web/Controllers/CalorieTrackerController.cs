using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CulturNary.Web.Models;
using CulturNary.Web.Services;
using Microsoft.AspNetCore.Identity;
using CulturNary.Web.Areas.Identity.Data;
using Microsoft.AspNetCore.Authorization;
using CulturNary.Web.Models.DTO;
using Newtonsoft.Json;
using System.Web;

namespace CulturNary.Web.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = "Signed,Admin")]
    [ApiController]
    public class CalorieTrackerController : ControllerBase
    {
        private readonly CulturNaryDbContext _context;

        public CalorieTrackerController(CulturNaryDbContext context)
        {
            _context = context;
        }

        // GET: api/CalorieTracker
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CalorieTracker>>> GetCalorieTrackers()
        {
            return await _context.CalorieTrackers.ToListAsync();
        }

        // GET: api/CalorieTracker/5
        [HttpGet("{id}")]
        public async Task<ActionResult<List<CalorieTrackerDto>>> GetCalorieTracker(int id)
        {
            var calorieTracker = await _context.CalorieTrackers
                                .Where(x => x.PersonId == id)
                                .Select(c => new CalorieTrackerDto
                                {
                                    Id = c.Id,
                                    PersonId = c.PersonId,
                                    PersonCalories = c.PersonCalories
                                })
                                .ToListAsync();

            return Ok(calorieTracker);
        }

        // PUT: api/CalorieTracker/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCalorieTracker(int id, CalorieTracker calorieTracker)
        {
            if (id != calorieTracker.Id)
            {
                return BadRequest();
            }

            _context.Entry(calorieTracker).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CalorieTrackerExists(id))
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

        // POST: api/CalorieTracker
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CalorieTrackerDto>> PostCalorieTracker(CalorieTrackerDto calorieTrackerDto)
        {
            CalorieTracker calorieTracker = new CalorieTracker()
            {
                Id = calorieTrackerDto.Id,
                PersonId = calorieTrackerDto.PersonId,
                PersonCalories = calorieTrackerDto.PersonCalories
            };

            _context.CalorieTrackers.Add(calorieTracker);
            await _context.SaveChangesAsync();

            CalorieTrackerDto createdCalorieTrackerDto = new CalorieTrackerDto()
            {
                Id = calorieTracker.Id,
                PersonId = calorieTracker.PersonId,
                PersonCalories = calorieTracker.PersonCalories
            };
            return CreatedAtAction("GetCalorieTracker", new { id = calorieTracker.Id }, createdCalorieTrackerDto);
        }

        // DELETE: api/CalorieTracker/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCalorieTracker(int id)
        {
            var calorieTracker = await _context.CalorieTrackers.FindAsync(id);
            if (calorieTracker == null)
            {
                return NotFound();
            }

            _context.CalorieTrackers.Remove(calorieTracker);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CalorieTrackerExists(int id)
        {
            return _context.CalorieTrackers.Any(e => e.Id == id);
        }
    }
}
