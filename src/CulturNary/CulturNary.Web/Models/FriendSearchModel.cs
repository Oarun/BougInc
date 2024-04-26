using System.ComponentModel.DataAnnotations;
using CulturNary.Web.Areas.Identity.Data;
namespace CulturNary.Web.Models;
public class FriendSearchModel
{
    [Display(Name = "Alcohol-Free")]
    public bool AlcoholFree { get; set; }

    [Display(Name = "Celery-Free")]
    public bool CeleryFree { get; set; }

    [Display(Name = "Crustacean-Free")]
    public bool CrustaceanFree { get; set; }

    [Display(Name = "Dairy-Free")]
    public bool DairyFree { get; set; }

    [Display(Name = "Egg-Free")]
    public bool EggFree { get; set; }

    [Display(Name = "Fish-Free")]
    public bool FishFree { get; set; }

    [Display(Name = "Gluten-Free")]
    public bool GlutenFree { get; set; }

    [Display(Name = "Immuno-Supportive")]
    public bool ImmunoSupportive { get; set; }

    [Display(Name = "Keto-Friendly")]
    public bool KetoFriendly { get; set; }

    [Display(Name = "Kidney-Friendly")]
    public bool KidneyFriendly { get; set; }

    [Display(Name = "Kosher")]
    public bool Kosher { get; set; }

    [Display(Name = "Low-Potassium")]
    public bool LowPotassium { get; set; }

    [Display(Name = "Low-Sugar")]
    public bool LowSugar { get; set; }

    [Display(Name = "Lupine-Free")]
    public bool LupineFree { get; set; }

    [Display(Name = "Mediterranean")]
    public bool Mediterranean { get; set; }

    [Display(Name = "Mollusk-Free")]
    public bool MolluskFree { get; set; }

    [Display(Name = "Mustard-Free")]
    public bool MustardFree { get; set; }

    [Display(Name = "Paleo")]
    public bool Paleo { get; set; }

    [Display(Name = "Peanut-Free")]
    public bool PeanutFree { get; set; }

    [Display(Name = "Pescatarian")]
    public bool Pescatarian { get; set; }

    [Display(Name = "Pork-Free")]
    public bool PorkFree { get; set; }

    [Display(Name = "Red Meat-Free")]
    public bool RedMeatFree { get; set; }

    [Display(Name = "Sesame-Free")]
    public bool SesameFree { get; set; }

    [Display(Name = "Shellfish-Free")]
    public bool ShellfishFree { get; set; }

    [Display(Name = "Soy-Free")]
    public bool SoyFree { get; set; }

    [Display(Name = "Sulfite-Free")]
    public bool SulfiteFree { get; set; }

    [Display(Name = "Tree Nut-Free")]
    public bool TreeNutFree { get; set; }

    [Display(Name = "Vegan")]
    public bool Vegan { get; set; }

    [Display(Name = "Vegetarian")]
    public bool Vegetarian { get; set; }

    [Display(Name = "Wheat-Free")]
    public bool WheatFree { get; set; }


    public List<SiteUser>? Users { get; set; } = null;
    public bool IsSubmitted { get; set; } = false;
    public List<string>? FriendshipStatus { get; set; } = null;
}