// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using CulturNary.Web.Areas.Identity.Data;
using AspNetCore.ReCaptcha;
using CulturNary.Web.Models; 
using CulturNary.Web.Data;

namespace CulturNary.Web.Areas.Identity.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<SiteUser> _signInManager;
        private readonly UserManager<SiteUser> _userManager;
        private readonly IUserStore<SiteUser> _userStore;
        private readonly IUserEmailStore<SiteUser> _emailStore;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly IReCaptchaService _recaptcha;
        private readonly CulturNaryDbContext _culturNaryDbContext;

        public RegisterModel(
            UserManager<SiteUser> userManager,
            IUserStore<SiteUser> userStore,
            SignInManager<SiteUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender,
            IReCaptchaService recaptcha,
            CulturNaryDbContext culturNaryDbContext)
        {
            _userManager = userManager;
            _userStore = userStore;
            _emailStore = GetEmailStore();
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _recaptcha = recaptcha;
            _culturNaryDbContext = culturNaryDbContext;
        }

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
        public string ReturnUrl { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public IList<AuthenticationScheme> ExternalLogins { get; set; }
        

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
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
            [Required]
            public string RecaptchaResponse { get; set; }
            [Display(Name = "Optional Keyword")]
            public string AdminKeyword {get; set;}
        }


        public async Task<IActionResult> OnGetAsync(string returnUrl = null)
        {
            if(User.Identity.IsAuthenticated){
                return RedirectToPage("/Index");
            }
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            return Page();
        }
        public bool AuthorizationKeyword(string keyword){
            var adminKeyword = Environment.GetEnvironmentVariable("ADMIN_KEYWORD");
            return keyword == adminKeyword;
        }
        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                if(Input.RecaptchaResponse == ""){
                    ModelState.AddModelError(string.Empty, "You failed the CAPTCHA.");
                    return Page();
                }
                var user = CreateUser();
                if (user == null)
                {
                    _logger.LogInformation("User is null");
                }
                await _userStore.SetUserNameAsync(user, Input.Email, CancellationToken.None);
                await _emailStore.SetEmailAsync(user, Input.Email, CancellationToken.None);
                user.EmailConfirmed = true;
                var result = await _userManager.CreateAsync(user, Input.Password);

                if (result.Succeeded)
                {
                    if (AuthorizationKeyword(Input.AdminKeyword))
                    {
                        if (!await _userManager.IsInRoleAsync(user, "Admin"))
                        {
                            var roleExist = await _userManager.AddToRoleAsync(user, "Admin");
                            if (!roleExist.Succeeded)
                            {
                                ModelState.AddModelError(string.Empty, "Error while adding role to user. ");
                                return Page();
                            }
                        }
                    }
                    else
                    {
                        var roleExist = await _userManager.AddToRoleAsync(user, "Signed");
                        if (!roleExist.Succeeded)
                        {
                            ModelState.AddModelError(string.Empty, "Error while adding role to user. ");
                            return Page();
                        }
                    }
                    _logger.LogInformation("User created a new account with password.");

                    var userId = await _userManager.GetUserIdAsync(user);

                    // Create a new Person entity
                    var person = new Person
                    {
                        IdentityId = user.Id,
                        // Set other properties of the Person entity as needed
                    };
                    // Add the Person entity to the CulturNary DbContext and save changes
                    _culturNaryDbContext.People.Add(person);
                    await _culturNaryDbContext.SaveChangesAsync();

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = userId, code = code, returnUrl = returnUrl },
                        protocol: Request.Scheme);

                    
                    await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    }
                    
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return LocalRedirect(returnUrl);
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }

        private SiteUser CreateUser()
        {
            try
            {
                return Activator.CreateInstance<SiteUser>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(SiteUser)}'. " +
                    $"Ensure that '{nameof(SiteUser)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            }
        }

        private IUserEmailStore<SiteUser> GetEmailStore()
        {
            if (!_userManager.SupportsUserEmail)
            {
                throw new NotSupportedException("The default UI requires a user store with email support.");
            }
            return (IUserEmailStore<SiteUser>)_userStore;
        }
    }
}
