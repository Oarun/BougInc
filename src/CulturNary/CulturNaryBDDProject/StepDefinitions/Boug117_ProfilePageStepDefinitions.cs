// using CulturNaryBDDProject.Drivers;
// using CulturNaryBDDProject.PageObjects;
// using CulturNaryBDDProject.Shared;
// using Microsoft.Extensions.Configuration;
// using NUnit.Framework;
// using Reqnroll;

// namespace CulturNaryBDDProject.StepDefinitions
// {
//     public sealed class Boug117_ProfilePageStepDefinitions
//     {
//         // For additional details on Reqnroll step definitions see https://go.reqnroll.net/doc-stepdef
//         [Binding]
//         public class ProfilePageSteps
//         {
//             private readonly ScenarioContext _scenarioContext;
//             private readonly LoginPageObject _loginPage;
//             private readonly ProfilePageObject _profilePage;

//             private IConfigurationRoot Configuration { get; }

//             public ProfilePageSteps(ScenarioContext context, BrowserDriver browserDriver)
//             {
//                 _loginPage = new LoginPageObject(browserDriver.Current);
//                 _profilePage = new ProfilePageObject(browserDriver.Current);
//                 _scenarioContext = context;

//                 IConfigurationBuilder builder = new ConfigurationBuilder().AddUserSecrets<UserLoginsStepDefinitions>();
//                 Configuration = builder.Build();
//             }
//         }
//     }
// }