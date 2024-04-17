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
    public class Boug127_AppropriateAccessStepDefinitions
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly HomePageObject _homePage;
        private readonly LoginPageObject _loginPage;
        private readonly RecipeSearchEnginePageObject _recipeSearchEnginePage;

        private readonly IWebDriver _webDriver;

        public Boug127_AppropriateAccessStepDefinitions(ScenarioContext context, BrowserDriver browserDriver )
        {
            _webDriver = browserDriver.Current;
            _loginPage = new LoginPageObject(_webDriver);
            _homePage = new HomePageObject(_webDriver);
            _recipeSearchEnginePage = new RecipeSearchEnginePageObject(_webDriver);
            _scenarioContext = context;
        }

        [Given("the following user exists")]
        public void GivenTheFollowingUserExists(DataTable dataTable)
        {
            dataTable.Rows.ToList().ForEach(row =>
            {
                Console.WriteLine(row["UserName"]);
                Console.WriteLine(row["Password"]);
            });
        }
        /*
        [Given("I login as {string} with password {string}")]
        public void GivenILoginAsWithPassword(string p0, string p1)
        {
            _loginPage.GoTo();
            _loginPage.EnterUsername(p0);
            _loginPage.EnterPassword(p1);
            _loginPage.Login();
        }
        */
        [Given("a user is signed in with UserName {string} and Password {string}")]
        public void GivenAUserIsSignedInWithUserNameAndPassword(string p0, string p1)
        {
            _loginPage.GoTo();
            _loginPage.EnterUsername(p0);
            _loginPage.EnterPassword(p1);
            _loginPage.Login();
        }
        [Given("a non-signed-in user is not logged into the system")]
        public void GivenANon_Signed_InUserIsNotLoggedIntoTheSystem()
        {
            _homePage.GoTo();
            _homePage.Logout();
        }
        [When("the non-signed-in user tries to access signed-in user pages")]
        public void WhenTheNon_Signed_InUserTriesToAccessSigned_InUserPages()
        {
            _recipeSearchEnginePage.GoTo();
        }

        [Then("the system should prevent access and redirect the user to the login page")]
        public void ThenTheSystemShouldPreventAccessAndRedirectTheUserToTheLoginPage()
        {
            string currentUrl = _webDriver.Url;
            Assert.That(currentUrl.Contains("/Identity/Account/Login"), Is.True, "User was not redirected to the login page.");
        }
        [When("the non-signed-in user tries to access signed-in user pages directly via URL")]
        public void WhenTheNon_Signed_InUserTriesToAccessSigned_InUserPagesDirectlyViaURL()
        {
            _webDriver.Navigate().GoToUrl(Common.UrlFor("RecipeSearchEngine"));
        }
        [When("the non-signed-in user tries to access non-signed-in user pages")]
        public void WhenTheNon_Signed_InUserTriesToAccessNon_Signed_InUserPages()
        {
            _loginPage.GoTo();
        }
        [Then("the system should allow access without any redirection")]
        public void ThenTheSystemShouldAllowAccessWithoutAnyRedirection()
        {
            string currentUrl = _webDriver.Url;
            Assert.That(currentUrl.Contains("/Identity/Account/Login"), Is.True, "User reached login page.");
        }

        [When("the signed-in user tries to access non-signed-in user pages")]
        public void WhenTheSigned_InUserTriesToAccessNon_Signed_InUserPages()
        {
            _loginPage.GoTo();
        }
        [Then("the system should prevent access and redirect the user to the appropriate page")]
        public void ThenTheSystemShouldPreventAccessAndRedirectTheUserToTheAppropriatePage()
        {
            string currentUrl = _webDriver.Url;
            Assert.That(currentUrl.Contains("/?page=%2FIndex"), Is.True, "User reached login page.");
            _homePage.Logout();
        }

        [When("the signed-in user tries to access the registration or login page")]
        public void WhenTheSigned_InUserTriesToAccessTheRegistrationOrLoginPage()
        {
            _loginPage.GoTo();
        }
        [Then("the system should prevent access and redirect the user to the homepage or dashboard")]
        public void ThenTheSystemShouldPreventAccessAndRedirectTheUserToTheHomepageOrDashboard()
        {
            string currentUrl = _webDriver.Url;
            Assert.That(currentUrl.Contains("/?page=%2FIndex"), Is.True, "User reached login page.");
            _homePage.Logout();
        }
        [When("the signed-in user tries to access signed-in user pages")]
        public void WhenTheSigned_InUserTriesToAccessSigned_InUserPages()
        {
            _recipeSearchEnginePage.GoTo();
        }
        [Then("the system should allow access without any redirection to the signed-in user pages")]
        public void ThenTheSystemShouldAllowAccessWithoutAnyRedirectionToTheSigned_InUserPages()
        {
            string currentUrl = _webDriver.Url;
            Assert.That(currentUrl.Contains("/?page=%2FIndex"), Is.False, "User reached login page.");
            _homePage.Logout();
        }
    }
}