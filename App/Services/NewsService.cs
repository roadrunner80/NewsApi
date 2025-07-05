using System;
using System.Collections.Generic;
using System.Linq;
using NewsApi.Clients;
using NewsApi.Mappers;

namespace NewsApi.Services
{
    public interface INewsService
    {
        List<NewsItem> Fetch(int numberOfArticles);
        List<NewsItem> Search(string keyword, int numberOfArticles);
        NewsItem Search(DateOnly date);
    }

    public class NewsService : INewsService
    {
        private readonly IGNewsClient _gNewsClient;

        public NewsService(IGNewsClient gNewsClient)
        {
            _gNewsClient = gNewsClient;
        }
        
        public List<NewsItem> Fetch(int numberOfArticles)
        {
            return _gNewsClient.TopHeadlines(numberOfArticles)
                .Articles.Select(article => NewsMapper.MapToNewsItem(article))
                .ToList();
        }

        public List<NewsItem> Search(string keyword, int numberOfArticles)
        {
            return _gNewsClient.Search(keyword, numberOfArticles)
                .Articles.Select(article => NewsMapper.MapToNewsItem(article))
                .ToList();
        }

        public NewsItem Search(DateOnly date)
        {
            return NewsMapper.MapToNewsItem(_gNewsClient.Search(date));
        }
    }
}