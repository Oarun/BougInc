using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CulturNary.Web.Models;
using CulturNary.DAL.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;  // Add this line
using System.Security.Claims;

namespace CulturNary.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Signed,Admin")]
    public class FavoriteRecipesApiController : ControllerBase
    {
        private readonly IFavoriteRecipeRepository _favoriteRecipeRepository;
        private readonly IPersonRepository _personRepository;  // Add this line

        public FavoriteRecipesApiController(IFavoriteRecipeRepository favoriteRecipeRepository, IPersonRepository personRepository)  // Update this line
        {
            _favoriteRecipeRepository = favoriteRecipeRepository;
            _personRepository = personRepository;  // Add this line
        }

        // POST: api/FavoriteRecipes/favorite/add
        [HttpPost("favorite/add")]  // Changed from [HttpPost("favorite")]
        public async Task<ActionResult<FavoriteRecipe>> PostFavoriteRecipe(FavoriteRecipe favoriteRecipe)
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
        [HttpGet("favorite")]
        public async Task<ActionResult<FavoriteRecipe>> GetFavoriteRecipes()
        {
            try
            {
                // Get the current user's ID
                string currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var personOfInterest = _personRepository.GetPersonByIdentityId(currentUserId);
                // Get all favorite recipes for the current user
                var favorites = _favoriteRecipeRepository.GetFavoriteRecipeForPersonID(personOfInterest.Id);

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
