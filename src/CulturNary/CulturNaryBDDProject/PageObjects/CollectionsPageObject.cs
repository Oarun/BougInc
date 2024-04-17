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
            _pageName = "Collections";
        }

        public IWebElement CreateCollectionFormContainer => _webDriver.FindElement(By.Id("createCollectionFormContainer"));
        public IWebElement CollectionContainer => _webDriver.FindElement(By.Id("CollectionContainer"));
        public IWebElement EditIcon => _webDriver.FindElement(By.Id("editIcon"));
        public IWebElement DeleteIcon => _webDriver.FindElement(By.Id("deleteIcon"));

    }
}