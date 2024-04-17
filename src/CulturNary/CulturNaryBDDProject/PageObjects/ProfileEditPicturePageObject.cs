using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CulturNaryBDDProject.PageObjects
{
    public class ProfileEditPicturePageObject : PageObject
    {
        public ProfileEditPicturePageObject(IWebDriver webDriver) : base(webDriver)
        {
            _pageName = "ProfileEditPicture";

            if(_webDriver == null)
            {
                throw new ArgumentNullException("webDriver");
            }
        }

        public IWebElement PictureInput => _webDriver.FindElement(By.Id("Input_profileImage"));
        public IWebElement UploadButton => _webDriver.FindElement(By.Id("update-profile-button"));
        
        public IWebElement ProfileButton => _webDriver.FindElement(By.Id("back-button"));

        public void UploadPicture(string filePath)
        {
            PictureInput.SendKeys(filePath);
        }

        public void Upload()
        {
            UploadButton.Click();
        }

        public void GoToProfile()
        {
            ProfileButton.Click();
        }
    }
}