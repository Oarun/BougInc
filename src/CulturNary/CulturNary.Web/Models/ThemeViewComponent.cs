using Microsoft.AspNetCore.Mvc;

namespace CulturNary.Web.Models;

public class ThemeViewComponent : ViewComponent
{
    public IViewComponentResult Invoke()
    {
        var brightness = HttpContext.Session.GetString("ThemeBrightness") ?? "default";
        var color = HttpContext.Session.GetString("ThemeColor") ?? "default";

        var theme = new ThemeViewModel
        {
            Brightness = brightness,
            Color = color
        };

        return View("Theme", theme);
    }
}

public class ThemeViewModel
{
    public string Brightness { get; set; }
    public string Color { get; set; }
}