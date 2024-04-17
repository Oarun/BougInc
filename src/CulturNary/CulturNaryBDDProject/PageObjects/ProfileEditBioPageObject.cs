using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CulturNaryBDDProject.PageObjects
{
    public class ProfileEditBioPageObject : PageObject
    {
        public ProfileEditBioPageObject(IWebDriver webDriver) : base(webDriver)
        {
            _pageName = "ProfileEditBio";

            if(_webDriver == null)
            {
                throw new ArgumentNullException("webDriver");
            }
        }

        public IWebElement BioInput => _webDriver.FindElement(By.Id("Input_biography"));
        public IWebElement SaveButton => _webDriver.FindElement(By.Id("update-profile-button"));
        
        public IWebElement ProfileButton => _webDriver.FindElement(By.Id("back-button"));

        public void EnterBio(string bio)
        {
            BioInput.Clear();
            BioInput.SendKeys(bio);
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