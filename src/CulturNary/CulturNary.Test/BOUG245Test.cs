// using CulturNary.DAL.Concrete;
// using CulturNary.DAL.Abstract;
// using CulturNary.Web.Models;
// using CulturNary.Web.Areas.Identity.Data;
// using Microsoft.AspNetCore.Identity;
// using Microsoft.EntityFrameworkCore;
// using Moq;
// using NUnit.Framework;
// using System;
// using System.Collections.Generic;
// using System.Linq;
// using Microsoft.EntityFrameworkCore;
// using Microsoft.Extensions.DependencyInjection;

// [TestFixture]
// public class BlockedUserRepositoryTests
// {
//     private Mock<DbSet<BlockedUser>> _blockedUsersMock;
//     private Mock<CulturNaryDbContext> _contextMock;
//     private Mock<UserManager<SiteUser>> _userManagerMock;
//     private Mock<IPersonRepository> _personRepositoryMock;
//     private BlockedUserRepository _repository;

//     [SetUp]
//     public void SetUp()
//     {
//         var data = new List<BlockedUser>().AsQueryable();
    
//         _blockedUsersMock = new Mock<DbSet<BlockedUser>>();
//         _blockedUsersMock.As<IQueryable<BlockedUser>>().Setup(m => m.Provider).Returns(data.Provider);
//         _blockedUsersMock.As<IQueryable<BlockedUser>>().Setup(m => m.Expression).Returns(data.Expression);
//         _blockedUsersMock.As<IQueryable<BlockedUser>>().Setup(m => m.ElementType).Returns(data.ElementType);
//         _blockedUsersMock.As<IQueryable<BlockedUser>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
    
//         var options = new DbContextOptionsBuilder<CulturNaryDbContext>()
//             .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
//             .UseInternalServiceProvider(new ServiceCollection()
//                 .AddEntityFrameworkInMemoryDatabase()
//                 .BuildServiceProvider())
//             .Options;
//         _contextMock = new Mock<CulturNaryDbContext>(options);
//         _contextMock.Setup(x => x.BlockedUsers).Returns(_blockedUsersMock.Object);
    
//         var userStoreMock = new Mock<IUserStore<SiteUser>>();
//         _userManagerMock = new Mock<UserManager<SiteUser>>(userStoreMock.Object, null, null, null, null, null, null, null, null);
    
//         _personRepositoryMock = new Mock<IPersonRepository>();
    
//         _repository = new BlockedUserRepository(_contextMock.Object, _personRepositoryMock.Object, _userManagerMock.Object);
//     }

//     [Test]
//     public void BlockUser_WhenCalled_AddsBlockedUserToDbSet()
//     {
//         // Arrange
//         var blockerId = "testUserId";
//         var blockedId = "testFriendId";
//         var blockedUser = new BlockedUser { BlockerIdentityId = blockerId, BlockedIdentityId = blockedId };
//         _blockedUsersMock.Setup(m => m.Add(It.IsAny<BlockedUser>()));
    
//         // Act
//         _repository.BlockUser(blockerId, blockedId);
    
//         // Assert
//         _blockedUsersMock.Verify(m => m.Add(It.IsAny<BlockedUser>()), Times.Once);
//     }
//     [Test]
//     public void UnblockUser_WhenCalled_RemovesBlockedUserFromDbSet()
//     {
//         // Arrange
//         var blockerId = "testUserId";
//         var blockedId = "testFriendId";
//         var blockedUser = new BlockedUser { BlockerIdentityId = blockerId, BlockedIdentityId = blockedId };
//         _blockedUsersMock.Setup(m => m.Find(blockerId, blockedId)).Returns(blockedUser);
    
//         // Act
//         _repository.UnblockUser(blockerId, blockedId);
    
//         // Assert
//         _blockedUsersMock.Verify(m => m.Remove(blockedUser), Times.Once);
//     }
    
//     [Test]
//     public void IsUserBlocked_WhenUserIsBlocked_ReturnsTrue()
//     {
//         // Arrange
//         var blockerId = "testUserId";
//         var blockedId = "testFriendId";
//         var blockedUser = new BlockedUser { BlockerIdentityId = blockerId, BlockedIdentityId = blockedId };
//         _blockedUsersMock.Setup(m => m.Find(blockerId, blockedId)).Returns(blockedUser);
    
//         // Act
//         var result = _repository.IsUserBlocked(blockerId, blockedId);
    
//         // Assert
//         Assert.IsTrue(result);
//     }
    
//     [Test]
//     public void GetBlockedUsers_WhenCalled_ReturnsBlockedUsers()
//     {
//         // Arrange
//         var blockedUsers = new List<BlockedUser>
//         {
//             new BlockedUser { BlockerIdentityId = "testUserId1", BlockedIdentityId = "testFriendId1" },
//             new BlockedUser { BlockerIdentityId = "testUserId2", BlockedIdentityId = "testFriendId2" },
//         }.AsQueryable();
//         _blockedUsersMock.As<IQueryable<BlockedUser>>().Setup(m => m.Provider).Returns(blockedUsers.Provider);
//         _blockedUsersMock.As<IQueryable<BlockedUser>>().Setup(m => m.Expression).Returns(blockedUsers.Expression);
//         _blockedUsersMock.As<IQueryable<BlockedUser>>().Setup(m => m.ElementType).Returns(blockedUsers.ElementType);
//         _blockedUsersMock.As<IQueryable<BlockedUser>>().Setup(m => m.GetEnumerator()).Returns(blockedUsers.GetEnumerator());
//         _blockedUsersMock.Setup(m => m.AsNoTracking()).Returns(_blockedUsersMock.Object);

//         // Act
//         var userId = "testUserId";
//         var result = _repository.GetBlockedUsers(userId);

//         // Assert
//         Assert.AreEqual(blockedUsers.Count(), result.Count());
//     }
//     // Add more tests for UnblockUser, IsUserBlocked and GetBlockedUsers
// }