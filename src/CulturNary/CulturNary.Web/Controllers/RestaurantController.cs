using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CulturNary.Web.Models;
using CulturNary.Web.Services;
using Microsoft.AspNetCore.Identity;
using CulturNary.Web.Areas.Identity.Data;
using Microsoft.AspNetCore.Authorization;
using CulturNary.Web.Models.DTO;

namespace CulturNary.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Signed,Admin")]

    public class RestaurantController : ControllerBase
    {
        private readonly CulturNaryDbContext _context;
        private readonly UserManager<SiteUser> _userManager;
        private readonly IGoogleMapsService _googleMapsService;

        public RestaurantController(CulturNaryDbContext context, UserManager<SiteUser> userManager, IGoogleMapsService googleMapsService)
        {
            _context = context;
            _userManager = userManager;
            _googleMapsService = googleMapsService;
        }

        // GET: api/Restaurant
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Restaurant>>> GetRestaurants()
        {
            var userId = _userManager.GetUserId(User);
            return await _context.Restaurants.ToListAsync();
        }

        // Get: api/Restaurant/GetGoogleApi
        [HttpGet("GetGoogleApi")]
        public async Task<ActionResult<string>> GetGoogleApi()
        {
            var apiKey = await _googleMapsService.GetApiKeyAsync();
            return Ok(new { ApiKey = apiKey });
        }

        // GET: api/Restaurant/5 Uses Person Id
        [HttpGet("{id}")]
        public async Task<ActionResult<List<RestaurantDto>>> GetRestaurant(int id)
        {
            var restaurants = await _context.Restaurants
                            .Where(x => x.PersonId == id)
                            .Select (r => new RestaurantDto
                            {
                                RestaurantsName = r.RestaurantsName,
                                RestaurantsAddress = r.RestaurantsAddress,
                                RestaurantsWebsite = r.RestaurantsWebsite,
                                RestaurantsMenu = r.RestaurantsMenu,
                                RestaurantsPhoneNumber = r.RestaurantsPhoneNumber,
                                RestaurantType = r.RestaurantType,
                                RestaurantsNotes = r.RestaurantsNotes
                            })
                            .ToListAsync();
            return Ok(restaurants);
        }

        // PUT: api/Restaurant/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRestaurant(int id, Restaurant restaurant)
        {
            if (id != restaurant.Id)
            {
                return BadRequest();
            }

            _context.Entry(restaurant).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RestaurantExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Restaurant
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<RestaurantDto>> PostRestaurant(RestaurantDto restaurantDto)
        {
            Restaurant restaurant = new Restaurant()
            {
                PersonId = restaurantDto.PersonId,
                RestaurantsName = restaurantDto.RestaurantsName,
                RestaurantsAddress = restaurantDto.RestaurantsAddress,
                RestaurantsWebsite = restaurantDto.RestaurantsWebsite,
                RestaurantsMenu = restaurantDto.RestaurantsMenu,
                RestaurantsPhoneNumber = restaurantDto.RestaurantsPhoneNumber,
                RestaurantsNotes = restaurantDto.RestaurantsNotes,
                RestaurantType = restaurantDto.RestaurantType

            };

            _context.Restaurants.Add(restaurant);
            await _context.SaveChangesAsync();

            RestaurantDto createdRestaurantDto = new RestaurantDto()
            {
                PersonId = restaurant.PersonId,
                RestaurantsName = restaurant.RestaurantsName,
                RestaurantsAddress = restaurant.RestaurantsAddress,
                RestaurantsWebsite = restaurant.RestaurantsWebsite,
                RestaurantsMenu = restaurant.RestaurantsMenu,
                RestaurantsPhoneNumber = restaurant.RestaurantsPhoneNumber,
                RestaurantsNotes = restaurant.RestaurantsNotes,
                RestaurantType = restaurant.RestaurantType

            };
            return CreatedAtAction("GetRestaurant", new { id = restaurant.Id }, createdRestaurantDto);
        }

        // DELETE: api/Restaurant/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRestaurant(int id)
        {
            var restaurant = await _context.Restaurants.FindAsync(id);
            if (restaurant == null)
            {
                return NotFound();
            }

            _context.Restaurants.Remove(restaurant);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RestaurantExists(int id)
        {
            return _context.Restaurants.Any(e => e.Id == id);
        }
    }
}
