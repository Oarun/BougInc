using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CulturNary.Web.Models;
using CulturNary.DAL.Concrete;
using Microsoft.AspNetCore.Authorization;

namespace CulturNary.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Signed,Admin")]
    public class FavoriteRecipesApiController : ControllerBase
    {
        private readonly FavoriteRecipeRepository _favoriteRecipeRepository;

        public FavoriteRecipesApiController(FavoriteRecipeRepository favoriteRecipeRepository)
        {
            _favoriteRecipeRepository = favoriteRecipeRepository;
        }

        // POST: api/FavoriteRecipes
        [HttpPost]
        public async Task<ActionResult<FavoriteRecipe>> PostFavoriteRecipe(FavoriteRecipe favoriteRecipe)
        {
            try
            {
                await _favoriteRecipeRepository.AddFavoriteRecipe(favoriteRecipe);
                return CreatedAtAction("GetFavoriteRecipe", new { id = favoriteRecipe.Id }, favoriteRecipe);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        // Other methods (GET, PUT, DELETE) go here...
    }
}