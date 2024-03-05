using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;
using CulturNary.Web.Services;
using System.Web;

namespace CulturNary.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RecipeSearchController : ControllerBase
    {
        private readonly IRecipeSearchService _recipeSearchService;

        public RecipeSearchController(IRecipeSearchService recipeSearchService)
        {
            _recipeSearchService = recipeSearchService;
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search(string q)
        {
            try
            {
                var result = await _recipeSearchService.SearchRecipesAsync(q);
                return Ok(result);
            }
            catch (HttpRequestException ex)
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable, ex.Message);
            }
        }
    }
}
