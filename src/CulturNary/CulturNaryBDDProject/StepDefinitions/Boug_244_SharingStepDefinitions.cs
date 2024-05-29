using Reqnroll;
using System;
using System.Linq;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using CulturNaryBDDProject.Shared;
using System.Diagnostics;
using System.Collections.ObjectModel;
using Microsoft.Extensions.Configuration;
using CulturNaryBDDProject.PageObjects;
using CulturNaryBDDProject.Drivers;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

namespace CulturNaryBDDProject.StepDefinitions
{
    [Binding]
    public class Boug_244_SharingStepDefinitions
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly HomePageObject _homePage;
        private readonly LoginPageObject _loginPage;
        private readonly RecipeSearchEnginePageObject _recipeSearchEnginePage;

        private readonly IWebDriver _webDriver;

        public Boug_244_SharingStepDefinitions(ScenarioContext context, BrowserDriver browserDriver )
        {
            _webDriver = browserDriver.Current;
            _loginPage = new LoginPageObject(_webDriver);
            _homePage = new HomePageObject(_webDriver);
            _recipeSearchEnginePage = new RecipeSearchEnginePageObject(_webDriver);
            _scenarioContext = context;
        }

        [Given("the following users exist in BougTwoFourtyFour")]
        public void GivenTheFollowingUsersExistInBougTwoFourtyFour(DataTable dataTable)
        {
            dataTable.Rows.ToList().ForEach(row =>
            {
                Console.WriteLine(row["UserName"]);
                Console.WriteLine(row["Password"]);
            });
        }

        [Given("UserA has shared a favorite recipe with UserB")]
        public void GivenUserAHasSharedAFavoriteRecipeWithUserB()
        {
            _recipeSearchEnginePage.GoTo();
            _recipeSearchEnginePage.SearchForRecipe("Pork Chops");
        }

        [When("UserB views their shared recipes list")]
        public void WhenUserBViewsTheirSharedRecipesList()
        {
            _scenarioContext.Pending();
        }


        [Then("the shared recipe should be listed there")]
        public void ThenTheSharedRecipeShouldBeListedThere()
        {
            _scenarioContext.Pending();
        }

        [Given("UserA is logged into their account")]
        public void GivenUserAIsLoggedIntoTheirAccount()
        {
            _scenarioContext.Pending();
        }

        [Given("UserA has marked a recipe as a favorite")]
        public void GivenUserAHasMarkedARecipeAsAFavorite()
        {
            _scenarioContext.Pending();
        }

        [When("UserA selects the option to share the favorite recipe with UserB")]
        public void WhenUserASelectsTheOptionToShareTheFavoriteRecipeWithUserB()
        {
            _scenarioContext.Pending();
        }

        [Then("UserB should receive it")]
        public void ThenUserBShouldReceiveIt()
        {
            _scenarioContext.Pending();
        }
    }
}