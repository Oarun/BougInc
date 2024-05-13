// using Moq;
// using NUnit.Framework;
// using Microsoft.AspNetCore.Identity;
// using Microsoft.Extensions.Logging; 
// using CulturNary.Web.Areas.Identity.Data;
// using CulturNary.DAL.Abstract; 
// using CulturNary.DAL.Concrete;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using CulturNary.Web.Models;

// namespace CulturNary.Test
// {
//     [TestFixture]
//     public class BOUG214Test
//     {
//         private Mock<UserManager<SiteUser>> _userManager;
//         private Mock<ILogger<PersonRepository>> _logger;
//         private Mock<IPersonRepository> _personRepository;
//         private PersonRepository _personRepositoryConcrete;

//         [SetUp]
//         public void Setup()
//         {
//             _userManager = new Mock<UserManager<SiteUser>>();
//             _logger = new Mock<ILogger<PersonRepository>>();
//             _personRepository = new Mock<IPersonRepository>();
//             _personRepositoryConcrete = new PersonRepository(null, _userManager.Object, _logger.Object);
//         }

//         [Test]
//         public void GetPersonByIdentityId_WhenCalled_ReturnsPerson()
//         {
//             // Arrange
//             var identityId = "123";
//             var person = new Person { IdentityId = identityId };
//             _personRepository.Setup(x => x.GetPersonByIdentityId(identityId)).Returns(person);

//             // Act
//             var result = _personRepositoryConcrete.GetPersonByIdentityId(identityId);

//             // Assert
//             Assert.That(result, Is.EqualTo(person));
//         }

//         [Test]
//         public async Task GetUsersWithDietaryRestrictions_WhenCalled_ReturnsDictionary()
//         {
//             // Arrange
//             var currentUserId = "123";
//             var currentUser = new SiteUser { Id = currentUserId };
//             var users = new List<SiteUser> { currentUser };
//             _userManager.Setup(x => x.FindByIdAsync(currentUserId)).ReturnsAsync(currentUser);
//             _userManager.Setup(x => x.Users).Returns(users.AsQueryable());
//             _userManager.Setup(x => x.Users).Returns(users.AsQueryable());
//             _logger.Setup(x => x.LogInformation(It.IsAny<string>()));
//             _personRepository.Setup(x => x.GetUsersWithDietaryRestrictions(currentUserId)).ReturnsAsync(new Dictionary<SiteUser, double>());

//             // Act
//             var result = await _personRepositoryConcrete.GetUsersWithDietaryRestrictions(currentUserId);

//             // Assert
//             Assert.That(result, Is.TypeOf<Dictionary<SiteUser, double>>());
//         }
//     }
// }