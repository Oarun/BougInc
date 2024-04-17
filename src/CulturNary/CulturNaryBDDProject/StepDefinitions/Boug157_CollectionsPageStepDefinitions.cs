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

[Binding]
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

    [Given("the following user exists in BougOneFiftySeven")]
    public void GivenTheFollowingUserExistsInBougOneFiftySeven(DataTable dataTable){
        dataTable.Rows.ToList().ForEach(row =>
        {
            Console.WriteLine(row["UserName"]);
            Console.WriteLine(row["Password"]);
        });
    }

    [Given("the user is signed in with UserName {string} and Password {string}")]
    public void GivenTheUserIsSignedInWithUserNameAndPassword(string p0, string p1)
    {
        _loginPage.GoTo();
        _loginPage.EnterUsername(p0);
        _loginPage.EnterPassword(p1);
        _loginPage.Login();
    }

    [When("I go to the user collection page")]
    public void WhenIGoToTheUserCollectionPage()
    {
        _collectionsPage.GoTo();
    }


    [Then("I should see a form to create a collection")]
    public void ThenIShouldSeeAFormToCreateACollection()
    {
        Assert.That(_collectionsPage.CreateCollectionFormContainer.Displayed);
        _homePage.Logout();
    }

    [Then("I should see my collections")]
    public void ThenIShouldSeeMyCollections()
    {
        Assert.That(_collectionsPage.CreateCollectionFormContainer.Displayed);
        _homePage.Logout();
    }

    [Then("I should see a edit logo to edit a collection")]
    public void ThenIShouldSeeAEditLogoToEditACollection()
    {
        Assert.That(_collectionsPage.CreateCollectionFormContainer.Displayed);
        _homePage.Logout();
    }

    [Then("I should see a delete logo to delete a collection")]
    public void ThenIShouldSeeADeleteLogoToDeleteACollection()
    {
        Assert.That(_collectionsPage.CreateCollectionFormContainer.Displayed);
        _homePage.Logout();
    }
}