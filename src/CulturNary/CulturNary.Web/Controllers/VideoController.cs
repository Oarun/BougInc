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
using System.Runtime.CompilerServices;
using CulturNary.Web.Areas.Identity.Data;
using Microsoft.AspNetCore.Authorization;

namespace CulturNary.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Signed,Admin")]

    public class VideoController : ControllerBase
    {
        private readonly CulturNaryDbContext _context;
        private readonly UserManager<SiteUser> _userManager;
        public VideoController(CulturNaryDbContext context, UserManager<SiteUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: api/Video
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Video>>> GetVideos()
        {
            var userId = _userManager.GetUserId(User);
            return await _context.Videos.ToListAsync();
        }

        // GET: api/Video/5
        [HttpGet("{id}")]
        public async Task<ActionResult<List<VideoDto>>> GetVideo(int id)
        {
            var videos = await _context.Videos
                            .Where(x => x.PersonId == id)
                            .Select (r => new VideoDto
                            {
                                PersonId = r.PersonId,
                                VideoName = r.VideoName,
                                VideoLink = r.VideoLink,
                                VideoNotes = r.VideoNotes,
                                VideoType = r.VideoType
                            })
                            .ToListAsync();
            return Ok(videos);
        }

        // PUT: api/Video/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVideo(int id, Video video)
        {
            if (id != video.Id)
            {
                return BadRequest();
            }

            _context.Entry(video).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VideoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(video);
        }

        // POST: api/Video
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<VideoDto>> PostVideo(VideoDto videoDto)
        {
            Video video = new Video()
            {
                PersonId = videoDto.PersonId,
                VideoName = videoDto.VideoName,
                VideoLink = videoDto.VideoLink,
                VideoNotes = videoDto.VideoNotes,
                VideoType = videoDto.VideoType

            };

            _context.Videos.Add(video);
            await _context.SaveChangesAsync();

            VideoDto createdVideoDto = new VideoDto()
            {
                PersonId = video.PersonId,
                VideoName = video.VideoName,
                VideoLink = video.VideoLink,
                VideoNotes = video.VideoNotes,
                VideoType = video.VideoType

            };
            return CreatedAtAction("GetVideo", new { id = video.Id }, createdVideoDto);
        }

        // DELETE: api/Video/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVideo(int id)
        {
            var video = await _context.Videos.FindAsync(id);
            if (video == null)
            {
                return NotFound();
            }

            _context.Videos.Remove(video);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool VideoExists(int id)
        {
            return _context.Videos.Any(e => e.Id == id);
        }
    }
}
