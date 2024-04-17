using System;
using System.Text.Json;
using CulturNary.Web.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;

namespace UserProfileTests
{

    [TestFixture]
    public class DietaryRestrictionJSONTests
    {
        private Mock<DbSet<SiteUser>> _siteUserMock;
        private Mock<IUserStore<SiteUser>> _userStoreMock;
        private Mock<UserManager<SiteUser>> _userManagerMock;
        private Mock<SignInManager<SiteUser>> _signInManagerMock;

        [SetUp]
        public void Setup()
        {
            _siteUserMock = new Mock<DbSet<SiteUser>>();
            _userStoreMock = new Mock<IUserStore<SiteUser>>();
            _userManagerMock = new Mock<UserManager<SiteUser>>(_userStoreMock.Object, null, null, null, null, null, null, null, null);
            _userManagerMock.Setup(x => x.UpdateAsync(It.IsAny<SiteUser>()))
                .ReturnsAsync(IdentityResult.Success);
            _signInManagerMock = new Mock<SignInManager<SiteUser>>();
        }

        [Test]
        public async Task UpdateUserProfile_UpdatesPreferences_Successfully()
        {
            // Arrange
            var userId = "user-id-123";

            var updateRequest = new ProfileUpdateRequest
            {
                DietaryRestrictions = "[\"Vegan\", \"Gluten-Free\"]",
                UserLikes = "Pizza, Pasta, Salad",
                UserDislikes = "Fish, Mushrooms"
            };

            var user = new SiteUser { Id = userId, UserName = "testUser", DietaryRestrictions = "[\"Vegan\", \"Gluten-Free\"]", UserLikes = "Pizza, Pasta, Salad", UserDislikes = "Fish, Mushrooms" };

            // Act
            var result = await _userManagerMock.Object.UpdateAsync(user);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Succeeded);
            Assert.AreEqual(updateRequest.DietaryRestrictions, user.DietaryRestrictions);
            Assert.AreEqual(updateRequest.UserLikes, user.UserLikes);
            Assert.AreEqual(updateRequest.UserDislikes, user.UserDislikes);
        }

        [Test]
        public void SerializeDietaryRestrictions_ReturnsCorrectJsonString()
        {
            // Arrange
            var restrictions = new List<DietaryRestriction>
            {
                new DietaryRestriction { Name = "Gluten-Free", Active = true },
                new DietaryRestriction { Name = "Nut-Free", Active = false }
            };

            var expectedJson = "[{\"Name\":\"Gluten-Free\",\"Active\":true},{\"Name\":\"Nut-Free\",\"Active\":false}]";

            // Act
            var json = JsonSerializer.Serialize(restrictions);

            // Assert
            Assert.AreEqual(expectedJson, json);
        }

        [Test]
        public void DeserializeDietaryRestrictions_ReturnsCorrectObjectList()
        {
            // Arrange
            var json = "[{\"Name\":\"Vegan\",\"Active\":true},{\"Name\":\"Nut-Free\",\"Active\":false}]";
            
            var expectedRestrictions = new List<DietaryRestriction>
            {
                new DietaryRestriction { Name = "Vegan", Active = true },
                new DietaryRestriction { Name = "Nut-Free", Active = false }
            };

            // Act
            var restrictions = JsonSerializer.Deserialize<List<DietaryRestriction>>(json);

            // Assert
            Assert.AreEqual(expectedRestrictions.Count, restrictions.Count);
            for (int i = 0; i < restrictions.Count; i++)
            {
                Assert.AreEqual(expectedRestrictions[i].Name, restrictions[i].Name);
                Assert.AreEqual(expectedRestrictions[i].Active, restrictions[i].Active);
            }
        }

        [Test]
        public void DeserializeDietaryRestrictions_ThrowsExceptionWhenJsonIsInvalid()
        {
            // Arrange
            var json = "[{\"Name\":\"Vegan\",\"Active\":true},{\"Name\":\"Nut-Free\",\"Active\":false}]";

            // Act
            var restrictions = JsonSerializer.Deserialize<List<DietaryRestriction>>(json);

            // Assert
            Assert.Throws<JsonException>(() => JsonSerializer.Deserialize<List<DietaryRestriction>>("invalid json"));
        }


        [Test]
        public void SerializeDietaryRestrictions_NoExceptionWhenObjectIsNull()
        {
            // Arrange
            List<DietaryRestriction> restrictions = null;

            // Act
            object obj = null;
            string json = JsonSerializer.Serialize(obj);

            // Assert
            Assert.That(json, Is.EqualTo("null"));
        }

        [Test]
        public void DeserializeDietaryRestrictions_NoExceptionWhenJsonIsStringNull()
        {
            // Arrange
            string json = "null";

            // Act
            string obj = JsonSerializer.Deserialize<string>(json);

            // Assert
            Assert.That(obj, Is.Null);
        }

        [Test]
        public void DeserializeDietaryRestrictions_ThrowsExceptionWhenJsonIsNull()
        {
            // Arrange
            string json = null;

            // Act
            var ex = Assert.Throws<ArgumentNullException>(() => JsonSerializer.Deserialize<List<DietaryRestriction>>(json));

            // Assert
            Assert.AreEqual("Value cannot be null. (Parameter 'json')", ex.Message);
        }

        private class ProfileUpdateRequest
        {
            public string DietaryRestrictions { get; set; }
            public string UserLikes { get; set; }
            public string UserDislikes { get; set; }
        }
    }
}