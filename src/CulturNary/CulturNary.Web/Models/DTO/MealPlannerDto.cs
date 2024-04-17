using Microsoft.CodeAnalysis.CSharp.Syntax;
using NuGet.Protocol.Plugins;


namespace CulturNary.Web.Models.DTO
{
    public class NutrientRange
    {
        public double Min { get; set; }
        public double Max { get; set; }
    }

    public class MealPlanRequestDto
    {
        public List<string> Meals { get; set; }
        public List<string> Allergies { get; set; }
        public List<string> Diets { get; set; }
        public int CalorieMin { get; set; }
        public int CalorieMax { get; set; }
        public Dictionary<string, NutrientRange> Macronutrients { get; set; }
        public Dictionary<string, NutrientRange> Micronutrients { get; set; }
    }

    public class MealPlanResponseDto
    {
        public string Status { get; set; }
        public List<MealDayDto> Meals { get; set; }
    }

    public class MealDayDto
    {
        public List<MealDto> Breakfast { get; set; }
        public List<MealDto> Lunch { get; set; }
        public List<MealDto> Dinner { get; set; }
    }

    public class MealDto
    {
        public string Recipe { get; set; }
        public string Uri { get; set; }
    }

}