using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using CulturNary.DAL.Abstract;
using CulturNary.DAL.Concrete;
using System.Threading.Tasks;
using CulturNary.Web.Services;
using System.Web;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace CulturNary.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Signed,Admin")]
    public class FriendApiController : ControllerBase
    {
        private readonly IPersonRepository _personRepository;
        private readonly IFriendRequestRepository _friendRequestRepository;
        private readonly IFriendshipRepository _friendshipRepository;
        private readonly IBlockedUserRepository _blockedUserRepository;

        public FriendApiController(IPersonRepository personRepository, 
            IFriendshipRepository friendshipRepository,
            IFriendRequestRepository friendRequestRepository,
            IBlockedUserRepository blockedUserRepository)
        {
            _blockedUserRepository = blockedUserRepository;
            _personRepository = personRepository;
            _friendRequestRepository = friendRequestRepository;
            _friendshipRepository = friendshipRepository;
        }


        [HttpPost("SendFriendRequest/{id}")]
        public async Task<IActionResult> SendFriendRequest(string id) 
        {
            if (string.IsNullOrEmpty(id) || id == User.FindFirstValue(ClaimTypes.NameIdentifier))
            {
                return BadRequest("Invalid friend ID.");
            }
            try
            {
                string currentUserId = User.FindFirstValue(System.Security.Claims.ClaimTypes.NameIdentifier);
                
                // Check if the current user is blocked by the user they are trying to send a friend request to
                if (_blockedUserRepository.IsUserBlocked(id, currentUserId))
                {
                    return BadRequest(new { message = "Unable to process the request at this time." });
                }
        
                await _friendRequestRepository.SendFriendRequest(currentUserId, id); // Use the ID from the request body
                return Ok(new { message = "Friend request sent successfully." });
            }
            catch (HttpRequestException ex)
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable, ex.Message);
            }
        }
        [HttpPost]
        [Route("RespondToFriendRequest/{requestId}/{accept}")]
        public IActionResult RespondToFriendRequest(string requestId, bool accept)
        {
            if (string.IsNullOrEmpty(requestId))
            {
                return NotFound();
            }
            try
            {
                string currentUserId = User.FindFirstValue(System.Security.Claims.ClaimTypes.NameIdentifier);
                if (accept)
                {
                    _friendshipRepository.AcceptFriendRequest(currentUserId, requestId);
                }
                else
                {
                    _friendshipRepository.RejectFriendRequest(currentUserId, requestId);
                }

                return RedirectToAction("FriendsList", "SocialMedia"); // Assuming you have a FriendsList action
            }
            catch (Exception ex)
            {
                // Handle any exceptions, e.g., friend request not found
                return BadRequest(new { error = ex.Message });
            }
        }
        [HttpPost]
        [Route("BlockFriendRequest/{id}")]
        public IActionResult BlockFriendRequest(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }
            try
            {
                string currentUserId = User.FindFirstValue(System.Security.Claims.ClaimTypes.NameIdentifier);
                _friendshipRepository.RejectFriendRequest(currentUserId, id);
                _blockedUserRepository.BlockUser(currentUserId, id);

                return RedirectToAction("FriendsList", "SocialMedia"); // Assuming you have a FriendsList action
            }
            catch (Exception ex)
            {
                // Handle any exceptions, e.g., friend request not found
                return BadRequest(new { error = ex.Message });
            }
        }
        [HttpPost]
        [Route("UnblockUser/{id}")]
        public IActionResult UnblockUser(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }
            try
            {
                string currentUserId = User.FindFirstValue(System.Security.Claims.ClaimTypes.NameIdentifier);
                _blockedUserRepository.UnblockUser(currentUserId, id);

                string redirectUrl = Url.Content("~/Identity/Account/Manage/BlockedUsers");
                return Redirect(redirectUrl);
            }
            catch (Exception ex)
            {
                // Handle any exceptions, e.g., user not found
                return BadRequest(new { error = ex.Message });
            }
        }
        [HttpPost]
        [Route("RemoveFriend/{id}")]
        public IActionResult RemoveFriend(string id)
        {
            // Get the current user's ID
            var currentUserId = User.FindFirstValue(System.Security.Claims.ClaimTypes.NameIdentifier);

            // Check if the friend exists
            var friend = _personRepository.GetPersonByIdentityId(id);
            if (friend == null)
            {
                return NotFound();
            }

            // Check if the current user and the friend are friends
            if (!_friendshipRepository.AreFriends(currentUserId, id))
            {
                return BadRequest("You are not friends with this user.");
            }

            // Remove the friend
            _friendshipRepository.RemoveFriend(currentUserId, id);

             return RedirectToAction("FriendsList", "SocialMedia");
        }
    }
}