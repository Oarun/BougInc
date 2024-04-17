using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace CulturNary.Web.Models
{
    public class Accept
    {
        public List<All> all { get; set; }
    }

    public class All
    {
        public List<string> health { get; set; }
        public List<string> dish { get; set; }
        public List<string> meal { get; set; }
    }

    public class MealTimeSection
    {
        public Accept accept { get; set; }
        public Fit fit { get; set; }
    }

    public class ENERCKCAL
    {
        public int min { get; set; }
        public int max { get; set; }
    }

    public class Fit
    {
        [JsonProperty("ENERC_KCAL")]
        public ENERCKCAL ENERCKCAL { get; set; }

        [JsonProperty("SUGAR.added")]
        public SUGARAdded SUGARAdded { get; set; }
    }

    public class Sections
    {
        public MealTimeSection Breakfast { get; set; }
        public MealTimeSection Lunch { get; set; }
        public MealTimeSection Dinner { get; set; }
    }

    public class Plan
    {
        public Accept accept { get; set; }
        public Fit fit { get; set; }
        public Sections sections { get; set; }
    }

    public class MealPlan
    {
        public int id { get; set; }
        public int personId { get; set; }
        public int size { get; set; }
        public virtual Plan plan { get; set; }
        public virtual Person person { get; set; }
    }

    public class SUGARAdded
    {
        public int max { get; set; }
    }

    public class MealPlanRequest
    {
        public List<string> Meals { get; set; }
        public List<string> Allergies { get; set; }
        public List<string> Diets { get; set; }
        public int CalorieMin { get; set; }
        public int CalorieMax { get; set; }
        public List<string> Macronutrients { get; set; }
        public List<string> Micronutrients { get; set; }
    }

}
