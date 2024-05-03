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
        public async Task<ActionResult<FavoriteRecipe>> AddFavoriteRecipe(FavoriteRecipe favoriteRecipe)
        {
            try
            {
                var existingRecipe = _favoriteRecipeRepository.GetFavoriteRecipeByRecipeId(favoriteRecipe.RecipeId);

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
            catch (Exception)
            {
                return BadRequest();
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
