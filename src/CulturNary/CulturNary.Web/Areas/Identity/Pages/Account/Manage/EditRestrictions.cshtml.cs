// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CulturNary.Web.Areas.Identity.Data;
using CulturNary.Web.Data.Migrations;
using System.Text.Json;

namespace CulturNary.Web.Areas.Identity.Pages.Account.Manage
{
    public class DietRestrictionsModel : PageModel
    {
        private readonly UserManager<SiteUser> _userManager;
        private readonly SignInManager<SiteUser> _signInManager;

        public DietRestrictionsModel(
            UserManager<SiteUser> userManager,
            SignInManager<SiteUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public List<DietaryRestriction> DietaryRestrictions { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [TempData]
        public string StatusMessage { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public class InputModel
        {
            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            /// 
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
        }

        public List<DietaryRestriction> ToDietaryRestrictionsList()
        {
            var restrictionsList = new List<DietaryRestriction>
            {
                new DietaryRestriction { Name = "Alcohol-Free", Active = Input.AlcoholFree },
                new DietaryRestriction { Name = "Celery-Free", Active = Input.CeleryFree },
                new DietaryRestriction { Name = "Crustacean-Free", Active = Input.CrustaceanFree },
                new DietaryRestriction { Name = "Dairy-Free", Active = Input.DairyFree },
                new DietaryRestriction { Name = "Egg-Free", Active = Input.EggFree },
                new DietaryRestriction { Name = "Fish-Free", Active = Input.FishFree },
                new DietaryRestriction { Name = "Gluten-Free", Active = Input.GlutenFree },
                new DietaryRestriction { Name = "Immuno-Supportive", Active = Input.ImmunoSupportive },
                new DietaryRestriction { Name = "Keto-Friendly", Active = Input.KetoFriendly },
                new DietaryRestriction { Name = "Kidney-Friendly", Active = Input.KidneyFriendly },
                new DietaryRestriction { Name = "Kosher", Active = Input.Kosher },
                new DietaryRestriction { Name = "Low-Potassium", Active = Input.LowPotassium },
                new DietaryRestriction { Name = "Low-Sugar", Active = Input.LowSugar },
                new DietaryRestriction { Name = "Lupine-Free", Active = Input.LupineFree },
                new DietaryRestriction { Name = "Mediterranean", Active = Input.Mediterranean },
                new DietaryRestriction { Name = "Mollusk-Free", Active = Input.MolluskFree },
                new DietaryRestriction { Name = "Mustard-Free", Active = Input.MustardFree },
                new DietaryRestriction { Name = "Paleo", Active = Input.Paleo },
                new DietaryRestriction { Name = "Peanut-Free", Active = Input.PeanutFree },
                new DietaryRestriction { Name = "Pescatarian", Active = Input.Pescatarian },
                new DietaryRestriction { Name = "Pork-Free", Active = Input.PorkFree },
                new DietaryRestriction { Name = "Red Meat-Free", Active = Input.RedMeatFree },
                new DietaryRestriction { Name = "Sesame-Free", Active = Input.SesameFree },
                new DietaryRestriction { Name = "Shellfish-Free", Active = Input.ShellfishFree },
                new DietaryRestriction { Name = "Soy-Free", Active = Input.SoyFree },
                new DietaryRestriction { Name = "Sulfite-Free", Active = Input.SulfiteFree },
                new DietaryRestriction { Name = "Tree Nut-Free", Active = Input.TreeNutFree },
                new DietaryRestriction { Name = "Vegan", Active = Input.Vegan },
                new DietaryRestriction { Name = "Vegetarian", Active = Input.Vegetarian },
                new DietaryRestriction { Name = "Wheat-Free", Active = Input.WheatFree }
            };

            return restrictionsList;
        }

        private async Task LoadAsync(SiteUser user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var userData = await _userManager.GetUserAsync(User);
            DietaryRestrictions = userData.GetDietaryRestrictions();

            Username = userName;

            // Console.WriteLine("----DietaryRestrictions----");
            // Console.WriteLine(DietaryRestrictions.First().Name);
            // Console.WriteLine(DietaryRestrictions.First().Active);
            // Console.WriteLine("----DietaryRestrictions----");

            Input = new InputModel
            {
                AlcoholFree = DietaryRestrictions.FirstOrDefault(x => x.Name == "Alcohol-Free")?.Active ?? false,
                CeleryFree = DietaryRestrictions.FirstOrDefault(x => x.Name == "Celery-Free")?.Active ?? false,
                CrustaceanFree = DietaryRestrictions.FirstOrDefault(x => x.Name == "Crustacean-Free")?.Active ?? false,
                DairyFree = DietaryRestrictions.FirstOrDefault(x => x.Name == "Dairy-Free")?.Active ?? false,
                EggFree = DietaryRestrictions.FirstOrDefault(x => x.Name == "Egg-Free")?.Active ?? false,
                FishFree = DietaryRestrictions.FirstOrDefault(x => x.Name == "Fish-Free")?.Active ?? false,
                GlutenFree = DietaryRestrictions.FirstOrDefault(x => x.Name == "Gluten-Free")?.Active ?? false,
                ImmunoSupportive = DietaryRestrictions.FirstOrDefault(x => x.Name == "Immuno-Supportive")?.Active ?? false,
                KetoFriendly = DietaryRestrictions.FirstOrDefault(x => x.Name == "Keto-Friendly")?.Active ?? false,
                KidneyFriendly = DietaryRestrictions.FirstOrDefault(x => x.Name == "Kidney-Friendly")?.Active ?? false,
                Kosher = DietaryRestrictions.FirstOrDefault(x => x.Name == "Kosher")?.Active ?? false,
                LowPotassium = DietaryRestrictions.FirstOrDefault(x => x.Name == "Low-Potassium")?.Active ?? false,
                LowSugar = DietaryRestrictions.FirstOrDefault(x => x.Name == "Low-Sugar")?.Active ?? false,
                LupineFree = DietaryRestrictions.FirstOrDefault(x => x.Name == "Lupine-Free")?.Active ?? false,
                Mediterranean = DietaryRestrictions.FirstOrDefault(x => x.Name == "Mediterranean")?.Active ?? false,
                MolluskFree = DietaryRestrictions.FirstOrDefault(x => x.Name == "Mollusk-Free")?.Active ?? false,
                MustardFree = DietaryRestrictions.FirstOrDefault(x => x.Name == "Mustard-Free")?.Active ?? false,
                Paleo = DietaryRestrictions.FirstOrDefault(x => x.Name == "Paleo")?.Active ?? false,
                PeanutFree = DietaryRestrictions.FirstOrDefault(x => x.Name == "Peanut-Free")?.Active ?? false,
                Pescatarian = DietaryRestrictions.FirstOrDefault(x => x.Name == "Pescatarian")?.Active ?? false,
                PorkFree = DietaryRestrictions.FirstOrDefault(x => x.Name == "Pork-Free")?.Active ?? false,
                RedMeatFree = DietaryRestrictions.FirstOrDefault(x => x.Name == "Red Meat-Free")?.Active ?? false,
                SesameFree = DietaryRestrictions.FirstOrDefault(x => x.Name == "Sesame-Free")?.Active ?? false,
                ShellfishFree = DietaryRestrictions.FirstOrDefault(x => x.Name == "Shellfish-Free")?.Active ?? false,
                SoyFree = DietaryRestrictions.FirstOrDefault(x => x.Name == "Soy-Free")?.Active ?? false,
                SulfiteFree = DietaryRestrictions.FirstOrDefault(x => x.Name == "Sulfite-Free")?.Active ?? false,
                TreeNutFree = DietaryRestrictions.FirstOrDefault(x => x.Name == "Tree Nut-Free")?.Active ?? false,
                Vegan = DietaryRestrictions.FirstOrDefault(x => x.Name == "Vegan")?.Active ?? false,
                Vegetarian = DietaryRestrictions.FirstOrDefault(x => x.Name == "Vegetarian")?.Active ?? false,
                WheatFree = DietaryRestrictions.FirstOrDefault(x => x.Name == "Wheat-Free")?.Active ?? false
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }


            var userData = await _userManager.GetUserAsync(User);
            var restrictions = userData.GetDietaryRestrictions();
            var newRestrictionsList = ToDietaryRestrictionsList();

            if (!restrictions.SequenceEqual(newRestrictionsList))
            {
                var newRestrictionsJSON = JsonSerializer.Serialize(newRestrictionsList);

                user.DietaryRestrictions = newRestrictionsJSON;

                var updateRestrictionsResult = await _userManager.UpdateAsync(user);
                if (!updateRestrictionsResult.Succeeded)
                {
                    StatusMessage = "Unexpected error when trying to edit your dietary restrictions.";
                    return RedirectToPage();
                }
                StatusMessage = "Your dietary restrictions have been updated";
            }
            
            await _signInManager.RefreshSignInAsync(user);
            return RedirectToPage();
        }
    }
}
