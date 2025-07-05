namespace NewsApi.Mappers
{
    public static class NewsMapper
    {
        public static NewsItem MapToNewsItem(GNewsItem gNewsItem)
        {
            if (gNewsItem != null)
            {
                return new NewsItem
                {
                    Title = gNewsItem.Title,
                    Description = gNewsItem.Description,
                    Content = gNewsItem.Content,
                    Url = gNewsItem.Url,
                    Image = gNewsItem.Image,
                    PublishedAt = gNewsItem.PublishedAt,
                    Source = new NewsSource
                    {
                        Name = gNewsItem.Source.Name,
                        Url = gNewsItem.Source.Url
                    }
                };
            }
            else
            {
                return null;
            }
        }
    }
}
