
using System.Web;

namespace CulturNary.Web.Services
{
    public interface IRecipeSearchService
    {
        Task<string> SearchRecipesAsync(string query);
    }


    public class RecipeSearchService : IRecipeSearchService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public RecipeSearchService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<string> SearchRecipesAsync(string query)
        {
            var appId = _configuration["Edemam:RecipeAppId"];
            var appKey = _configuration["Edemam:RecipeAppKey"];
            var baseUrl = "https://api.edamam.com/api/recipes/v2";
            var type = "public";

            var encodedQuery = HttpUtility.UrlEncode(query);
            var url = $"{baseUrl}?type={type}&q={encodedQuery}&app_id={appId}&app_key={appKey}";

            var response = await _httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }
            else
            {
                throw new HttpRequestException($"Error fetching recipes: {response.ReasonPhrase}");
            }
        }
    }


}
