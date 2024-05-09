using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CulturNary.Web.Models;
using CulturNary.Web.Services;
using Microsoft.AspNetCore.Identity;
using CulturNary.Web.Areas.Identity.Data;
using Microsoft.AspNetCore.Authorization;
using CulturNary.Web.Models.DTO;
using Newtonsoft.Json;
using System.Web;


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

         // Get: api/Restaurant/NearbySearchRestaurants
        [HttpGet("NearbySearchRestaurants")]
        public async Task<ActionResult> NearbySearchRestaurants(double latitude, double longitude, int radius, string type)
        {
            HttpClient client = new HttpClient();
            var apiKey = await _googleMapsService.GetApiKeyAsync();
            string url = $"https://maps.googleapis.com/maps/api/place/nearbysearch/json?location={latitude},{longitude}&radius={radius}&type={type}&key={apiKey}";

            HttpResponseMessage response = await client.GetAsync(url);
            string responseBody = await response.Content.ReadAsStringAsync();

            // Forward the response from Google Maps API to the client
            return Content(responseBody, "application/json");
        }

        [HttpPost("RestaurantAddressConvert")]
        public async Task<ActionResult<List<(double Latitude, double Longitude)>>> RestaurantAddressConvert(List<string> addressList)
        {
            List<(double Latitude, double Longitude)> coordinates = new List<(double, double)>();
            HttpClient client = new HttpClient();
            var apiKey = await _googleMapsService.GetApiKeyAsync();

            foreach (string address in addressList)
            {
                string newAddress = address.Replace(" ", "%20");
                string url = $"https://maps.googleapis.com/maps/api/geocode/json?address={newAddress}&key={apiKey}";

                HttpResponseMessage response = await client.GetAsync(url);
                string responseBody = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    // Parse JSON response to extract coordinates
                    var data = JsonConvert.DeserializeObject<dynamic>(responseBody);
                    if (data.results.Count > 0)
                    {
                        var location = data.results[0].geometry.location;
                        double latitude = location.lat;
                        double longitude = location.lng;
                        coordinates.Add((latitude, longitude));
                    }
                    else
                    {
                        // Address not found (possibly invalid) - continue to next address
                        continue;
                    }
                }
                else
                {
                    // Handle API errors (e.g., log the error)
                    // You could also return a specific error message here
                }
            }

            return Ok(coordinates);
        }

        // GET: api/Restaurant/5 Uses Person Id
        [HttpGet("{id}")]
        public async Task<ActionResult<List<RestaurantDto>>> GetRestaurant(int id)
        {
            var restaurants = await _context.Restaurants
                            .Where(x => x.PersonId == id)
                            .Select (r => new RestaurantDto
                            {
                                Id = r.Id,
                                PersonId = r.PersonId,
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
                Id = restaurant.Id,
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
