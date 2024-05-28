using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CulturNary.Web.Models;
using Microsoft.AspNetCore.Authorization;
using System.Net.Mail;
using System.Net;

namespace CulturNary.Web.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    public IActionResult About()
    {
        return View();
    }

    [HttpGet]
    public IActionResult Contact()
    {
        return View();
    }

    [Authorize(Roles = "Signed,Admin")]
    public IActionResult MealPlanner()
    {
        return View();
    }

    [Authorize(Roles = "Signed,Admin")]
    public IActionResult MealPlanGenerator()
    {
        return View();
    }

    [Authorize(Roles = "Signed,Admin")]
    public IActionResult GroceryList()
    {
        return View();
    }

    [Authorize(Roles = "Signed,Admin")]
    public IActionResult Collections()
    {
        return View();
    }
    [Authorize(Roles = "Signed,Admin")]
    public IActionResult SearchEngines()
    {
        return View();
    }
    [Authorize(Roles = "Signed,Admin")]
    public IActionResult RecipeSearchEngine()
    {
        return View();
    }

    [Authorize(Roles = "Signed,Admin")]
    public IActionResult Tools()
    {
        return View();
    }

    [Authorize(Roles = "Signed,Admin")]
    public IActionResult Restaurants()
    {
        return View();
    }

    [Authorize(Roles = "Signed,Admin")]
    public IActionResult Video()
    {
        return View();
    }

    [Authorize(Roles = "Signed,Admin")]
    public IActionResult AiAssistant()
    {
        return View();
    }

    [Authorize(Roles = "Signed,Admin")]
    public IActionResult News()
    {
        return View();
    }

    public IActionResult FAQ()
    {
        return View();
    }


    [Route("Home/Error/{code?}")]
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    [HttpGet]
    public IActionResult Error(int? code)
    {
        var errorViewModel = new ErrorViewModel
        {
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
            StatusCode = code
        };

        Console.WriteLine($"Error: {code}");

        return View(errorViewModel);

    }

    [HttpPost]
    public IActionResult SwitchBright(bool isDark)
    {

        string theme = isDark ? "dark" : "light";

        HttpContext.Session.SetString("ThemeBrightness", theme);

        var refererUrl = Request.Headers["Referer"].ToString();
        if(!string.IsNullOrEmpty(refererUrl))
        {
            return Redirect(refererUrl);
        }

        return RedirectToAction("Index", "Home");
    }

    [HttpPost]
    public IActionResult SwitchColor(string primaryColor)
    {
        HttpContext.Session.SetString("ThemeColor", primaryColor);

        var refererUrl = Request.Headers["Referer"].ToString();
        if(!string.IsNullOrEmpty(refererUrl))
        {
            return Redirect(refererUrl);
        }

        return RedirectToAction("Index", "Home");
    }
}
