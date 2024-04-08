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
            [Display(Name = "Dairy-Free")]
            public bool DairyFree { get; set; }

            [Display(Name = "Kosher")]
            public bool Kosher { get; set; }
        }

        public List<DietaryRestriction> ToDietaryRestrictionsList()
        {
            var restrictionsList = new List<DietaryRestriction>
            {
                new DietaryRestriction { Name = "Dairy-Free", Active = Input.DairyFree },
                new DietaryRestriction { Name = "Kosher", Active = Input.Kosher }
            };

            return restrictionsList;
        }

        private async Task LoadAsync(SiteUser user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var userData = await _userManager.GetUserAsync(User);
            DietaryRestrictions = userData.GetDietaryRestrictions();

            Username = userName;

            Console.WriteLine("----DietaryRestrictions----");
            Console.WriteLine(DietaryRestrictions.First().Name);
            Console.WriteLine(DietaryRestrictions.First().Active);
            Console.WriteLine("----DietaryRestrictions----");

            Input = new InputModel
            {
                DairyFree = DietaryRestrictions.FirstOrDefault(x => x.Name == "Dairy-Free").Active,
                Kosher = DietaryRestrictions.FirstOrDefault(x => x.Name == "Kosher").Active

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
