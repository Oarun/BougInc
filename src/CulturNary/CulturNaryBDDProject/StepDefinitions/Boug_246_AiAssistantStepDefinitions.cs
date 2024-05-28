using System;
using System.Linq;
using NUnit.Framework;
using OpenQA.Selenium;
using CulturNaryBDDProject.PageObjects;

namespace CulturNaryBDDProject.StepDefinitions
{

    [Binding]
    public class Boug_246_AiAssistantStepDefinitions
    {
        private IWebDriver _driver;
        private readonly ScenarioContext _scenarioContext;
        private readonly LoginPageObject _loginPage;
        private readonly AiAssistantPageObject _aiAssistantPage;

        public Boug_246_AiAssistantStepDefinitions(ScenarioContext context)
        {
            _scenarioContext = context;
            _loginPage = new LoginPageObject(_driver);
            _aiAssistantPage = new AiAssistantPageObject(_driver);
        }

                    [Given("the following user exists in BougTwoFourtySix")]
            public void GivenTheFollowingUserExistsInBougTwoFourtySix(DataTable dataTable)
            {
                dataTable.Rows.ToList().ForEach(row =>
                {
                    Console.WriteLine(row["UserName"]);
                    Console.WriteLine(row["Password"]);
                });
            }

            [Given("a user is logged in with the UserName {string} and Password {string}")]
            public void GivenAUserIsLoggedInWithUserNameAndPassword(string username, string password)
            {
                _loginPage.GoTo();
                _loginPage.EnterUsername(username);
                _loginPage.EnterPassword(password);
                _loginPage.Login();
            }

            [When("the user navigates to the AI Assistant page")]
            public void WhenTheUserNavigatesToAIAssistantPage()
            {
                _aiAssistantPage.GoTo();
            }

            [When("the user enters and submits a url to a recipe")]
            public void WhenTheUserEntersAndSubmitsAUrlToARecipe()
            {
                _aiAssistantPage.EnterURL("https://www.bettycrocker.com/recipes/easy-apple-pie-cookie-cups/2b2b7594-c7e1-4bb3-8e47-08a2e792797b");
                _aiAssistantPage.SubmitURL();

            }

            [Then("the user is shown a comparison of the recipe to their dietary restrictions")]
            public void ThenTheUserIsShownAComparisonOfTheRecipeToTheirDietaryRestrictions()
            {
                Assert.That(_aiAssistantPage.aiOutput.Text.Any, Is.True);
            }

    }

}