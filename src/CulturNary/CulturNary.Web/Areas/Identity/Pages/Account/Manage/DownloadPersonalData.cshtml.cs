// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using CulturNary.Web.Areas.Identity.Data;
using CulturNary.DAL.Abstract;
using CulturNary.DAL.Concrete;
namespace CulturNary.Web.Areas.Identity.Pages.Account.Manage
{
    public class DownloadPersonalDataModel : PageModel
    {
        private readonly UserManager<SiteUser> _userManager;
        private readonly ILogger<DownloadPersonalDataModel> _logger;
        private readonly IFavoriteRecipeRepository _favoriteRecipeRepository;
        private readonly IPersonRepository _personRepository;  // Add this line

        public DownloadPersonalDataModel(
            UserManager<SiteUser> userManager,
            ILogger<DownloadPersonalDataModel> logger,
            IFavoriteRecipeRepository favoriteRecipeRepository,
            IPersonRepository personRepository)  // Add this line
        {
            _userManager = userManager;
            _logger = logger;
            _favoriteRecipeRepository = favoriteRecipeRepository;
            _personRepository = personRepository;  // Add this line
        }

        public IActionResult OnGet()
        {
            return NotFound();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            _logger.LogInformation("User with ID '{UserId}' asked for their personal data.", _userManager.GetUserId(User));

            // Only include personal data for download
            var personalData = new Dictionary<string, string>();
            var personalDataProps = typeof(SiteUser).GetProperties().Where(
                            prop => Attribute.IsDefined(prop, typeof(PersonalDataAttribute)));
            foreach (var p in personalDataProps)
            {
                personalData.Add(p.Name, p.GetValue(user)?.ToString() ?? "null");
            }

            // Get the person associated with the current identity id
            var person = _personRepository.GetPersonByIdentityId(user.Id);

            // If the person exists, get their favorite recipes
            if (person != null)
            {
                var favoriteRecipes = _favoriteRecipeRepository.GetFavoriteRecipeForPersonID(person.Id);
                personalData.Add("Favorite Recipes", string.Join(", ", favoriteRecipes.Select(r => $"Label: {r.Label}, Uri: {r.Uri}")));
            }

            var logins = await _userManager.GetLoginsAsync(user);
            foreach (var l in logins)
            {
                personalData.Add($"{l.LoginProvider} external login provider key", l.ProviderKey);
            }

            personalData.Add($"Authenticator Key", await _userManager.GetAuthenticatorKeyAsync(user));

            Response.Headers.TryAdd("Content-Disposition", "attachment; filename=PersonalData.json");
            return new FileContentResult(JsonSerializer.SerializeToUtf8Bytes(personalData), "application/json");
        }
    }
}
