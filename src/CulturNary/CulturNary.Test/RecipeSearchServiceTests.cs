using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using CulturNary.Web.Services;
using Microsoft.Extensions.Configuration;
using Moq;
using Moq.Protected;
using NUnit.Framework;

namespace CulturNary.Tests.Services
{
    [TestFixture]
    public class RecipeSearchServiceTests
    {
        private Mock<HttpMessageHandler> _mockHttpMessageHandler;
        private Mock<IConfiguration> _mockConfiguration;
        private RecipeSearchService _service;

        [SetUp]
        public void SetUp()
        {
            // Reset mocks before each test
            _mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            var httpClient = new HttpClient(_mockHttpMessageHandler.Object)
            {
                BaseAddress = new Uri("https://api.edamam.com/")
            };

            _mockConfiguration = new Mock<IConfiguration>();
            _mockConfiguration.Setup(c => c["Edemam:RecipeAppId"]).Returns("testAppId");
            _mockConfiguration.Setup(c => c["Edemam:RecipeAppKey"]).Returns("testAppKey");

            _service = new RecipeSearchService(httpClient, _mockConfiguration.Object);
        }

        [TestCase("chicken")]
        [TestCase("pasta")]
        public async Task SearchRecipesAsync_ReturnsRecipes_OnValidQueries(string query)
        {
            var expectedResponse = "{\"recipes\":[]}";
            _mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(expectedResponse),
                });

            var result = await _service.SearchRecipesAsync(query);
            Assert.AreEqual(expectedResponse, result);
        }

        [Test]
        public void SearchRecipesAsync_ThrowsHttpRequestException_OnServerError()
        {
            _mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    ReasonPhrase = "Internal Server Error",
                });

            Assert.ThrowsAsync<HttpRequestException>(() => _service.SearchRecipesAsync("test"));
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void SearchRecipesAsync_ThrowsArgumentException_OnInvalidQuery(string query)
        {
            Assert.ThrowsAsync< InvalidOperationException> (() => _service.SearchRecipesAsync(query));
        }

        [Test]
        public async Task SearchRecipesAsync_CorrectlyEncodesQueryParameters()
        {
            var query = "chicken soup";
            // Use the same encoding as the service for consistency
            var encodedQuery = HttpUtility.UrlEncode(query);
            var expectedUri = new Uri($"https://api.edamam.com/api/recipes/v2?type=public&q={encodedQuery}&app_id=testAppId&app_key=testAppKey");

            HttpRequestMessage actualRequestMessage = null;
            _mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent("{\"recipes\":[]}")
                })
                .Callback((HttpRequestMessage request, CancellationToken token) =>
                {
                    actualRequestMessage = request;
                });

            await _service.SearchRecipesAsync(query);

            Assert.IsNotNull(actualRequestMessage);
            Assert.AreEqual(expectedUri, actualRequestMessage.RequestUri);
        }


        [Test]
        public void SearchRecipesAsync_HandlesTimeoutException()
        {
            _mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ThrowsAsync(new TaskCanceledException("The request was canceled due to the configured HttpClient timeout."));

            var ex = Assert.ThrowsAsync<TaskCanceledException>(() => _service.SearchRecipesAsync("timeout test"));
            Assert.That(ex.Message, Contains.Substring("The request was canceled due to the configured HttpClient timeout."));
        }

        [Test]
        public async Task SearchRecipesAsync_HandlesEmptyResponse()
        {
            _mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(""),
                });

            var result = await _service.SearchRecipesAsync("empty response");
            Assert.IsEmpty(result);
        }

    }
}
