using System;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium;
using Reqnroll;
using CulturNaryBDDProject.PageObjects;

namespace CulturNaryBDDProject.StepDefinitions
{

    [Binding]
    public class Boug_247_ThemesStepDefinitions
    {
        private IWebDriver _driver;
        private HomePageObject _homePage;

        [BeforeScenario]
        public void BeforeScenario()
        {
            _driver = new FirefoxDriver();
            _homePage = new HomePageObject(_driver);
        }

        [AfterScenario]
        public void AfterScenario()
        {
            _driver.Quit();
        }

        [Given("I am on the home page")]
        public void GivenIAmOnTheHomePage()
        {
            _driver.Navigate().GoToUrl("/");
        }

        [When("I change the brightness of the site to Light Mode")]
        public void WhenIChangeTheBrightnessOfTheSiteToLightMode()
        {
            _homePage.SetLightMode();
        }

        [Then("the site should change theme to Light Mode")]
        public void ThenTheSiteShouldChangeThemeToLightMode()
        {
            _homePage.CurrentBrightness.Text.Contains("light");
        }

        [When("I change the color of the site to Red")]
        public void WhenIChangeTheColorOfTheSiteToRed()
        {
            _homePage.SetColor("Red");
        }

        [Then("the site should change theme to Red")]
        public void ThenTheSiteShouldChangeThemeToRed()
        {
            _homePage.CurrentColor.Text.Contains("red");
        }

        [When("I change the color of the site to Blue")]
        public void WhenIChangeTheColorOfTheSiteToBlue()
        {
            _homePage.SetColor("Blue");
        }

        [When("I change the brightness of the site to Dark Mode")]
        public void WhenIChangeTheBrightnessOfTheSiteToDarkMode()
        {
            _homePage.SetDarkMode();
        }

        [Then("the site should change theme to Dark Mode and Blue")]
        public void ThenTheSiteShouldChangeThemeToDarkModeAndBlue()
        {
            _homePage.CurrentBrightness.Text.Contains("dark");
            _homePage.CurrentColor.Text.Contains("blue");
        }
    }
}