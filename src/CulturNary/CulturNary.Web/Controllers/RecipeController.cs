using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CulturNary.Web.Models;
using CulturNary.Web.Models.DTO;
using CulturNary.Web.Areas.Identity.Data;
using Microsoft.AspNetCore.Authorization;

namespace CulturNary.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Signed,Admin")]
    public class RecipeController : ControllerBase
    {
        private readonly CulturNaryDbContext _context;

        public RecipeController(CulturNaryDbContext context)
        {
            _context = context;
        }

        // GET: api/Recipe
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Recipe>>> GetRecipes()
        {
            return await _context.Recipes.ToListAsync();
        }

        // GET: api/Recipes/5 This uses CollectionId 
        [HttpGet("{collectionId}")]
        public async Task<ActionResult<List<RecipeDto>>> GetRecipes(int collectionId)
        {
            var Recipes = await _context.Recipes
                                    .Where(x => x.CollectionId == collectionId)
                                    .Select(c => new RecipeDto
                                    {
                                        Id = c.Id,
                                        CollectionId = c.CollectionId,
                                        Name = c.Name,
                                        Description = c.Description,
                                        RecipeImg = c.Img,
                                        PersonId = c.PersonId,
                                        Uri = c.Uri
                                        // Map additional properties if needed
                                    })
                                    .ToListAsync();

            return Ok(Recipes);
        }

         // GET: api/Recipe/ByPersonId/5 This uses person id
        [HttpGet("ByPersonId/{id}")]
        public async Task<ActionResult<List<RecipeDto>>> GetRecipesByPersonId(int id)
        {
            var recipes = await _context.Recipes
                                    .Where(x => x.PersonId == id)
                                    .Select(c => new RecipeDto
                                    {
                                        Id = c.Id,
                                        PersonId = c.PersonId,
                                        Name = c.Name,
                                        Description = c.Description,
                                        RecipeImg = c.Img,
                                        Uri = c.Uri
                                        // Map additional properties if needed
                                    })
                                    .ToListAsync();

            return Ok(recipes);
        }

        // PUT: api/Recipe/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRecipe(int id, Recipe recipe)
        {
            if (id != recipe.Id)
            {
                return BadRequest();
            }

            _context.Entry(recipe).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RecipeExists(id))
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

        // POST: api/Recipe
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Recipe>> PostRecipe(Recipe recipe)
        {
            _context.Recipes.Add(recipe);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRecipe", new { id = recipe.Id }, recipe);
        }

        // DELETE: api/Recipe/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRecipe(int id)
        {
            var recipe = await _context.Recipes.FindAsync(id);
            if (recipe == null)
            {
                return NotFound();
            }

            _context.Recipes.Remove(recipe);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RecipeExists(int id)
        {
            return _context.Recipes.Any(e => e.Id == id);
        }
    }
}
