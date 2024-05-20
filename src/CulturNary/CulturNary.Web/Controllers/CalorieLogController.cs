using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CulturNary.Web.Models;

namespace CulturNary.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalorieLogController : ControllerBase
    {
        private readonly CulturNaryDbContext _context;

        public CalorieLogController(CulturNaryDbContext context)
        {
            _context = context;
        }

        // GET: api/CalorieLog
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CalorieLog>>> GetCalorieLogs()
        {
            return await _context.CalorieLogs.ToListAsync();
        }

        // GET: api/CalorieLog/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CalorieLog>> GetCalorieLog(int id)
        {
            var calorieLog = await _context.CalorieLogs.FindAsync(id);

            if (calorieLog == null)
            {
                return NotFound();
            }

            return calorieLog;
        }

        // PUT: api/CalorieLog/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCalorieLog(int id, CalorieLog calorieLog)
        {
            if (id != calorieLog.Id)
            {
                return BadRequest();
            }

            _context.Entry(calorieLog).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CalorieLogExists(id))
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

        // POST: api/CalorieLog
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CalorieLog>> PostCalorieLog(CalorieLog calorieLog)
        {
            _context.CalorieLogs.Add(calorieLog);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCalorieLog", new { id = calorieLog.Id }, calorieLog);
        }

        // DELETE: api/CalorieLog/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCalorieLog(int id)
        {
            var calorieLog = await _context.CalorieLogs.FindAsync(id);
            if (calorieLog == null)
            {
                return NotFound();
            }

            _context.CalorieLogs.Remove(calorieLog);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CalorieLogExists(int id)
        {
            return _context.CalorieLogs.Any(e => e.Id == id);
        }
    }
}
