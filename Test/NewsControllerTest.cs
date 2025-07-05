using Microsoft.AspNetCore.Mvc.Testing;
using WireMock.RequestBuilders;
using WireMock.Server;
using Xunit;

namespace NewsApi.Controllers
{
    public class NewsControllerTest : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;
        private readonly WireMockServer _mockServer;

        public NewsControllerTest(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
            _mockServer = WireMockServer.Start();
        }

        [Theory]
        [InlineData("/news/fetch?numberOfArticles=5", "/top-headlines")]
        [InlineData("/news/Search/foo?numberOfArticles=5", "/search")]
        [InlineData("/news/search/at/2025-05-13", "/top-headlines")]
        public async Task Get_EndpointsReturnSuccessAndCorrectContentType(string url, string mockPath)
        {
            // Arrange
            _mockServer.ResetMappings();
            GNews gNews = new GNews
            {
                TotalArticles = 10,
                Articles = new List<GNewsItem>
                {
                    new GNewsItem
                    {
                        Title = "Sample News",
                        Description = "This is a sample news description.",
                        Content = "Full content of the news.",
                        Url = "http://example.com/news",
                        Image = "http://example.com/image.jpg",
                        Source = new GNewsSource
                        {
                            Name = "Example News Source",
                            Url = "http://example.com/source"
                        }
                    }
                }
            };
            _mockServer.Given(
                Request.Create()
                .UsingGet()
                .WithPath(mockPath))
            .RespondWith(
                WireMock.ResponseBuilders.Response.Create()
                .WithStatusCode(System.Net.HttpStatusCode.OK)
                .WithBodyAsJson(gNews));

            // Act
            var response = await _client.GetAsync(url);

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.NotNull(response.Content.Headers.ContentType);
            Assert.Equal("application/json; charset=utf-8", response.Content.Headers.ContentType.ToString());
        }

        [Theory]
        [InlineData("/news/Search/foo%20bar?numberOfArticles=5")]
        public async Task InvalidSearchString_ExpectBadRequest(string url)
        {
            // Arrange
            _mockServer.ResetMappings();
            GNews gNews = new GNews
            {
                TotalArticles = 0,
                Articles = new List<GNewsItem>()
            };
            _mockServer.Given(
                Request.Create()
                .UsingGet()
                .WithPath("/search"))
            .RespondWith(
                WireMock.ResponseBuilders.Response.Create()
                .WithStatusCode(System.Net.HttpStatusCode.OK)
                .WithBodyAsJson(gNews));

            // Act
            var response = await _client.GetAsync(url);

            // Assert
            Assert.Equal(response.StatusCode, System.Net.HttpStatusCode.BadRequest);
            Assert.NotNull(response.Content.Headers.ContentType);
            Assert.Equal("application/problem+json; charset=utf-8", response.Content.Headers.ContentType.ToString());
        }
    }
}