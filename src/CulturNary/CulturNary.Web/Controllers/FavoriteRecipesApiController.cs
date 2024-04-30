// using System;
// using System.Threading.Tasks;
// using Microsoft.AspNetCore.Mvc;
// using CulturNary.Web.Models;
// using CulturNary.DAL.Abstract;
// using Microsoft.AspNetCore.Authorization;

// namespace CulturNary.Web.Controllers
// {
//     [Route("api/[controller]")]
//     [ApiController]
//     [Authorize(Roles = "Signed,Admin")]
//     public class FavoriteRecipesApiController : ControllerBase
//     {
//         private readonly IFavoriteRecipeRepository _favoriteRecipeRepository;

//         public FavoriteRecipesApiController(IFavoriteRecipeRepository favoriteRecipeRepository)
//         {
//             _favoriteRecipeRepository = favoriteRecipeRepository;
//         }

//         // POST: api/FavoriteRecipes
//         [HttpPost]
//         public async Task<ActionResult<FavoriteRecipe>> PostFavoriteRecipe(FavoriteRecipe favoriteRecipe)
//         {
//             try
//             {
//                 var existingRecipe = _favoriteRecipeRepository.GetFavoriteRecipeByRecipeId(favoriteRecipe.RecipeId);

//                 if (existingRecipe == null)
//                 {
//                     _favoriteRecipeRepository.Add(favoriteRecipe);
//                 }
//                 return StatusCode(200);
//             }
//             catch (Exception)
//             {
//                 return BadRequest();
//             }
//         }
//         [HttpGet]
//         public async Task<ActionResult<FavoriteRecipe>> GetFavoriteRecipe()
//         {
//             try
//             {
//                 return Ok(_favoriteRecipeRepository.GetAll());
//             }
//             catch (Exception)
//             {
//                 return BadRequest();
//             }
//         }

//         // Other methods (GET, PUT, DELETE) go here...
//     }
// }