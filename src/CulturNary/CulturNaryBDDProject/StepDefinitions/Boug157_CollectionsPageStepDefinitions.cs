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
    private readonly ScenarioContext _scenarioContext;
    private readonly HomePageObject _homePage;
    private readonly LoginPageObject _loginPage;
    private readonly RecipeSearchEnginePageObject _recipeSearchEnginePage;
    private readonly CollectionsPageObject _collectionsPage;

    private readonly IWebDriver _webDriver;

    public Boug157_CollectionsPageStepDefinitions(ScenarioContext context, BrowserDriver browserDriver )
    {
        _webDriver = browserDriver.Current;
        _loginPage = new LoginPageObject(_webDriver);
        _homePage = new HomePageObject(_webDriver);
        _recipeSearchEnginePage = new RecipeSearchEnginePageObject(_webDriver);
        _collectionsPage = new CollectionsPageObject(_webDriver);
        _scenarioContext = context;
    }
 
    // [Given("I am a signed in user with UserName {string} and Password {string}")]
    // public void GivenIAmALoggedInUser(string p0, string p1)
    // {
    //     _loginPage.GoTo();
    //     _loginPage.EnterUsername(p0);
    //     _loginPage.EnterPassword(p1);
    //     _loginPage.Login();
    // }

    [Given("I am a signed in user with UserName {string} and Password {string}")]
    public void GivenIAmASignedInUserWithUserNameAndPassword(string p0, string p1)
    {
        _loginPage.GoTo();
        _loginPage.EnterUsername(p0);
        _loginPage.EnterPassword(p1);
        _loginPage.Login();
    }
    
    // [When("I go to the user collection page")]
    // public void WhenIGoToTheUserCollectionPage()
    // {
    //     _collectionsPage.GoTo();
    // }

    [When("I go to the user collection page")]
    public void WhenIGoToTheUserCollectionPage()
    {
        _collectionsPage.GoTo();
    }


    [Then("I should see a form to create a collection")]
    public void ThenIShouldSeeAFormToCreateACollection()
    {
        Assert.That(_collectionsPage.CreateCollectionFormContainer.Displayed);
    }

    // [Then("I should see a collection called {string}")]
    // public void ThenIShouldSeeACollectionCalled(string lunch)
    // {
    //     Assert.That(_collectionsPage.CreateCollectionFormContainer.Displayed);
    // }

    // [Then("I should see a delete logo to delete a collection")]
    // public void ThenIShouldSeeADeleteLogoToDeleteACollection()
    // {
    //     Assert.That(_collectionsPage.EditIcon.Displayed);
    // }

    // [Then("I should see a edit logo to edit a collection")]
    // public void ThenIShouldSeeAEditLogoToEditACollection()
    // {
    //     Assert.That(_collectionsPage.DeleteIcon.Displayed);
    // }
    
    [Then("I should see a collection called {string}")]
    public void ThenIShouldSeeACollectionCalled(string lunch)
    {
        Assert.That(_collectionsPage.CreateCollectionFormContainer.Displayed);
    }

    [Then("I should see a edit logo to edit a collection")]
    public void ThenIShouldSeeAEditLogoToEditACollection()
    {
        Assert.That(_collectionsPage.EditIcon.Displayed);
    }

    [Then("I should see a delete logo to delete a collection")]
    public void ThenIShouldSeeADeleteLogoToDeleteACollection()
    {
        Assert.That(_collectionsPage.DeleteIcon.Displayed);
    }
}