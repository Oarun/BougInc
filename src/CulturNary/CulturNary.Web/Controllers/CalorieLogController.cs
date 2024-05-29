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
    public class CalorieLogController : ControllerBase
    {
        private readonly CulturNaryDbContext _context;

        public CalorieLogController(CulturNaryDbContext context)
        {
            _context = context;
        }

        // // GET: api/CalorieLog
        // [HttpGet]
        // public async Task<ActionResult<IEnumerable<CalorieLog>>> GetCalorieLogs()
        // {
        //     return await _context.CalorieLogs.ToListAsync();
        // }

        // GET: api/Restaurant/5 Uses Person Id
        [HttpGet("{id}")]
        public async Task<ActionResult<List<CalorieLogDto>>> GetCalorieLogs(int id)
        {
            var CalorieLogs = await _context.CalorieLogs
                            .Where(x => x.PersonId == id)
                            .Select (c => new CalorieLogDto
                            {
                                Id = c.Id,
                                PersonId = c.PersonId,
                                CaloriesLogged = c.CaloriesLogged,
                                LogDate = c.LogDate
                            })
                            .OrderBy(x => x.LogDate)
                            .ToListAsync();
            return Ok(CalorieLogs);
        }

        // // PUT: api/CalorieLog/5
        // // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        // [HttpPut("{id}")]
        // public async Task<IActionResult> PutCalorieLog(CalorieLogDto calorieLogDto)
        // {
        //     if (id != calorieLogDto.Id)
        //     {
        //         return BadRequest();
        //     }

        //     _context.Entry(calorieLog).State = EntityState.Modified;

        //     try
        //     {
        //         await _context.SaveChangesAsync();
        //     }
        //     catch (DbUpdateConcurrencyException)
        //     {
        //         if (!CalorieLogExists(id))
        //         {
        //             return NotFound();
        //         }
        //         else
        //         {
        //             throw;
        //         }
        //     }

        //     return NoContent();
        // }

        // POST: api/CalorieLog
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CalorieLogDto>> PostCalorieLog(CalorieLogDto calorieLogDto)
        {
            CalorieLog calorieLog = new CalorieLog()
            {
                Id = calorieLogDto.Id,
                PersonId = calorieLogDto.PersonId,
                CaloriesLogged = calorieLogDto.CaloriesLogged,
                LogDate = calorieLogDto.LogDate
            };

            _context.CalorieLogs.Add(calorieLog);
            await _context.SaveChangesAsync();

            CalorieLogDto createdCalorieLogDto = new CalorieLogDto()
            {
                Id = calorieLog.Id,
                PersonId = calorieLog.PersonId,
                CaloriesLogged = calorieLog.CaloriesLogged,
                LogDate = calorieLog.LogDate
            };
            return CreatedAtAction("GetCalorieLog", new { id = calorieLog.Id }, createdCalorieLogDto);
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
