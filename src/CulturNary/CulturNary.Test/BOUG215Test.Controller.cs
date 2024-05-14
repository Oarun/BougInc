using CulturNary.Web.Controllers;
using CulturNary.Web.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CulturNary.Test
{
    internal class BOUG215TestController
    {
        public class NewsApiControllerTests
        {
            private NewsApiController _controller;
            private Mock<INewsService> _newsServiceMock;

            [SetUp]
            public void SetUp()
            {
                _newsServiceMock = new Mock<INewsService>();
                _controller = new NewsApiController(_newsServiceMock.Object);
            }

            [Test]
            public async Task Search_ReturnsOkResult_WhenNewsServiceReturnsNews()
            {
                // Arrange
                var query = "test";
                var fakeNews = "{\"articles\": [{\"title\": \"Test News\"}]}";
                _newsServiceMock.Setup(n => n.GetNewsAsync(query)).ReturnsAsync(fakeNews);

                // Act
                var result = await _controller.Search(query);

                // Assert
                Assert.IsInstanceOf<OkObjectResult>(result);
                var okResult = result as OkObjectResult;
                Assert.AreEqual(fakeNews, okResult.Value);
            }

            [Test]
            public async Task Search_Returns503ServiceUnavailable_WhenNewsServiceThrowsHttpRequestException()
            {
                // Arrange
                var query = "test";
                _newsServiceMock.Setup(n => n.GetNewsAsync(query))
                                .ThrowsAsync(new HttpRequestException("Service is unavailable"));

                // Act
                var result = await _controller.Search(query);

                // Assert
                Assert.IsInstanceOf<ObjectResult>(result);
                var objectResult = result as ObjectResult;
                Assert.AreEqual(StatusCodes.Status503ServiceUnavailable, objectResult.StatusCode);
                Assert.AreEqual("Service is unavailable", objectResult.Value);
            }

            [Test]
            public async Task Search_Returns500InternalServerError_WhenNewsServiceThrowsException()
            {
                // Arrange
                var query = "test";
                _newsServiceMock.Setup(n => n.GetNewsAsync(query))
                                .ThrowsAsync(new Exception("Unexpected error"));

                // Act
                var result = await _controller.Search(query);

                // Assert
                Assert.IsInstanceOf<ObjectResult>(result);
                var objectResult = result as ObjectResult;
                Assert.AreEqual(StatusCodes.Status500InternalServerError, objectResult.StatusCode);
                Assert.AreEqual("Unexpected error", objectResult.Value);
            }
        }
    }
}