using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CulturNary.Web.Models;
using CulturNary.DAL.Abstract;
using Microsoft.AspNetCore.Authorization;

namespace CulturNary.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Signed,Admin")]
    public class FavoriteRecipesApiController : ControllerBase
    {
        private readonly IFavoriteRecipeRepository _favoriteRecipeRepository;

        public FavoriteRecipesApiController(IFavoriteRecipeRepository favoriteRecipeRepository)
        {
            _favoriteRecipeRepository = favoriteRecipeRepository;
        }

        // POST: api/FavoriteRecipes/favorite/add
        [HttpPost("favorite/add")]  // Changed from [HttpPost("favorite")]
        public async Task<ActionResult<FavoriteRecipe>> PostFavoriteRecipe(FavoriteRecipe favoriteRecipe)
        {
            try
            {
                var recipeIdString = favoriteRecipe.RecipeId?.ToString();

                if (recipeIdString != null)
                {
                    var existingRecipe = _favoriteRecipeRepository.GetFavoriteRecipeByRecipeId(recipeIdString);

                    if (existingRecipe == null)
                    {
                        _favoriteRecipeRepository.Add(favoriteRecipe);
                        return StatusCode(201);
                    }
                    else
                    {
                        return StatusCode(409, "Recipe already favorited");
                    }
                }
                else
                {
                    return BadRequest("RecipeId cannot be null.");
                }
            }
            catch (Exception ex)
            {
                // Optionally log the exception here
                return BadRequest(ex.Message);
            }
        }

        // GET: api/FavoriteRecipes/favorite
        [HttpGet("favorite")]  // This route is unchanged
        public async Task<ActionResult<FavoriteRecipe>> GetFavoriteRecipes()
        {
            try
            {
                var favorites = _favoriteRecipeRepository.GetAll();
                return Ok(favorites);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        // Other methods (PUT, DELETE) go here...
    }
}
