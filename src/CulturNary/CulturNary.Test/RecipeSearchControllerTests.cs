using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using CulturNary.Web.Controllers;
using CulturNary.Web.Services;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace CulturNary.Tests.Controllers
{
    [TestFixture]
    public class RecipeSearchControllerTests
    {
        private Mock<IRecipeSearchService> _mockRecipeSearchService;
        private RecipeSearchController _controller;

        [SetUp]
        public void Setup()
        {
            _mockRecipeSearchService = new Mock<IRecipeSearchService>();
            _controller = new RecipeSearchController(_mockRecipeSearchService.Object);
        }

        [Test]
        public async Task Search_ReturnsOk_WithValidQuery()
        {
            // Arrange
            var query = "chicken";
            var expectedServiceResult = "{\"recipes\":[]}";
            _mockRecipeSearchService.Setup(service => service.SearchRecipesAsync(query))
                                    .ReturnsAsync(expectedServiceResult);

            // Act
            var result = await _controller.Search(query);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.AreEqual(expectedServiceResult, okResult.Value);
        }

        [Test]
        public async Task Search_ReturnsServiceUnavailable_OnHttpRequestException()
        {
            // Arrange
            var query = "chicken";
            _mockRecipeSearchService.Setup(s => s.SearchRecipesAsync(query)).ThrowsAsync(new HttpRequestException("Service unavailable"));

            // Act
            var result = await _controller.Search(query);

            // Assert
            Assert.IsInstanceOf<ObjectResult>(result);
            var objectResult = result as ObjectResult;
            Assert.AreEqual(StatusCodes.Status503ServiceUnavailable, objectResult.StatusCode);
            Assert.IsTrue(objectResult.Value.ToString().Contains("Service unavailable"));
        }


        [Test]
        public async Task Search_CallsRecipeSearchService_WithCorrectQuery()
        {
            // Arrange
            var query = "salad";
            _mockRecipeSearchService.Setup(s => s.SearchRecipesAsync(It.IsAny<string>()))
                                    .ReturnsAsync("Service response");

            // Act
            await _controller.Search(query);

            // Assert
            _mockRecipeSearchService.Verify(s => s.SearchRecipesAsync(query), Times.Once());
        }

    }
}