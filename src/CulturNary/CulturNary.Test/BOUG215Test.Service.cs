using CulturNary.Web.Services;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Moq;
using Moq.Protected;
using NUnit.Framework;
using Microsoft.Extensions.Configuration;
namespace CulturNary.Test
{
    internal class BOUG215TestService
    {
        public class NewsServiceTests
        {
            private NewsService _newsService;
            private Mock<HttpMessageHandler> _httpMessageHandlerMock;
            private Mock<IConfiguration> _configurationMock;

            [SetUp]
            public void SetUp()
            {
                _httpMessageHandlerMock = new Mock<HttpMessageHandler>();
                var httpClient = new HttpClient(_httpMessageHandlerMock.Object);
                _configurationMock = new Mock<IConfiguration>();

                _newsService = new NewsService(httpClient, _configurationMock.Object);
            }

            [Test]
            public async Task GetNewsAsync_ReturnsNews_WhenResponseIsSuccessful()
            {
                // Arrange
                var fakeNews = "{\"articles\": [{\"title\": \"Test News\"}]}";
                var query = "test";
                var apiKey = "dummyApiKey";
                _configurationMock.Setup(c => c["NewsApiKey"]).Returns(apiKey);
                _httpMessageHandlerMock.Protected()
                    .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                    .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.OK)
                    {
                        Content = new StringContent(fakeNews)
                    });

                // Act
                var result = await _newsService.GetNewsAsync(query);

                // Assert
                Assert.AreEqual(fakeNews, result);
            }

            [Test]
            public void GetNewsAsync_ThrowsHttpRequestException_WhenResponseIsNotSuccessful()
            {
                // Arrange
                var query = "test";
                var apiKey = "dummyApiKey";
                _configurationMock.Setup(c => c["NewsApiKey"]).Returns(apiKey);
                _httpMessageHandlerMock.Protected()
                    .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                    .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.BadRequest)
                    {
                        ReasonPhrase = "Bad Request",
                        Content = new StringContent("Error occurred")
                    });

                // Act & Assert
                var ex = Assert.ThrowsAsync<HttpRequestException>(async () => await _newsService.GetNewsAsync(query));
                Assert.That(ex.Message, Is.EqualTo("Error fetching news: BadRequest Bad Request"));
            }
        }
    }
}
