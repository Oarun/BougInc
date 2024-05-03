using System;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium;
using Reqnroll;
using CulturNaryBDDProject.PageObjects;

namespace CulturNaryBDDProject.StepDefinitions
{
    [Binding]
    public class Boug_186_MealPlannerStepDefinitions
    {
        private IWebDriver _driver;
        //private MealPlannerPageObject _mealPlanPage;

        [BeforeScenario]
        public void BeforeScenario()
        {
            _driver = new FirefoxDriver();
            //_mealPlanPage = new MealPlannerPageObject(_driver);
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

        [When("I look at the navigation bar")]
        public void WhenILookAtTheNavigationBar()
        {
            _driver.FindElement(By.Id("navbar"));
        }

        [Then("I can see a link to the meal planner")]
        public void ThenICanSeeALinkToTheMealPlanner()
        {
            _driver.FindElement(By.Id("meal-planner-link"));
        }

        [When("I click on the meal planner link")]
        public void WhenIClickOnTheMealPlannerLink()
        {
            _driver.FindElement(By.Id("meal-planner-link")).Click();
        }

        [Then("I am taken to the meal planner page")]
        public void ThenIAmTakenToTheMealPlannerPage()
        {
            _driver.Url.Contains("/MealPlanner");
        }

        [Given("I am on the meal planner page")]
        public void GivenIAmOnTheMealPlannerPage()
        {
            _driver.Navigate().GoToUrl("/MealPlanner");
        }

        [Then("I can see health and dietary preferences and settings")]
        public void ThenICanSeeHealthAndDietaryPreferencesAndSettings()
        {
            _driver.FindElement(By.Id(".allergies"));
            _driver.FindElement(By.Id(".diet"));
            _driver.FindElement(By.Id(".calories"));
            _driver.FindElement(By.Id(".macronutients"));
            _driver.FindElement(By.Id(".micronutrients"));
        }

        [Then("I can select health and dietary preferences and settings")]
        public void ThenICanSelectHealthAndDietaryPreferencesAndSettings()
        {
            _driver.FindElement(By.Id("allergies")).Click();
        }

        [Then("I can enter minimum and maximum calories")]
        public void ThenICanEnterMinimumAndMaximumCalories()
        {
            _driver.FindElement(By.Id("min-calories")).SendKeys("1000");
        }
    }
}
