using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using CulturNaryBDDProject.Shared;
using System;
using System.Diagnostics;
using System.Collections.ObjectModel;
using Microsoft.Extensions.Configuration;
using CulturNaryBDDProject.PageObjects;
using CulturNaryBDDProject.Drivers;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using SpecFlow.Actions.Selenium;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using System.Linq;


namespace CulturNaryBDDProject.StepDefinitions
{
    // Wrapper for the data we get for each user
    public class TestUser
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
    }

    [Binding]
    public sealed class UserLoginStepDefinitions
    {
        private readonly LoginPageObject _loginPage;
        private readonly Drivers.BrowserDriver _webDriver;
        private readonly ScenarioContext _scenarioContext;
        private IConfigurationRoot Configuration { get; set; }

        public UserLoginStepDefinitions(ScenarioContext context, Drivers.BrowserDriver browserDriver)
        {
            _scenarioContext = context;
            _webDriver = browserDriver;
            _loginPage = new LoginPageObject(_webDriver.Current);

            // Get the configuration
            // we need to keep the admin password secret
            // IConfigurationBuilder builder = new ConfigurationBuilder().AddUserSecrets<UserLoginStepDefinitions>();
            // Configuration = builder.Build();
        }

        [Given(@"the following user exists")]
        public void GivenTheFollowingUserExists(Table table)
        {
            // Nothing to do for this step other than to save the background data
            // that defines the users
            IEnumerable<TestUser> users = table.CreateSet<TestUser>();

            foreach (TestUser u in users)
            {
                u.Password = "password123";
            }

            _scenarioContext["Users"] = users;
        }

        [Given(@"I login as '([^']*)' with password '([^']*)'")]
        public void GivenILoginAsTestuserFakemailComWithPasswordPassword123(string username, string password)
        {
            // Go to the login page
            _loginPage.GoTo();
            //Thread.Sleep(3000);
            // Now (attempt to) log them in.  Assumes previous steps defined the user
            TestUser u = new TestUser();
            u.UserName = username;
            u.Password = password;
            _loginPage.EnterUsername(u.UserName);
            _loginPage.EnterPassword(u.Password);
            _loginPage.Login();
        }

        [Given(@"I am a user with first name '([^']*)'"), When(@"I am a user with first name '([^']*)'")]
        public void GivenIAmAUserWithFirstName(string firstName)
        {
            // Find this user, first look in users, then in non-users
            IEnumerable<TestUser> users = (IEnumerable<TestUser>)_scenarioContext["Users"];
            TestUser u = users.Where(u => u.FirstName == firstName).FirstOrDefault();
            if (u == null)
            {
                // must have been selecting from non-users
                IEnumerable<TestUser> nonUsers = (IEnumerable<TestUser>)_scenarioContext["NonUsers"];
                u = nonUsers.Where(u => u.FirstName == firstName).FirstOrDefault();
            }
            _scenarioContext["CurrentUser"] = u;
        }

        [Given(@"I login"),When(@"I login")]
        public void WhenILogin()
        {
            // Go to the login page
            _loginPage.GoTo();
            //Thread.Sleep(3000);
            // Now (attempt to) log them in.  Assumes previous steps defined the user
            TestUser u = (TestUser)_scenarioContext["CurrentUser"];
            _loginPage.EnterUsername(u.UserName);
            _loginPage.EnterPassword(u.Password);
            _loginPage.Login();
        }

        [Given(@"I am on the ""([^""]*)"" page")]
        [When(@"I am on the ""([^""]*)"" page")]
        public void GivenIAmOnThePage(string page)
        {
            _webDriver.Current.Navigate().GoToUrl(Common.UrlFor(page));
        }
    }
}