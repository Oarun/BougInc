using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using CulturNaryBDDProject.Shared;
using System.Collections.ObjectModel;
using System;

namespace CulturNaryBDDProject.PageObjects
{
    public class ProfilePageObject : PageObject
    {

        public ProfilePageObject(IWebDriver webDriver) : base(webDriver)
        {
            _pageName = "Profile";

            if(_webDriver == null)
            {
                throw new ArgumentNullException("webDriver");
            }
        }

        public IWebElement DisplayName => _webDriver.FindElement(By.Id("displayName"));
        public IWebElement ProfilePicture => _webDriver.FindElement(By.Id("profilePicture"));
        public IWebElement Biography => _webDriver.FindElement(By.Id("biography"));

        public IWebElement EditDisplayNameButton => _webDriver.FindElement(By.Id("edit-displayname"));
        public IWebElement EditProfilePictureButton => _webDriver.FindElement(By.Id("edit-profilepic"));
        public IWebElement EditBiographyButton => _webDriver.FindElement(By.Id("edit-biography"));

        public void EditDisplayName()
        {
            WebDriverWait wait = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(10));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.Id("edit-displayname")));

            EditDisplayNameButton.Click();
        }

        public void EditProfilePicture()
        {
            WebDriverWait wait = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(10));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.Id("edit-profilepic")));

            EditProfilePictureButton.Click();
        }

        public void EditBiography()
        {
            WebDriverWait wait = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(10));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.Id("edit-biography")));

            EditBiographyButton.Click();
        }

    }

}