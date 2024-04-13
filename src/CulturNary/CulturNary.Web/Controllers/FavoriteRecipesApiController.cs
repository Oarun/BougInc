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

        // POST: api/FavoriteRecipes
        [HttpPost]
        public async Task<ActionResult<FavoriteRecipe>> PostFavoriteRecipe(FavoriteRecipe favoriteRecipe)
        {
            try
            {
                _favoriteRecipeRepository.Add(favoriteRecipe);
                return StatusCode(200);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        // Other methods (GET, PUT, DELETE) go here...
    }
}