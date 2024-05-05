using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CulturNary.Web.Models;
using CulturNary.DAL.Abstract;
using CulturNary.DAL.Concrete;
using Microsoft.AspNetCore.Authorization;
using CulturNary.Web.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using CulturNary.Web.Areas.Identity.Data;

namespace CulturNary.Web.Controllers
{
    [Route("SocialMedia")]
    [Authorize(Roles = "Signed,Admin")]
    public class SocialMediaController : Controller
    {  
        private readonly IPersonRepository _personRepository;
        private readonly IFriendRequestRepository _friendRequestRepository;
        private readonly IFriendshipRepository _friendshipRepository;

        public SocialMediaController(
            IPersonRepository personRepository, 
            IFriendshipRepository friendshipRepository,
            IFriendRequestRepository friendRequestRepository)
        {
            _personRepository = personRepository;
            _friendRequestRepository = friendRequestRepository;
            _friendshipRepository = friendshipRepository;
        }

        // GET: SocialMedia/Friends
        [HttpGet("Friends")]
        public async Task<IActionResult> Friends()
        {
            var model = new FriendSearchModel
            {
                Users = new List<SiteUser>()
            };

            string currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userMatchPercentages = await _personRepository.GetUsersWithDietaryRestrictions(currentUserId);

            // Assign the users and match percentages to the model
            model.Users = userMatchPercentages.Keys.ToList();
            model.UserMatchPercentages = userMatchPercentages;

            // Initialize FriendshipStatus list
            model.FriendshipStatus = new List<string>();

            foreach (var user in model.Users)
            {
                // Check if they are friends
                if (_friendshipRepository.AreFriends(currentUserId, user.Id))
                {
                    model.FriendshipStatus.Add("Friend");
                }
                // Check if there's a pending friend request
                else if (_friendRequestRepository.IsFriendRequestPending(currentUserId, user.Id))
                {
                    model.FriendshipStatus.Add("Friend Request Pending");
                }
                else
                {
                    model.FriendshipStatus.Add("Send Friend Request");
                }
            }

            model.IsSubmitted = true;
            return View(model);
        }

        [HttpGet("FriendsList")]
        public async Task<IActionResult> FriendsList()
        {
            var model = new FriendModel
            {
                Friends = await _friendshipRepository.GetFriends(User.FindFirstValue(ClaimTypes.NameIdentifier)),
                FriendRequests = await _friendRequestRepository.GetFriendRequests(User.FindFirstValue(ClaimTypes.NameIdentifier))
            };
            return View(model);
        }
        [HttpGet("Messaging")]
        public async Task<IActionResult> Messaging()
        {
            return View();
        }
    }
}