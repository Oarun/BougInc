using System.Net.Http;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using CulturNary.Web.Models;
using CulturNary.Web.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using CulturNary.Web.Models.DTO;
using CulturNary.Web.Services;

namespace CulturNary.Web.Controllers
{
    // [ApiController]
    // [Route("[controller]")]
    // public class MealPlannerController : ControllerBase
    // {
    //     private readonly MealPlannerService _mealPlannerService;

    //     public MealPlannerController(MealPlannerService mealPlannerService)
    //     {
    //         _mealPlannerService = mealPlannerService;
    //     }

    //     [HttpPost("plan")]
    //     public async Task<ActionResult<MealPlanResponseDto>> CreatePlan([FromBody] MealPlanRequestDto request)
    //     {
    //         try
    //         {
    //             var result = await _mealPlannerService.CreateMealPlanAsync(request);
    //             return Ok(result);
    //         }
    //         catch (Exception ex)
    //         {
    //             return BadRequest(ex.Message);
    //         }
    //     }
    // }

}
