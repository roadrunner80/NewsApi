namespace NewsApi.Mappers
{
    public class MapperTest
    {
        [Fact]
        public void TestMapper()
        {
            // Arrange
            var gNewsItem = new GNewsItem
            {
                Title = "Sample News",
                Description = "This is a sample news description.",
                Content = "Full content of the news.",
                Url = "http://example.com/news",
                Image = "http://example.com/image.jpg",
                PublishedAt = DateTime.Parse("2023-10-01T12:00:00Z"),
                Source = new GNewsSource
                {
                    Name = "Example News Source",
                    Url = "http://example.com/source"
                }
            };

            // Act
            var newsItem = NewsMapper.MapToNewsItem(gNewsItem);

            // Assert
            Assert.NotNull(newsItem);
            Assert.Equal("Sample News", newsItem.Title);
            Assert.Equal("This is a sample news description.", newsItem.Description);
            Assert.Equal("Full content of the news.", newsItem.Content);
            Assert.Equal("http://example.com/news", newsItem.Url);
            Assert.Equal("http://example.com/image.jpg", newsItem.Image);
            Assert.Equal(DateTime.Parse("2023-10-01T12:00:00Z"), newsItem.PublishedAt);
            Assert.NotNull(newsItem.Source);
            Assert.Equal("Example News Source", newsItem.Source.Name);
            Assert.Equal("http://example.com/source", newsItem.Source.Url);

        }
    }
}