using System.Security.Policy;
using System.Text.Json;
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

        public void SetDietaryRestrictions(List<DietaryRestriction> restrictions)
        {
            DietaryRestrictions = JsonSerializer.Serialize(restrictions);
        }
    }

    public class DietaryRestriction
    {
        public string? Name { get; set; }
        public bool Active { get; set; }
    }

}