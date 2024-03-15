using Microsoft.AspNetCore.Mvc;

public class ThemeViewComponent : ViewComponent
{
    public IViewComponentResult Invoke()
    {
        var theme = HttpContext.Session.GetString("Theme") ?? "default";


        return View("Theme", theme);
    }
}