using CulturNary.Web.Models.DTO;
using CulturNary.Web.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CulturNary.Web.Controllers
{
    // [Route("api/[controller]")]
    // [ApiController]
    // public class NewsApiController : ControllerBase
    // {
    //     private readonly NewsService _newsService;

    //     public NewsApiController(NewsService newsService)
    //     {
    //         _newsService = newsService;
    //     }

    //     [HttpGet("top-headlines")]
    //     public async Task<IActionResult> GetTopHeadlines()
    //     {
    //         var news = await _newsService.GetTopHeadlinesAsync();
    //         var dto = new NewsDto { Status = news.Status, TotalResults = news.TotalResults };
    //         return Ok(dto);
    //     }
    // }
}
