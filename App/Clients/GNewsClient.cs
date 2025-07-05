using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace NewsApi.Clients
{
    public interface IGNewsClient
    {
        GNews TopHeadlines(int numberOfArticles);
        GNews Search(string searchKeywords, int numberOfArticles);
        GNewsItem Search(DateOnly at);
    }

    public class GNewsClient : IGNewsClient
    {

        const string TOP_HEADLINES_ENDPOINT = "top-headlines";
        const string SEARCH_ENDPOINT = "search";
        const string API_KEY = "925d2cb933f8d012fe447aba723deef6";
        const string LANGUAGE = "de";
        private readonly HttpClient _httpClient;

        public GNewsClient(IHttpClientFactory factory)
        {
            _httpClient = factory.CreateClient("gNewsClient");
            var overwriteGNewsApiBaseUrl = Environment.GetEnvironmentVariable("GNEWS_API_BASE_URL");
            if (!string.IsNullOrEmpty(overwriteGNewsApiBaseUrl))
            {
                _httpClient.BaseAddress = new System.Uri(overwriteGNewsApiBaseUrl);
            }
            else
            {
                _httpClient.BaseAddress = new System.Uri("https://gnews.io/api/v4/");
            }
        }

        public GNews TopHeadlines(int numberOfArticles)
        {
            string requestUri = $"{TOP_HEADLINES_ENDPOINT}?token={API_KEY}&lang={LANGUAGE}&max={numberOfArticles}";
            var response = _httpClient.GetAsync(requestUri).Result;
            response.EnsureSuccessStatusCode();
            string responseBody = response.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<GNews>(responseBody);
        }

        public GNews Search(string searchKeyword, int numberOfArticles)
        {
            string requestUri = $"{SEARCH_ENDPOINT}?token={API_KEY}&lang={LANGUAGE}&max={numberOfArticles}&q={searchKeyword}";
            var response = _httpClient.GetAsync(requestUri).Result;
            response.EnsureSuccessStatusCode();
            string responseBody = response.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<GNews>(responseBody);
        }

        public GNewsItem Search(DateOnly at)
        {
            var from = at.ToDateTime(TimeOnly.MinValue);
            var to = at.ToDateTime(TimeOnly.MaxValue);
            string requestUri = $"{TOP_HEADLINES_ENDPOINT}?token={API_KEY}&lang={LANGUAGE}&max={1}&from?{from.ToString("yyyy-MM-ddTHH:mm:ssZ")}&to={to.ToString("yyyy-MM-ddTHH:mm:ssZ")}";
            var response = _httpClient.GetAsync(requestUri).Result;
            response.EnsureSuccessStatusCode();
            string responseBody = response.Content.ReadAsStringAsync().Result;
            var gNews = JsonConvert.DeserializeObject<GNews>(responseBody);
            if (gNews != null && gNews.Articles != null && gNews.Articles.Count > 0)
            {
                GNewsItem gNewsItem = gNews.Articles[0];
                // Fix bug in GNewsApi. if date is in the future, it returns an article.
                if (gNewsItem.PublishedAt >= from && gNewsItem.PublishedAt <= to)
                {
                    return gNewsItem;
                }
            }
            return null;
        }
    }
}