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

namespace CulturNary.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CollectionController : ControllerBase
    {
        private readonly CulturNaryDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;


        public CollectionController(CulturNaryDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: api/Collection
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Collection>>> GetCollections()
        {
            var userId = _userManager.GetUserId(User);
            return await _context.Collections.ToListAsync();
        }

        // GET: api/Collection/5 This uses Person Id 
        [HttpGet("{id}")]
        public async Task<ActionResult<List<CollectionDto>>> GetCollections(int id)
        {
            var collections = await _context.Collections
                                    .Where(x => x.PersonId == id)
                                    .Select(c => new CollectionDto
                                    {
                                        Id = c.Id,
                                        PersonId = c.PersonId,
                                        Name = c.Name,
                                        Description = c.Description
                                        // Map additional properties if needed
                                    })
                                    .ToListAsync();

            return Ok(collections);
        }

        // GET: api/Collection/ById/5 This uses collecton id
        [HttpGet("ById/{id}")]
        public async Task<ActionResult<CollectionDto>> GetCollectionById(int id)
        {
            // Find the collection by its ID
            var collection = await _context.Collections
                .Where(c => c.Id == id)
                .Select(c => new CollectionDto
                {
                    Id = c.Id,
                    PersonId = c.PersonId,
                    Name = c.Name,
                    Description = c.Description
                    // Map additional properties if needed
                })
                .FirstOrDefaultAsync();

            // If the collection is not found, return NotFound
            if (collection == null)
            {
                return NotFound();
            }

            // Return the collection
            return Ok(collection);
        }

        // PUT: api/Collection/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCollection(int id, CollectionDto collectionDto)
        {
            if (id != collectionDto.Id)
            {
                return BadRequest();
            }

            // Retrieve the collection entity from the database
            var collection = await _context.Collections.FindAsync(id);
            if (collection == null)
            {
                return NotFound();
            }

            // Update the properties of the retrieved collection entity
            collection.Name = collectionDto.Name;
            collection.Description = collectionDto.Description;

            try
            {
                // Save changes to the database
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CollectionExists(id))
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

        // POST: api/Collection
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CollectionDto>> PostCollection(CollectionDto collectionDto)
        {
            // Map the properties from CollectionDto to Collection
            Collection collection = new Collection()
            {
                PersonId = collectionDto.PersonId,
                Name = collectionDto.Name,
                Description = collectionDto.Description
            };

            _context.Collections.Add(collection);
            await _context.SaveChangesAsync();

            // Map the Collection object back to CollectionDto before returning it
            CollectionDto createdCollectionDto = new CollectionDto()
            {
                Id = collection.Id,
                Name = collection.Name,
                Description = collection.Description
            };

            return CreatedAtAction("GetCollection", new { id = collection.Id }, createdCollectionDto);
        }

        // DELETE: api/Collection/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCollection(int id)
        {
            var collection = await _context.Collections.FindAsync(id);
            if (collection == null)
            {
                return NotFound();
            }
            // Delete dependent records (recipes) associated with the collection
            var recipesToDelete = _context.Recipes.Where(r => r.CollectionId == id);
            _context.Recipes.RemoveRange(recipesToDelete);

            _context.Collections.Remove(collection);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CollectionExists(int id)
        {
            return _context.Collections.Any(e => e.Id == id);
        }
    }
}
