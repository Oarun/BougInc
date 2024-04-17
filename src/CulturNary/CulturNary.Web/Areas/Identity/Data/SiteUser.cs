using System.Security.Policy;
using System.Text.Json;
using CulturNary.Web.Models;
using Microsoft.AspNetCore.Identity;

namespace CulturNary.Web.Areas.Identity.Data
{
    public class SiteUser : IdentityUser
    {

        [PersonalData]
        public string? DisplayName { get; set; }
        [PersonalData]
        public string? Biography { get; set; }
        [PersonalData]
        public string? ProfileImageName { get; set; }
        [PersonalData]
        public string? UserLikes { get; set; }
        [PersonalData]
        public string? UserDislikes { get; set; }
        [PersonalData]
        public string? DietaryRestrictions { get; set; }
        [PersonalData]
        public string? MealPlans { get; set; }


        public List<DietaryRestriction> GetDietaryRestrictions()
        {
            if (string.IsNullOrEmpty(DietaryRestrictions))
            {
                return new List<DietaryRestriction>();
            }
            else
            {
                return JsonSerializer.Deserialize<List<DietaryRestriction>>(DietaryRestrictions);
            }
        }

        // public void SetDietaryRestrictions(List<DietaryRestriction> restrictions)
        // {
        //     DietaryRestrictions = JsonSerializer.Serialize(restrictions);
        // }

        public List<MealPlan>? GetMealPlans()
        {
            return string.IsNullOrEmpty(MealPlans) ?
            new List<MealPlan>() :
                JsonSerializer.Deserialize<List<MealPlan>>(MealPlans);
        }

        public void SetMealPlans(List<MealPlan> mealPlans)
        {
            MealPlans = JsonSerializer.Serialize(mealPlans);
        }
    }

    public class DietaryRestriction : IEquatable<DietaryRestriction>
    {
        public string? Name { get; set; }
        public bool Active { get; set; }

        public bool Equals(DietaryRestriction other)
        {
            if (other == null) return false;
            return this.Name == other.Name && this.Active == other.Active;
        }
        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Active);
        }

    }

}