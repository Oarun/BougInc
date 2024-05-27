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
        public IWebElement BrightnessSelector => _webDriver.FindElement(By.Id("isDark"));
        public IWebElement ColorSelector => _webDriver.FindElement(By.Id("primaryColor"));
        public IWebElement CurrentBrightness => _webDriver.FindElement(By.Id("currentBrightness"));
        public IWebElement CurrentColor => _webDriver.FindElement(By.Id("currentColor"));


        public void Logout()
        {
                var logoutButtons = _webDriver.FindElements(By.XPath("//button[text()='Logout']"));

                if (logoutButtons.Count > 0)
                {
                    // If the logout button is present, click it to log out
                    logoutButtons[0].Click();
                }
        }

        public void SetLightMode()
        {
            var selectElement = new SelectElement(BrightnessSelector);
            selectElement.SelectByText("Light Mode");
        }

        public void SetDarkMode()
        {
            var selectElement = new SelectElement(BrightnessSelector);
            selectElement.SelectByText("Dark Mode");
        }

        public void SetColor(string color)
        {
            var selectElement = new SelectElement(ColorSelector);
            selectElement.SelectByText(color);
        }

    }
}
