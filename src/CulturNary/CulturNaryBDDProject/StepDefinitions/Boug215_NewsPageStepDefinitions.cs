using System;
using System.Linq;
using Reqnroll;
using CulturNaryBDDProject.PageObjects;
using CulturNaryBDDProject.Shared;
using OpenQA.Selenium;
using CulturNaryBDDProject.Drivers;
using NUnit.Framework;
using OpenQA.Selenium.Support.UI;

namespace CulturNaryBDDProject.StepDefinitions
{
    [Binding]
    public class Boug215_NewsPageStepDefinitions
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly HomePageObject _homePage;
        private readonly LoginPageObject _loginPage;
        private readonly IWebDriver _webDriver;

        public Boug215_NewsPageStepDefinitions(ScenarioContext context, BrowserDriver browserDriver)
        {
            _scenarioContext = context;
            _webDriver = browserDriver.Current;
            _homePage = new HomePageObject(_webDriver);
            _loginPage = new LoginPageObject(_webDriver);
        }

        [Given("the following user exists in BougTwoFifteen")]
        public void GivenTheFollowingUserExistsInBougTwoFifteen(DataTable dataTable)
        {
            dataTable.Rows.ToList().ForEach(row =>
            {
                Console.WriteLine(row["UserName"]);
                Console.WriteLine(row["Password"]);
            });
        }

        [Given("the user is logged in with the UserName {string} and Password {string}")]
        public void GivenTheUserIsLoggedInWithTheUserNameAndPassword(string p0, string p1)
        {
            _loginPage.GoTo();
            _loginPage.EnterUsername(p0);
            _loginPage.EnterPassword(p1);
            _loginPage.Login();
        }

        [When("the user navigates to the News page")]
        public void WhenTheUserNavigatesToTheNewsPage()
        {
            _webDriver.Navigate().GoToUrl("https://localhost:7253/Home/News");
        }

        [Then("the user should see a page with the title {string}")]
        public void ThenTheUserShouldSeeAPageWithTheTitle(string expectedTitle)
        {
            var actualTitle = _webDriver.Title;
            Assert.That(expectedTitle == actualTitle);
        }


        [Then("they should see an empty page with a form of select options")]
        public void ThenTheyShouldSeeAnEmptyPageWithAFormOfSelectOptions()
        {
            _webDriver.Navigate().GoToUrl("https://localhost:7253/Home/News");
            bool formExists = _webDriver.FindElement(By.Id("categoryForm")).Displayed;
            Assert.That(formExists, Is.True, "The form should be displayed.");
        }

        [Then("they should see a button to submit the form")]
        public void ThenTheyShouldSeeAButtonToSubmitTheForm()
        {
            var submitButton = _webDriver.FindElement(By.CssSelector("button[type='submit']")).Displayed;
            Assert.That(submitButton, Is.True, "Submit button should be visible.");
        }

        [Given("the user is on the news page")]
        public void GivenTheUserIsOnTheNewsPage()
        {
            _webDriver.Navigate().GoToUrl("https://localhost:7253/Home/News");
        }

        [When("the user selects a category from the form")]
        public void WhenTheUserSelectsACategoryFromTheForm()
        {
            var categorySelectElement = _webDriver.FindElement(By.Id("categorySelect"));
            var selectElement = new SelectElement(categorySelectElement);
            selectElement.SelectByText("Health");
        }


        [Then("the user should see a list of articles related to the category")]
        public void ThenTheUserShouldSeeAListOfArticlesRelatedToTheCategory()
        {
            var articles = _webDriver.FindElements(By.CssSelector(".article-class"));
            Assert.That(articles.Any(), Is.True, "Articles related to the selected category should be displayed.");
        }


        [Then("they should see a button to view the full article")]
        public void ThenTheyShouldSeeAButtonToViewTheFullArticle()
        {
            var readMoreLinkVisible = _webDriver.FindElement(By.CssSelector(".read-more-link-class")).Displayed;
            Assert.That(readMoreLinkVisible, Is.True, "Read more link should be visible.");
        }

        [Given("the user has made a category search on the news page")]
        public void GivenTheUserHasMadeACategorySearchOnTheNewsPage()
        {
            _webDriver.Navigate().GoToUrl("https://localhost:7253/Home/News");
            var categorySelectElement = _webDriver.FindElement(By.Id("categorySelect"));
            var selectElement = new SelectElement(categorySelectElement);
            selectElement.SelectByText("Health");
            _webDriver.FindElement(By.Id("search-button-id")).Click();
        }


        [When("the user selects the read more link on an article")]
        public void WhenTheUserSelectsTheReadMoreLinkOnAnArticle()
        {
            _webDriver.FindElement(By.CssSelector(".first-read-more-link-class")).Click();
        }

        [Then("the user should be redirected to the source of the article in a new tab")]
        public void ThenTheUserShouldBeRedirectedToTheSourceOfTheArticleInANewTab()
        {
            var allTabs = _webDriver.WindowHandles;
            _webDriver.SwitchTo().Window(allTabs[1]);
            Assert.That(_webDriver.Url.Contains("source_of_article_domain"));
        }
    }
}
