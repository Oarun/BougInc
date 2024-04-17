using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CulturNaryBDDProject.PageObjects
{
    public class RecipeSearchEnginePageObject : PageObject
    {
        public RecipeSearchEnginePageObject(IWebDriver webDriver) : base(webDriver)
        {
            _pageName = "RecipeSearchEngine";

            if(_webDriver == null)
            {
                throw new ArgumentNullException("webDriver");
            }
        }
    }
}