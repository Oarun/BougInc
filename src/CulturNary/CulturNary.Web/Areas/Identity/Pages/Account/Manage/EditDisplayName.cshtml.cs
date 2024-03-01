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

namespace CulturNary.Web.Areas.Identity.Pages.Account.Manage
{
    public class DisplayNameModel : PageModel
    {
        private readonly UserManager<SiteUser> _userManager;
        private readonly SignInManager<SiteUser> _signInManager;

        public DisplayNameModel(
            UserManager<SiteUser> userManager,
            SignInManager<SiteUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public string DisplayName { get; set; }

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
            [Display(Name = "Display Name")]
            public string displayName { get; set; }
        }

        private async Task LoadAsync(SiteUser user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var userData = await _userManager.GetUserAsync(User);
            DisplayName = userData.DisplayName;

            Username = userName;

            Input = new InputModel
            {
                displayName = ""
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            //StatusMessage = string.Empty; //Clear message
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
            var displayName = userData.DisplayName;

            if (Input.displayName != displayName && !string.IsNullOrEmpty(Input.displayName))
            {
                user.DisplayName = Input.displayName;
                var updateDisplayNameResult = await _userManager.UpdateAsync(user);
                if (!updateDisplayNameResult.Succeeded)
                {
                    StatusMessage = "Unexpected error when trying to edit your display name.";
                    return RedirectToPage();
                }
                StatusMessage = "Your display name has been updated";
            }

            await _signInManager.RefreshSignInAsync(user);
            return RedirectToPage();
        }
    }
}
