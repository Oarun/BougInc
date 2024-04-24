using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CulturNary.Web.Models;
using CulturNary.DAL.Abstract;
using Microsoft.AspNetCore.Authorization;

namespace CulturNary.Web.Controllers
{
    [Route("SocialMedia")]
    [Authorize(Roles = "Signed,Admin")]
    public class SocialMediaController : Controller
    {
        private readonly IPersonRepository _personRepository;

        public SocialMediaController(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        // GET: SocialMedia/Friends
        [HttpGet("Friends")]
        public async Task<IActionResult> Friends()
        {
            return View();
        }

        [HttpPost("Friends")]
        public async Task<IActionResult> Friends(FriendSearchModel model)
        {
            List<SiteUser> result = await _personRepository.GetUsersWithDietaryRestrictions(model);
            // TODO: Use the data in the model to update the database
            return RedirectToAction("Friends", result);
        }
    }
}