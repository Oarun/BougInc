using Moq;
using NUnit.Framework;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging; 
using CulturNary.Web.Areas.Identity.Data;
using CulturNary.DAL.Abstract; 
using CulturNary.DAL.Concrete;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

[TestFixture]
public class PersonRepositoryTests
{
    private Mock<UserManager<SiteUser>> _userManagerMock;
    private Mock<ILogger<PersonRepository>> _loggerMock;
    private PersonRepository _personRepository;

    [SetUp]
    public void Setup()
    {
        var dbContextMock = new Mock<CulturNaryDbContext>();
        _userManagerMock = new Mock<UserManager<SiteUser>>();
        _loggerMock = new Mock<ILogger<PersonRepository>>();
        _personRepository = new PersonRepository(dbContextMock.Object, _userManagerMock.Object, _loggerMock.Object);
    }

    [Test]
    public async Task GetUsersWithDietaryRestrictions_ReturnsCorrectScores()
    {
        // Arrange
        var testUserId = "testUserId";
        var users = new List<SiteUser>
        {
            new SiteUser { Id = testUserId, UserName = "TestUser", DietaryRestrictions = "Vegan" },
            new SiteUser { Id = "otherUserId", UserName = "OtherUser", DietaryRestrictions = "Vegetarian" }
        };
        var userStoreMock = new Mock<IUserStore<SiteUser>>();
        _userManagerMock = new Mock<UserManager<SiteUser>>(userStoreMock.Object, null, null, null, null, null, null, null, null);
        _userManagerMock.Setup(um => um.Users).Returns(users.AsQueryable());

        _personRepository = new PersonRepository(_userManagerMock.Object, _loggerMock.Object);

        // Act
        var result = await _personRepository.GetUsersWithDietaryRestrictions(testUserId);

        // Assert
        Assert.AreEqual(1, result.Count);
        Assert.AreEqual("Vegan", result[0].DietaryRestrictions);
    }
}