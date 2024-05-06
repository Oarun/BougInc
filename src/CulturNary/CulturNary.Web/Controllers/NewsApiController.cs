using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Threading.Tasks;
using CulturNary.Web.Services;
using System.Web;
using Microsoft.AspNetCore.Authorization;

namespace CulturNary.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Signed,Admin")]
    public class NewsApiController : ControllerBase
    {
        private readonly INewsService _newsService;

        public NewsApiController(INewsService newsService)
        {
            _newsService = newsService;
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search(string q)
        {
            try
            {
                var result = await _newsService.GetNewsAsync(q);
                return Ok(result);
            }
            catch (HttpRequestException ex)
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable, ex.Message);
            }
        }
    }
}
