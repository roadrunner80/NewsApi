using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NewsApi.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NewsApi.Controllers
{
    [ApiController]
    [Route("news")]
    public class NewsController : ControllerBase
    {
        private readonly INewsService _newsService;

        public NewsController(INewsService newsService)
        {
            _newsService = newsService;
        }

        [HttpGet]
        [Route("fetch")]
        public IEnumerable<NewsItem> Get(int numberOfArticles = 5)
        {
            return _newsService.Fetch(numberOfArticles);
        }

        [HttpGet]
        [Route("search/{keyword}")]
        public IEnumerable<NewsItem> Search([RegularExpression(@"^\w{1,40}$", ErrorMessage = "Characters are not allowed.")][FromRoute] string keyword, int numberOfArticles = 5)
        {
            return _newsService.Search(keyword, numberOfArticles);
        }

        [HttpGet]
        [Route("search/at/{date}")]
        public ActionResult<NewsItem> Search(System.DateOnly date)
        {
            var newsItem = _newsService.Search(date);
            if (newsItem == null)
            {
                return NotFound(new { Message = $"No news found for the date {date}." });
            }
            else
            {
                return newsItem;
            }
        }
    }
}