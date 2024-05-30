using CulturNary.Web.Controllers;
using CulturNary.DAL.Abstract;
using Moq;
using NUnit.Framework;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Security.Principal;
using System.Threading.Tasks;
using CulturNary.Web.Models;

[TestFixture]
public class FriendApiControllerTests_BOUG245
{
    private Mock<IPersonRepository> _personRepositoryMock;
    private Mock<IFriendRequestRepository> _friendRequestRepositoryMock;
    private Mock<IFriendshipRepository> _friendshipRepositoryMock;
    private Mock<IBlockedUserRepository> _blockedUserRepositoryMock;
    private Mock<ISharedRecipeRepository> _sharedRecipeRepositoryMock;
    private FriendApiController _controller;

    [SetUp]
    public void SetUp_BOUG245()
    {
        _personRepositoryMock = new Mock<IPersonRepository>();
        _friendRequestRepositoryMock = new Mock<IFriendRequestRepository>();
        _friendshipRepositoryMock = new Mock<IFriendshipRepository>();
        _blockedUserRepositoryMock = new Mock<IBlockedUserRepository>();
        _sharedRecipeRepositoryMock = new Mock<ISharedRecipeRepository>();

        var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
        {
            new Claim(ClaimTypes.NameIdentifier, "1"),
        }));

        _controller = new FriendApiController(_personRepositoryMock.Object, _friendshipRepositoryMock.Object, _friendRequestRepositoryMock.Object, _blockedUserRepositoryMock.Object, _sharedRecipeRepositoryMock.Object)
        {
            ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext() { User = user }
            }
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
    }


    [Test]
    public void RespondToFriendRequest_WhenCalled_ReturnsRedirect()
    {
        // Arrange
        var requestId = "1";
        var accept = true;

        // Act
        var result = _controller.RespondToFriendRequest(requestId, accept);

        // Assert
        Assert.That(result, Is.TypeOf<RedirectToActionResult>());
    }
    [Test]
    public void BlockFriendRequest_WhenCalled_CallsBlockUserOnRepository()
    {
        // Arrange
        var userId = "test-user-id";

        // Act
        _controller.BlockFriendRequest(userId);

        // Assert
        _blockedUserRepositoryMock.Verify(r => r.BlockUser(It.IsAny<string>(), userId), Times.Once);
    }

    [Test]
    public void UnblockUser_WhenCalled_CallsUnblockUserOnRepository()
    {
        // Arrange
        var userId = "test-user-id";

        // Act
        _controller.UnblockUser(userId);

        // Assert
        _blockedUserRepositoryMock.Verify(r => r.UnblockUser(It.IsAny<string>(), userId), Times.Once);
    }
    
}