using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CulturNaryBDDProject.PageObjects
{
    public class ProfileEditNamePageObject : PageObject
    {
        public ProfileEditNamePageObject(IWebDriver webDriver) : base(webDriver)
        {
            _pageName = "ProfileEditName";

            if(_webDriver == null)
            {
                throw new ArgumentNullException("webDriver");
            }
        }

        public IWebElement NameInput => _webDriver.FindElement(By.Id("Input_displayName"));
        public IWebElement SaveButton => _webDriver.FindElement(By.Id("update-profile-button"));
        public IWebElement ProfileButton => _webDriver.FindElement(By.Id("back-button"));

        public void EnterName(string name)
        {
            NameInput.Clear();
            NameInput.SendKeys(name);
        }

        public void Save()
        {
            SaveButton.Click();
        }

        public void GoToProfile()
        {
            ProfileButton.Click();
        }
    }
}