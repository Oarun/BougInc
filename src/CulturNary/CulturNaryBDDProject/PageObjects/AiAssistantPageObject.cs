using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using CulturNaryBDDProject.Shared;
using System.Collections.ObjectModel;
using System.Linq;

namespace CulturNaryBDDProject.PageObjects
{
    public class AiAssistantPageObject : PageObject
    {
        public AiAssistantPageObject(IWebDriver webDriver) : base(webDriver)
        {
            _pageName = "AiAssistant";
        }

        public IWebElement urlInput => _webDriver.FindElement(By.Id("recipeURL"));
        public IWebElement submitURLButton => _webDriver.FindElement(By.Id("recipeURLSubmit"));
        public IWebElement aiOutput => _webDriver.FindElement(By.Id("modelResponse"));


        public void EnterURL(string url)
        {
            urlInput.Clear();
            urlInput.SendKeys(url);
        }

        public void SubmitURL()
        {
            submitURLButton.Click();
        }

    }
}