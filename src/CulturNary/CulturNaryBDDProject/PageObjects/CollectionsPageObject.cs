using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using CulturNaryBDDProject.Shared;
using System.Collections.ObjectModel;
using System.Linq;

namespace CulturNaryBDDProject.PageObjects
{
    public class CollectionsPageObject : PageObject
    {
        public CollectionsPageObject(IWebDriver webDriver) : base(webDriver)
        {
            // using a named page (in Common.cs)
            _pageName = "Home";
        }

        public IWebElement RegisterButton => _webDriver.FindElement(By.Id("register-link"));
        public IWebElement NavBarHelloLink => _webDriver.FindElement(By.CssSelector("a[title=\"Manage\"]"));  //or _webDriver.FindElement(By.CssSelector("a[href=\"/Identity/Account/Manage\"]"));

        public IWebElement NewsletterEmailInput => _webDriver.FindElement(By.Id("newsletter-subscribe-email"));
        public IWebElement NewsletterSubscribeButton => _webDriver.FindElement(By.Id("newsletter-subscribe-submit-button"));
        
        private ReadOnlyCollection<IWebElement> Questions => _webDriver.FindElements(By.CssSelector("li.question a"));

        public string NavbarWelcomeText()
        {
            return NavBarHelloLink.Text;
        }

        public void Logout()
        {
            IWebElement navbarLogoutButton = _webDriver.FindElement(By.Id("logout-button"));
            navbarLogoutButton.Click();
        }

        public bool HasQuestionText(string text)
        {
            // Look through all the questions and see if text is present
            return Questions.Any(q => q.Text.Contains(text));
        }

        public void SubmitNewsletterEmail()
        {
            NewsletterSubscribeButton.Click();
        }
    }
}