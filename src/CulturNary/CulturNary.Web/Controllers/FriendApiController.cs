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

        public FriendApiController(IPersonRepository personRepository, 
            IFriendshipRepository friendshipRepository,
            IFriendRequestRepository friendRequestRepository)
        {
            _personRepository = personRepository;
            _friendRequestRepository = friendRequestRepository;
            _friendshipRepository = friendshipRepository;
        }


        [HttpPost("SendFriendRequest/{id}")]
        public async Task<IActionResult> SendFriendRequest(string id) 
        {
            try
            {
                string currentUserId = User.FindFirstValue(System.Security.Claims.ClaimTypes.NameIdentifier);
                await _friendRequestRepository.SendFriendRequest(currentUserId, id); // Use the ID from the request body
                return Ok(new { message = "Friend request sent successfully." });
            }
            catch (HttpRequestException ex)
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable, ex.Message);
            }
        }
    }
}