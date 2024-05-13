// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using CulturNary.Web.Areas.Identity.Data;
using CulturNary.Web.Models;

namespace CulturNary.Web.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class ExternalLoginModel : PageModel
    {
        private readonly SignInManager<SiteUser> _signInManager;
        private readonly UserManager<SiteUser> _userManager;
        private readonly IUserStore<SiteUser> _userStore;
        private readonly IUserEmailStore<SiteUser> _emailStore;
        private readonly IEmailSender _emailSender;
        private readonly ILogger<ExternalLoginModel> _logger;
        private readonly CulturNaryDbContext _culturNaryDbContext;

        public ExternalLoginModel(
            SignInManager<SiteUser> signInManager,
            UserManager<SiteUser> userManager,
            IUserStore<SiteUser> userStore,
            ILogger<ExternalLoginModel> logger,
            IEmailSender emailSender,
            CulturNaryDbContext culturNaryDbContext)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _userStore = userStore;
            _emailStore = GetEmailStore();
            _logger = logger;
            _emailSender = emailSender;
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
        public string ProviderDisplayName { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public string ReturnUrl { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [TempData]
        public string ErrorMessage { get; set; }

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
            public string Email { get; set; }
        }
        
        public IActionResult OnGet() => RedirectToPage("./Login");

        public IActionResult OnPost(string provider, string returnUrl = null)
        {
            // Request a redirect to the external login provider.
            var redirectUrl = Url.Page("./ExternalLogin", pageHandler: "Callback", values: new { returnUrl });
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            return new ChallengeResult(provider, properties);
        }

        public async Task<IActionResult> OnGetCallbackAsync(string returnUrl = null, string remoteError = null)
        {
            _logger.LogInformation("OnGetCallbackAsync started.");
            returnUrl = returnUrl ?? Url.Content("~/");

            if (remoteError != null)
            {
                ErrorMessage = $"Error from external provider: {remoteError}";
                _logger.LogError(ErrorMessage);
                return RedirectToPage("./Login", new { ReturnUrl = returnUrl });
            }

            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                ErrorMessage = "Error loading external login information.";
                _logger.LogError(ErrorMessage);
                return RedirectToPage("./Login", new { ReturnUrl = returnUrl });
            }

            // Sign in the user with this external login provider if the user already has a login.
            var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false, bypassTwoFactor: true);
            if (result.Succeeded)
            {
                _logger.LogInformation("{Name} logged in with {LoginProvider} provider.", info.Principal.Identity.Name, info.LoginProvider);
                return LocalRedirect(returnUrl);
            }

            if (result.IsLockedOut)
            {
                return RedirectToPage("./Lockout");
            }

            // Handle new or existing users without this external login.
            var email = info.Principal.FindFirstValue(ClaimTypes.Email);
            var user = await _userManager.FindByEmailAsync(email);
            if (user != null)
            {
                var logins = await _userManager.GetLoginsAsync(user);
                if (!logins.Any(l => l.LoginProvider == info.LoginProvider && l.ProviderKey == info.ProviderKey))
                {
                    ErrorMessage = "An account with this email already exists. Please log in using your local account.";
                    return RedirectToPage("./Login", new { ReturnUrl = returnUrl });
                }
            }
            else
            {
                // Create a new user if it doesn't exist
                user = new SiteUser { UserName = email, Email = email, EmailConfirmed = true };
                var password = GenerateRandomPassword(); // Ensure this generates a valid password
                var createResult = await _userManager.CreateAsync(user, password);
                
                if (!createResult.Succeeded)
                {
                    _logger.LogError($"User creation failed: {string.Join(", ", createResult.Errors.Select(e => e.Description))}");
                    return RedirectToPage("./Login", new { ReturnUrl = returnUrl });
                }

                var person = new Person
                {
                    IdentityId = user.Id,
                    // Set other properties of the Person entity as needed
                };
                _culturNaryDbContext.People.Add(person);
                await _culturNaryDbContext.SaveChangesAsync();
            }


            // Add the external login to the user
            var addLoginResult = await _userManager.AddLoginAsync(user, info);
            if (!addLoginResult.Succeeded)
            {
                _logger.LogError($"Failed to add external login for {email}: {string.Join(", ", addLoginResult.Errors.Select(e => e.Description))}");

                // Check if the error is because a user with the login already exists

                ErrorMessage = "An account with this login already exists. Please log in using your existing account.";

                return RedirectToPage("./Login", new { ReturnUrl = returnUrl });
            }

            // Sign in the user
            await _signInManager.SignInAsync(user, isPersistent: false);
            _logger.LogInformation("User {Email} signed in with {Provider} provider.", email, info.LoginProvider);
            return LocalRedirect(returnUrl);
        }


        private string GenerateRandomPassword()
        {
            var options = _userManager.Options.Password;
            
            // Define the characters to use in passwords.
            string lowers = "abcdefghijklmnopqrstuvwxyz";
            string uppers = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string digits = "0123456789";
            string nonAlphas = "!@#$%^&*";
            
            // Ensure the password contains the required character groups by picking randomly from each group.
            Random random = new Random();
            var passwordChars = new List<char>
            {
                lowers[random.Next(lowers.Length)], // Ensure at least one lowercase letter
                uppers[random.Next(uppers.Length)], // Ensure at least one uppercase letter
                digits[random.Next(digits.Length)], // Ensure at least one digit
                nonAlphas[random.Next(nonAlphas.Length)] // Ensure at least one non-alphanumeric character
            };

            // Fill the rest of the password length requirement with a random selection of all characters
            string allChars = lowers + uppers + digits + nonAlphas;
            int remainingLength = options.RequiredLength - passwordChars.Count;
            for (int i = 0; i < remainingLength; i++)
            {
                passwordChars.Add(allChars[random.Next(allChars.Length)]);
            }

            // Shuffle the constructed password to avoid a predictable pattern
            passwordChars = passwordChars.OrderBy(x => random.Next()).ToList();

            return new string(passwordChars.ToArray());
        }

        public async Task<IActionResult> OnPostConfirmationAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            // Get the information about the user from the external login provider
            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                ErrorMessage = "Error loading external login information during confirmation.";
                return RedirectToPage("./Login", new { ReturnUrl = returnUrl });
            }
        
            if (ModelState.IsValid)
            {
                var user = CreateUser();
        
                await _userStore.SetUserNameAsync(user, Input.Email, CancellationToken.None);
                await _emailStore.SetEmailAsync(user, Input.Email, CancellationToken.None);
        
                var result = await _userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await _userManager.AddLoginAsync(user, info);
                    if (result.Succeeded)
                    {
                        _logger.LogInformation("User created an account using {Name} provider.", info.LoginProvider);
        
                        var userId = await _userManager.GetUserIdAsync(user);
                        var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                        code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                        var callbackUrl = Url.Page(
                            "/Account/ConfirmEmail",
                            pageHandler: null,
                            values: new { area = "Identity", userId = userId, code = code },
                            protocol: Request.Scheme);
        
                        await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                            $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");
        
                        // If account confirmation is required, we need to show the link if we don't have a real email sender
                        if (_userManager.Options.SignIn.RequireConfirmedAccount)
                        {
                            return RedirectToPage("./RegisterConfirmation", new { Email = Input.Email });
                        }
        
                        await _signInManager.SignInAsync(user, isPersistent: false, info.LoginProvider);
        
                        // Add a "signed" role to the user
                        var roleResult = await _userManager.AddToRoleAsync(user, "Signed");
                        if (!roleResult.Succeeded)
                        {
                            _logger.LogError($"Failed to add 'Signed' role for {Input.Email}: {string.Join(", ", roleResult.Errors.Select(e => e.Description))}");
                        }
        
                        return LocalRedirect(returnUrl);
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
        
            ProviderDisplayName = info.ProviderDisplayName;
            ReturnUrl = returnUrl;
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
                    $"override the external login page in /Areas/Identity/Pages/Account/ExternalLogin.cshtml");
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

