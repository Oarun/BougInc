using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc; // Add this line
using Microsoft.AspNetCore.Mvc.RazorPages;
using CulturNary.DAL.Abstract;
using CulturNary.Web.Areas.Identity.Data;
using System.Security.Claims;
public class BlockedUsersModel : PageModel
{
    private readonly IBlockedUserRepository _blockedUserRepository;
    private readonly UserManager<SiteUser> _userManager;

    public IList<SiteUser> BlockedUsers { get; set; }

    public BlockedUsersModel(IBlockedUserRepository blockedUserRepository, UserManager<SiteUser> userManager)
    {
        _blockedUserRepository = blockedUserRepository;
        _userManager = userManager;
    }

    public async Task OnGetAsync()
    {
        var user = await _userManager.GetUserAsync(User);
        var userId = user.Id;

        BlockedUsers = _blockedUserRepository.GetBlockedUsers(userId);
    }

    public Task<IActionResult> OnPostUnblockUserAsync(string id)
    {
        if (string.IsNullOrEmpty(id))
        {
            return Task.FromResult<IActionResult>(BadRequest("Invalid user ID."));
        }
        try
        {
            string currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        
            _blockedUserRepository.UnblockUser(currentUserId, id); 
            return Task.FromResult<IActionResult>(RedirectToPage());
        }
        catch (HttpRequestException ex)
        {
            return Task.FromResult<IActionResult>(StatusCode(StatusCodes.Status503ServiceUnavailable, ex.Message));
        }
    }
}