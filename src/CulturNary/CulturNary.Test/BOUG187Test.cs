//using NUnit.Framework;
//using Moq;
//using CulturNary.DAL.Concrete;
//using CulturNary.DAL.Abstract;
//using CulturNary.Web.Models;
//using System.Linq;
//using Microsoft.EntityFrameworkCore;

//[TestFixture]
//public class FriendRequestRepositoryTests
//{
//    private FriendRequestRepository _repository;
//    private Mock<DbSet<FriendRequest>> _mockSet;
//    private Mock<CulturNaryDbContext> _mockContext;

//    [SetUp]
//    public void Setup()
//    {
//        _mockSet = new Mock<DbSet<FriendRequest>>();
//        _mockContext = new Mock<CulturNaryDbContext>();
//        _mockContext.Setup(m => m.FriendRequests).Returns(_mockSet.Object);
//        _repository = new FriendRequestRepository(_mockContext.Object);
//    }

//    [Test]
//    public void SendFriendRequestToPerson_ShouldCallAddOnDbSet()
//    {
//        _repository.SendFriendRequestToPerson(1, 2);
//        _mockSet.Verify(m => m.Add(It.IsAny<FriendRequest>()), Times.Once);
//    }

//    [Test]
//    public void AcceptFriendRequest_ShouldUpdateFriendRequestStatus()
//    {
//        // You need to setup a FriendRequest to be returned by the context
//        // Then you can verify that the status of the FriendRequest was updated
//    }

//    [Test]
//    public void RejectFriendRequest_ShouldUpdateFriendRequestStatus()
//    {
//        // Similar to the AcceptFriendRequest test
//    }

//    [Test]
//    public void GetFriendRequestByRequesterIdAndRecipientId_ShouldReturnCorrectFriendRequest()
//    {
//        // You need to setup a FriendRequest to be returned by the context
//        // Then you can verify that the correct FriendRequest was returned
//    }
//}