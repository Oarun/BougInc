using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using CulturNaryBDDProject.Shared;
using System.Collections.ObjectModel;
using System.Linq;

namespace CulturNaryBDDProject.PageObjects
{
    public class RestaurantPageObject : PageObject
    {
        public RestaurantPageObject(IWebDriver webDriver) : base(webDriver)
        {
            _pageName = "Restaurants";
        }

        public IWebElement CreateRestaurantFormContainer => _webDriver.FindElement(By.Id("createRestaurantFormContainer"));
        public IWebElement RestaurantContainer => _webDriver.FindElement(By.Id("createRestaurantFormContainer"));
        public IWebElement UserRestaurantsList => _webDriver.FindElement(By.Id("UserRestaurantsList"));
        public IWebElement ZipCodeInput => _webDriver.FindElement(By.Id("zipCodeInput"));
    }
}