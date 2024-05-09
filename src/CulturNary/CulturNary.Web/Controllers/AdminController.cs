using CulturNary.Web.Areas.Identity.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

[Authorize(Roles = "Admin")]
[Route("Admin")]
public class AdminController : Controller
{
    private readonly UserManager<SiteUser> _userManager; 

    public AdminController(UserManager<SiteUser> userManager)
    {
        _userManager = userManager;
    }
    [HttpGet("IsUserAdmin")]
    public async Task<bool> IsUserAdmin(SiteUser user)
    {
        var roles = await _userManager.GetRolesAsync(user);
        return roles.Contains("Admin");
    }
    [HttpGet("Users")]
    public async Task<IActionResult> Users()
    {
        var users = await _userManager.Users.AsNoTracking().ToListAsync();
        var userIsAdmin = new Dictionary<SiteUser, bool>();
        foreach (var user in users)
        {
            userIsAdmin[user] = await IsUserAdmin(user);
        }
        var currentUser = await _userManager.GetUserAsync(User);
        ViewBag.CurrentUserId = currentUser?.Id;
        return View(userIsAdmin);
    }
    [HttpPost("MakeAdmin/{userId}")]
    public async Task<IActionResult> MakeAdmin(string userId)
    {
        try
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound();
            }
            await _userManager.AddToRoleAsync(user, "Admin");
            return Ok();
        }
        catch (Exception ex)
        {
            // Log the exception
            Console.WriteLine(ex);
            // Return a 500 Internal Server Error status code
            return StatusCode(500);
        }
    }
    [HttpPost("DeleteUser/{userId}")]
    public async Task<IActionResult> DeleteUser(string userId)
    {
        try
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound();
            }
            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
            {
                return BadRequest();
            }
            return Ok();
        }
        catch (Exception ex)
        {
            // Log the exception
            Console.WriteLine(ex);
            // Return a 500 Internal Server Error status code
            return StatusCode(500);
        }
    }
    [HttpGet("Users/{userId}")]
    public async Task<IActionResult> GetUser(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            return NotFound();
        }

        var userIsAdmin = await IsUserAdmin(user);

        return Ok(new
        {
            user.Id,
            user.UserName,
            user.NormalizedUserName,
            user.Email,
            user.NormalizedEmail,
            user.EmailConfirmed,
            user.PhoneNumber,
            user.TwoFactorEnabled,
            IsAdmin = userIsAdmin
        });
    }
}