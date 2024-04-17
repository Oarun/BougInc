using CulturNary.Web.Models;
using CulturNary.Web.Models.DTO;

public class MealPlannerService
{
    private readonly HttpClient _httpClient;
    private readonly string _appId;

    public MealPlannerService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _appId = configuration["Edamam:MealAppId"]; // Ensure this setting is in your appsettings.json
        _httpClient.BaseAddress = new Uri(configuration["MealPlanningAPI:BaseUrl"]); // Set the base URL from configuration
    }

    private List<object> BuildFilters(List<string> userSelections, Dictionary<string, string> filterOptions)
    {
        return userSelections
            .Where(key => filterOptions.ContainsKey(key))
            .Select(key => new { health = filterOptions[key] })
            .ToList<object>();
    }


    public async Task<MealPlanResponseDto> CreateMealPlanAsync(MealPlanRequestDto request)
    {
        var allergyFilters = BuildFilters(request.Allergies, NutritionLabels.Allergies);
        var dietFilters = BuildFilters(request.Diets, NutritionLabels.Diets);
        var nutrientFilters = new Dictionary<string, object>();

        foreach (var macro in request.Macronutrients)
        {
            nutrientFilters[NutritionLabels.Macronutrients[macro.Key]] = new { min = macro.Value.Min, max = macro.Value.Max };
        }

        foreach (var micro in request.Micronutrients)
        {
            nutrientFilters[NutritionLabels.Micronutrients[micro.Key]] = new { min = micro.Value.Min, max = micro.Value.Max };
        }

        var requestBody = new
        {
            size = 7,
            plan = new
            {
                accept = new { all = allergyFilters.Concat(dietFilters).ToList() },
                fit = new
                {
                    ENERC_KCAL = new { min = request.CalorieMin, max = request.CalorieMax },
                    nutrients = nutrientFilters
                },
                sections = new
                {
                    Breakfast = new
                    {
                        accept = new
                        {
                            all = new List<object>
                            {
                                new { dish = new List<string>() },
                                new { meal = new List<string> { "breakfast" } }
                            }
                        },
                        fit = new
                        {
                            ENERC_KCAL = new { min = 300, max = 500 }
                        }
                    },
                    Lunch = new
                    {
                        accept = new
                        {
                            all = new List<object>
                            {
                                new { dish = new List<string>() },
                                new { meal = new List<string> { "lunch" } }
                            }
                        },
                        fit = new
                        {
                            ENERC_KCAL = new { min = 500, max = 800 }
                        }
                    },
                    Dinner = new
                    {
                        accept = new
                        {
                            all = new List<object>
                            {
                                new { dish = new List<string>() },
                                new { meal = new List<string> { "dinner" } }
                            }
                        },
                        fit = new
                        {
                            ENERC_KCAL = new { min = 600, max = 900 }
                        }
                    }
                }
            }
        };

        string requestUrl = $"{_appId}/select";
        var response = await _httpClient.PostAsJsonAsync(requestUrl, requestBody);
        if (!response.IsSuccessStatusCode)
        {
            throw new InvalidOperationException($"API call failed: {response.ReasonPhrase}");
        }

        return await response.Content.ReadFromJsonAsync<MealPlanResponseDto>();
    }
}
