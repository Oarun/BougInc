using Reqnroll;
using System;
using System.Linq;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using CulturNaryBDDProject.Shared;
using System.Diagnostics;
using System.Collections.ObjectModel;
using Microsoft.Extensions.Configuration;
using CulturNaryBDDProject.PageObjects;
using CulturNaryBDDProject.Drivers;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

namespace CulturNaryBDDProject.StepDefinitions;

public class Boug157_CollectionsPageStepDefinitions
{
    private readonly CollectionsPageObject _collectionsPageObject;
    private readonly ScenarioContext _scenarioContext;

    public Boug157_CollectionsPageStepDefinitions(ScenarioContext context, BrowserDriver browserDriver) 
    {
        _scenarioContext = context;
        _collectionsPageObject = new CollectionsPageObject(browserDriver.Current);
    } 
 
    [Given("I am a logged in user")]
    public void GivenIAmALoggedInUser()
    {
        // _pageObject.Pending();
    }
    
    [When("I go to the user collection page")]
    public void WhenIGoToTheUserCollectionPage()
    {
        // _pageObject.Pending();
    }
    
    [Then("I should see a form to create a collection")]
    public void ThenIShouldSeeAFormToCreateACollection()
    {
        // _pageObject.Pending();
    }
}