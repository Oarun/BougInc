using System.Security.Policy;
using System.Text.Json;
using CulturNary.Web.Models;
using Microsoft.AspNetCore.Identity;
using System.Text;
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

        public bool[] GetDietaryRestrictionsActiveArray()
        {
            var restrictions = GetDietaryRestrictions();
            return restrictions.Select(r => r.Active).ToArray();
        }
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
        public string GetDietaryRestrictionsString()
        {
            List<DietaryRestriction> dietaryRestrictions = GetDietaryRestrictions();
            StringBuilder restrictionsString = new StringBuilder();

            foreach (DietaryRestriction restriction in dietaryRestrictions)
            {
                if (restriction.Active)
                {
                    restrictionsString.AppendLine($"Name: {restriction.Name}, Active: {restriction.Active}");
                }
            }

            return restrictionsString.ToString();
        }

        // public void SetDietaryRestrictions(List<DietaryRestriction> restrictions)
        // {
        //     DietaryRestrictions = JsonSerializer.Serialize(restrictions);
        // }

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