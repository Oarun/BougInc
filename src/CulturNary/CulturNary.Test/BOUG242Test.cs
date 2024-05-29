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
using CulturNary.Web.Models;
using CulturNary.Web.Areas.Identity.Pages.Account.Manage;
using System.Security.Claims;
using NUnit.Framework;
using Moq;
using Microsoft.Extensions.Options;

public class DownloadPersonalDataModelTests
{
    // [Test]
    // public async Task OnPostAsync_WhenCalled_AddsFavoriteRecipesToPersonalData()
    // {
    // // Arrange
    // var userStoreMock = new Mock<IUserStore<SiteUser>>();
    // var userManagerMock = new Mock<UserManager<SiteUser>>(
    //     userStoreMock.Object, 
    //     null, 
    //     null, 
    //     null, 
    //     null, 
    //     null, 
    //     null, 
    //     null, 
    //     null);
    // var loggerMock = new Mock<ILogger<DownloadPersonalDataModel>>();
    // var favoriteRecipeRepositoryMock = new Mock<IFavoriteRecipeRepository>();
    // var personRepositoryMock = new Mock<IPersonRepository>();

    // var user = new SiteUser { Id = "test-user-id" };
    // var person = new Person { Id = 1, IdentityId = user.Id };
    // var favoriteRecipes = new List<FavoriteRecipe>
    // {
    //     new FavoriteRecipe { Label = "Recipe 1", Uri = "http://example.com/recipe1" },
    //     new FavoriteRecipe { Label = "Recipe 2", Uri = "http://example.com/recipe2" }
    // };

    // userManagerMock.Setup(um => um.GetUserAsync(It.IsAny<ClaimsPrincipal>())).ReturnsAsync(user);
    // personRepositoryMock.Setup(pr => pr.GetPersonByIdentityId(user.Id)).Returns(person);
    // favoriteRecipeRepositoryMock.Setup(frr => frr.GetFavoriteRecipeForPersonID(person.Id)).Returns(favoriteRecipes);

    // var model = new DownloadPersonalDataModel(userManagerMock.Object, loggerMock.Object, favoriteRecipeRepositoryMock.Object, personRepositoryMock.Object);

    // // Act
    // var result = await model.OnPostAsync();

    // // Assert
    // var fileResult = result as FileContentResult;
    // Assert.NotNull(fileResult);

    // var personalData = JsonSerializer.Deserialize<Dictionary<string, string>>(fileResult.FileContents);
    // Assert.True(personalData.ContainsKey("Favorite Recipes"));
    // Assert.AreEqual("Label: Recipe 1, Uri: http://example.com/recipe1, Label: Recipe 2, Uri: http://example.com/recipe2", personalData["Favorite Recipes"]);
    // }
}