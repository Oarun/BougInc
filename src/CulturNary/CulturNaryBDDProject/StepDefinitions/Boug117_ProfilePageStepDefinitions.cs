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
using OpenQA.Selenium.DevTools.V121.Profiler;
using System.Threading;
using SpecFlow.Actions.Selenium;

namespace CulturNaryBDDProject.StepDefinitions
{
    public sealed class Boug117_ProfilePageStepDefinitions
    {
        // For additional details on Reqnroll step definitions see https://go.reqnroll.net/doc-stepdef
        [Binding]
        public class ProfilePageStepDefinitions
        {
            private readonly Drivers.BrowserDriver _browserDriver;
            private readonly ScenarioContext _scenarioContext;
            private readonly HomePageObject _homePage;
            private readonly LoginPageObject _loginPage;
            private readonly ProfilePageObject _profilePage;
            private readonly ProfileEditNamePageObject _ProfileEditNamePage;
            private readonly ProfileEditPicturePageObject _ProfileEditPicturePage;
            private readonly ProfileEditBioPageObject _ProfileEditBioPage;

            private readonly IWebDriver _webDriver;

            public ProfilePageStepDefinitions(ScenarioContext context, Drivers.BrowserDriver browserDriver)
            {
                _browserDriver = browserDriver;
                _webDriver = browserDriver.Current;
                _homePage = new HomePageObject(_webDriver);
                _loginPage = new LoginPageObject(_webDriver);
                _profilePage = new ProfilePageObject(_webDriver);
                _ProfileEditNamePage = new ProfileEditNamePageObject(_webDriver);
                _ProfileEditPicturePage = new ProfileEditPicturePageObject(_webDriver);
                _ProfileEditBioPage = new ProfileEditBioPageObject(_webDriver);
                _scenarioContext = context;
            }

            [Given("the following user exists in BougOneSeventeen")]
            public void GivenTheFollowingUserExistsInBougOneSeventeen(DataTable dataTable)
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

            [When("the user navigates to the Profile page")]
            public void WhenTheUserNavigatesToTheProfilePage()
            {
                _profilePage.GoTo();
            }

            [Given("the user navigates to their Profile page")]
            public void WhenTheUserNavigatesToTheirProfilePage()
            {
                _profilePage.GoTo();
            }

            [Then("the user should see their display name, biography, and profile picture")]
            public void ThenTheUserShouldSeeTheirDisplayNameBiographyAndProfilePicture()
            {
                Assert.That(_profilePage.DisplayName.Displayed);
                Assert.That(_profilePage.Biography.Displayed);
                Assert.That(_profilePage.ProfilePicture.Displayed);

                Assert.That(_profilePage.DisplayName.Text, Is.EqualTo("Test User"));
                Assert.That(_profilePage.Biography.Text, Is.EqualTo("Hello!"));
                Assert.That(_profilePage.ProfilePicture.GetAttribute("src"), Is.EqualTo(@"https://culturnaryimage.blob.core.windows.net/images/scrungle.jpg"));
            }

            [Then("they should see an button to edit each of these")]
            public void ThenTheyShouldSeeAnButtonToEditEachOfThese()
            {
                Assert.That(_profilePage.EditDisplayNameButton.Displayed);
                Assert.That(_profilePage.EditBiographyButton.Displayed);
                Assert.That(_profilePage.EditProfilePictureButton.Displayed);
                _homePage.Logout();
            }
            
            [Given("clicks the button to edit their display name")]
            public void WhenClicksTheButtonToEditTheirDisplayName()
            {
                _profilePage.EditDisplayName();
            }

            [When("the user enters a new display name on the edit page")]
            public void WhenTheUserEntersANewDisplayNameOnTheEditPage()
            {
                _ProfileEditNamePage.EnterName("New Display Name");
            }

            [When("clicks the button to save their new display name")]
            public void WhenClicksTheButtonToSaveTheirNewDisplayName()
            {
                _ProfileEditNamePage.Save();
            }

            [Then("the user should see the updated display name on their profile page")]
            public void ThenTheUserShouldSeeTheUpdatedDisplayNameOnTheirProfilePage()
            {
                _ProfileEditNamePage.GoToProfile();
                Assert.That(_profilePage.DisplayName.Text, Is.EqualTo("New Display Name"));
                _homePage.Logout();
            }

            [Given("clicks the button to edit their biography")]
            public void WhenClicksTheButtonToEditTheirBiography()
            {
                _profilePage.EditBiography();
            }

            [When("the user enters a new biography on the edit page")]
            public void WhenTheUserEntersANewBiographyOnTheEditPage()
            {
                _ProfileEditBioPage.EnterBio("New Biography");
            }

            [When("clicks the button to save their new biography")]
            public void WhenClicksTheButtonToSaveTheirNewBiography()
            {
                _ProfileEditBioPage.Save();
            }

            [Then("the user should see the updated biopgraphy on their profile page")]
            public void ThenTheUserShouldSeeTheUpdatedBiopgraphyOnTheirProfilePage()
            {
                _ProfileEditBioPage.GoToProfile();
                Assert.That(_profilePage.Biography.Text, Is.EqualTo("New Biography"));
                _homePage.Logout();
            }

            [Given("clicks the button to edit their profile picture")]
            public void WhenClicksTheButtonToEditTheirProfilePicture()
            {
                _profilePage.EditProfilePicture();
            }

            [When("the user uploads a new profile picture")]
            public void WhenTheUserUploadsANewProfilePicture()
            {
                _ProfileEditPicturePage.UploadPicture(@"C:\Users\nyanm\\Pictures\harp.jpg");
            }

            [When("clicks the button to upload their new picture")]
            public void WhenClicksTheButtonToUploadTheirNewProfilePicture()
            {
                _browserDriver.ScrollToElement(_ProfileEditPicturePage.UploadButton);
                _ProfileEditPicturePage.Upload();
            }

            [Then("the user should see the new profile picture on their profile page")]
            public void ThenTheUserShouldSeeTheNewProfilePictureOnTheirProfilePage()
            {
                _browserDriver.ScrollToElement(_ProfileEditPicturePage.ProfileButton);
                _ProfileEditPicturePage.GoToProfile();
                Assert.That(_profilePage.ProfilePicture.GetAttribute("src"), Is.EqualTo(@"https://culturnaryimage.blob.core.windows.net/images/harp.jpg"));
                _homePage.Logout();
            }
        }
    }
}