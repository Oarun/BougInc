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
using CulturNary.Web.Models;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using CulturNary.Web.Controllers;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

public class MockUserManager : UserManager<SiteUser>
{
    public MockUserManager()
        : base(new Mock<IUserStore<SiteUser>>().Object,
              new Mock<IOptions<IdentityOptions>>().Object,
              new Mock<IPasswordHasher<SiteUser>>().Object,
              new IUserValidator<SiteUser>[0],
              new IPasswordValidator<SiteUser>[0],
              new Mock<ILookupNormalizer>().Object,
              new Mock<IdentityErrorDescriber>().Object,
              new Mock<IServiceProvider>().Object,
              new Mock<ILogger<UserManager<SiteUser>>>().Object)
    { }
}
[TestFixture]
public class FriendApiControllerTests
{
    private Mock<IPersonRepository> _personRepositoryMock;
    private Mock<IFriendRequestRepository> _friendRequestRepositoryMock;
    private Mock<IFriendshipRepository> _friendshipRepositoryMock;
    private FriendApiController _controller;

    [SetUp]
    public void SetUp()
    {
        _personRepositoryMock = new Mock<IPersonRepository>();
        _friendRequestRepositoryMock = new Mock<IFriendRequestRepository>();
        _friendshipRepositoryMock = new Mock<IFriendshipRepository>();

        _controller = new FriendApiController(
            _personRepositoryMock.Object,
            _friendshipRepositoryMock.Object,
            _friendRequestRepositoryMock.Object);

        var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
        {
            new Claim(ClaimTypes.NameIdentifier, "1"),
        }));

        _controller.ControllerContext = new ControllerContext()
        {
            HttpContext = new DefaultHttpContext() { User = user }
        };
    }

    [Test]
    public async Task SendFriendRequest_WhenCalled_ReturnsOk()
    {
        // Arrange
        var friendId = "2";

        // Act
        var result = await _controller.SendFriendRequest(friendId);

        // Assert
        Assert.That(result, Is.TypeOf<OkObjectResult>());
        _friendRequestRepositoryMock.Verify(r => r.SendFriendRequest("1", friendId), Times.Once);
    }

    [Test]
    public void RespondToFriendRequest_WhenCalled_ReturnsRedirect()
    {
        var result = _controller.RespondToFriendRequest("1", true);

        Assert.That(result, Is.TypeOf<RedirectToActionResult>());
    }

    [Test]
    public void RemoveFriend_WhenCalled_ReturnsRedirect()
    {
        _personRepositoryMock.Setup(pr => pr.GetPersonByIdentityId(It.IsAny<string>())).Returns(new Person());
        _friendshipRepositoryMock.Setup(fr => fr.AreFriends(It.IsAny<string>(), It.IsAny<string>())).Returns(true);

        var result = _controller.RemoveFriend("2");

        Assert.That(result, Is.TypeOf<RedirectToActionResult>());
    }

    [Test]
    public void RemoveFriend_WhenCalledWithInvalidFriendId_ReturnsNotFound()
    {
        _personRepositoryMock.Setup(pr => pr.GetPersonByIdentityId(It.IsAny<string>())).Returns((Person)null);

        var result = _controller.RemoveFriend("invalid");

        Assert.That(result, Is.TypeOf<NotFoundResult>());
    }

    [Test]
    public void RemoveFriend_WhenCalledWithNonFriendId_ReturnsBadRequest()
    {
        _personRepositoryMock.Setup(pr => pr.GetPersonByIdentityId(It.IsAny<string>())).Returns(new Person());
        _friendshipRepositoryMock.Setup(fr => fr.AreFriends(It.IsAny<string>(), It.IsAny<string>())).Returns(false);

        var result = _controller.RemoveFriend("non-friend");

        Assert.That(result, Is.TypeOf<BadRequestObjectResult>());
    }

    [Test]
    public async Task SendFriendRequest_WhenCalledWithSelfId_ReturnsBadRequest()
    {
        // Arrange
        var friendId = "1"; // same as user id

        // Act
        var result = await _controller.SendFriendRequest(friendId);

        // Assert
        Assert.That(result, Is.TypeOf<BadRequestObjectResult>());
    }
    [Test]
    public void GetFriendshipByPersonId_WhenCalled_ReturnsCorrectNumberOfFriendships()
    {
        // Arrange
        var data = new List<Friendship>
        {
            new Friendship { Id = 1, Person1Id = 1, Person2Id = 2, FriendshipDate = DateTime.Now },
            new Friendship { Id = 2, Person1Id = 1, Person2Id = 3, FriendshipDate = DateTime.Now },
            new Friendship { Id = 3, Person1Id = 2, Person2Id = 1, FriendshipDate = DateTime.Now }
        }.AsQueryable();
    
        var mockSet = new Mock<DbSet<Friendship>>();
        mockSet.As<IQueryable<Friendship>>().Setup(m => m.Provider).Returns(data.Provider);
        mockSet.As<IQueryable<Friendship>>().Setup(m => m.Expression).Returns(data.Expression);
        mockSet.As<IQueryable<Friendship>>().Setup(m => m.ElementType).Returns(data.ElementType);
        mockSet.As<IQueryable<Friendship>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
    
        var mockContext = new Mock<CulturNaryDbContext>();
        mockContext.Setup(c => c.Friendships).Returns(mockSet.Object);
        mockContext.Setup(c => c.Set<Friendship>()).Returns(mockSet.Object); // Add this line
    
        var mockPersonRepository = new Mock<IPersonRepository>();
        var mockUserManager = new MockUserManager();
        var mockFriendRequestRepository = new Mock<IFriendRequestRepository>();
    
        var repository = new FriendshipRepository(mockContext.Object, mockPersonRepository.Object, mockUserManager, mockFriendRequestRepository.Object);
    
        // Act
        var result = repository.GetFriendshipByPersonId(1);
    
        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.Count(), Is.EqualTo(3));
    }
    [Test]
    public void AreFriends_WhenCalled_ReturnsCorrectResult()
    {
        // Arrange
        var data = new List<Friendship>
        {
            new Friendship { Id = 1, Person1 = new Person { IdentityId = "1" }, Person2 = new Person { IdentityId = "2" } },
            new Friendship { Id = 2, Person1 = new Person { IdentityId = "1" }, Person2 = new Person { IdentityId = "3" } },
            new Friendship { Id = 3, Person1 = new Person { IdentityId = "2" }, Person2 = new Person { IdentityId = "3" } }
        }.AsQueryable();

        var mockSet = new Mock<DbSet<Friendship>>();
        mockSet.As<IQueryable<Friendship>>().Setup(m => m.Provider).Returns(data.Provider);
        mockSet.As<IQueryable<Friendship>>().Setup(m => m.Expression).Returns(data.Expression);
        mockSet.As<IQueryable<Friendship>>().Setup(m => m.ElementType).Returns(data.ElementType);
        mockSet.As<IQueryable<Friendship>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

        var mockContext = new Mock<CulturNaryDbContext>();
        mockContext.Setup(c => c.Friendships).Returns(mockSet.Object);
        mockContext.Setup(c => c.Set<Friendship>()).Returns(mockSet.Object);

        var mockPersonRepository = new Mock<IPersonRepository>();
        var mockUserManager = new MockUserManager();
        var mockFriendRequestRepository = new Mock<IFriendRequestRepository>();

        var repository = new FriendshipRepository(mockContext.Object, mockPersonRepository.Object, mockUserManager, mockFriendRequestRepository.Object);

        // Act
        var result1 = repository.AreFriends("1", "2");
        var result2 = repository.AreFriends("1", "3");
        var result3 = repository.AreFriends("1", "4");

        // Assert
        Assert.That(result1, Is.True);
        Assert.That(result2, Is.True);
        Assert.That(result3, Is.False);
    }
    
    [Test]
    public void AcceptFriendRequest_InvalidFriendRequest_ReturnsFalse()
    {
        // Arrange
        var mockPersonRepository = new Mock<IPersonRepository>();
        mockPersonRepository.Setup(r => r.GetPersonByIdentityId(It.IsAny<string>())).Returns(new Person { Id = 1 });
    
        var mockFriendRequestRepository = new Mock<IFriendRequestRepository>();
        mockFriendRequestRepository.Setup(r => r.GetByRequestAndRecipientId(It.IsAny<int>(), It.IsAny<int>())).Returns((FriendRequest)null);
    
        var mockDbSet = new Mock<DbSet<Friendship>>();
        var mockContext = new Mock<CulturNaryDbContext>();
        mockContext.Setup(c => c.Friendships).Returns(mockDbSet.Object);
    
        var mockUserManager = new Mock<UserManager<SiteUser>>(new Mock<IUserStore<SiteUser>>().Object, null, null, null, null, null, null, null, null);
    
        var repository = new FriendshipRepository(mockContext.Object, mockPersonRepository.Object, mockUserManager.Object, mockFriendRequestRepository.Object);
    
        // Act
        bool result = repository.AcceptFriendRequest("1", "2");
    
        // Assert
        Assert.IsFalse(result);
    }
    [Test]
    public void RejectFriendRequest_ValidRequest_ReturnsTrue()
    {
        // Arrange
        var mockPersonRepository = new Mock<IPersonRepository>();
        mockPersonRepository.Setup(r => r.GetPersonByIdentityId(It.IsAny<string>())).Returns(new Person { Id = 1 });
    
        var mockFriendRequestRepository = new Mock<IFriendRequestRepository>();
        mockFriendRequestRepository.Setup(r => r.GetByRequestAndRecipientId(It.IsAny<int>(), It.IsAny<int>())).Returns(new FriendRequest { RequesterId = 1, RecipientId = 2 });
    
        var mockDbSet = new Mock<DbSet<Friendship>>();
        var mockContext = new Mock<CulturNaryDbContext>();
        mockContext.Setup(c => c.Friendships).Returns(mockDbSet.Object);
    
        var mockUserManager = new Mock<UserManager<SiteUser>>(new Mock<IUserStore<SiteUser>>().Object, null, null, null, null, null, null, null, null);
    
        var repository = new FriendshipRepository(mockContext.Object, mockPersonRepository.Object, mockUserManager.Object, mockFriendRequestRepository.Object);
    
        // Act
        bool result = repository.RejectFriendRequest("1", "2");
    
        // Assert
        Assert.IsTrue(result);
    }
    
    
    
       
}