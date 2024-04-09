using CulturNary.Web.Areas.Identity.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

[Authorize(Roles = "Signed")]
[Route("Favorite")]
public class UserRecommendationsController : Controller
{
    public async Task<IActionResult> Users(){
        return View("Favorite")
    }
}