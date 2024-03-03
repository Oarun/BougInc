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
using CulturNary.Web.Services;

namespace CulturNary.Web.Areas.Identity.Pages.Account.Manage
{
    public class ProfileImageModel : PageModel
    {
        private readonly UserManager<SiteUser> _userManager;
        private readonly SignInManager<SiteUser> _signInManager;

        private readonly ImageStorageService _imageStorageService;

        public ProfileImageModel(
            UserManager<SiteUser> userManager,
            SignInManager<SiteUser> signInManager, ImageStorageService imageStorageService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _imageStorageService = imageStorageService;
        }

        [BindProperty]
        public IFormFile ProfileImage { get; set; }

        public string ProfileImageName { get; set; }

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
            [Display(Name = "Profile Picture")]
            public string profileImageName { get; set; }

            public IFormFile profileImage { get; set; }
        }

        private async Task LoadAsync(SiteUser user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var userData = await _userManager.GetUserAsync(User);
            ProfileImageName = userData.ProfileImageName;

            Username = userName;

            Input = new InputModel
            {
                profileImageName = ProfileImageName,
                profileImage = ProfileImage
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

            if(Input.profileImage != null && Input.profileImage.Length > 0 && Input.profileImage != ProfileImage)
            {

                var filePath = await _imageStorageService.UploadImageAsync(Input.profileImage);
                user.ProfileImageName = filePath;

                var updateProfileImageResult = await _userManager.UpdateAsync(user);
                if (!updateProfileImageResult.Succeeded)
                {
                    StatusMessage = "Unexpected error when trying to edit your profile image.";
                    return RedirectToPage();
                }
                StatusMessage = "Your profile image has been updated";
            }
            else
            {
                StatusMessage = "Unexpected error, image file is empty or corrupted.";
                return RedirectToPage();
            }


            await _signInManager.RefreshSignInAsync(user);
            return RedirectToPage();
        }
    }
}
