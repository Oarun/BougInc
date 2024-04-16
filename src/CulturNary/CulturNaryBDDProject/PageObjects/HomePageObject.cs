using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using CulturNaryBDDProject.Shared;
using System.Collections.ObjectModel;

namespace CulturNaryBDDProject.PageObjects
{
    public class HomePageObject : PageObject
    {
        public HomePageObject(IWebDriver webDriver) : base(webDriver)
        {
            // using a named page (in Common.cs)
            _pageName = "Home";
        }

        public IWebElement RegisterButton => _webDriver.FindElement(By.Id("register-link"));

        public void Logout()
        {
                var logoutButtons = _webDriver.FindElements(By.XPath("//button[text()='Logout']"));

                if (logoutButtons.Count > 0)
                {
                    // If the logout button is present, click it to log out
                    logoutButtons[0].Click();
                }
        }
    }
}
