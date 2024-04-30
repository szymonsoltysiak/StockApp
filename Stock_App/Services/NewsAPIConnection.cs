using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Stock_App.Services;
using System.Net.Http;


namespace Stock_App.Services
{
    public class NewsApiResponse
    {
        public string Status { get; set; }
        public int TotalResults { get; set; }
        public Article[] Articles { get; set; }
    }

    public class Article
    {
        public Source Source { get; set; }
        public string Author { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public string UrlToImage { get; set; }
        public DateTime PublishedAt { get; set; }
        public string Content { get; set; }
    }

    public class Source
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }

    public class NewsApiClient
    {
        public string ApiKey;
        public NewsApiResponse Response { get; set; }

        public NewsApiClient(string apiKey)
        {
            ApiKey = apiKey;
            Response = new NewsApiResponse();
        }
        bool isNotRemoved(Article article)
        {
            return article.Title != "[Removed]";
        }

        public async Task GetTopHeadlinesAsync()
        {
            string apiKey = ApiKey;

            string apiUrl = $"https://newsapi.org/v2/top-headlines?country=us&apiKey={apiKey}";

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("User-Agent", "C# NewsAPI Client");

                try
                {
                    HttpResponseMessage response = await client.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        string jsonResponse = await response.Content.ReadAsStringAsync();

                        Response = JsonConvert.DeserializeObject<NewsApiResponse>(jsonResponse);
                        Response.Articles = Array.FindAll(Response.Articles, isNotRemoved).ToArray();
                    }
                    else
                    {
                        string errorMessage = await response.Content.ReadAsStringAsync();
                        Response.Status = errorMessage;
                    }
                }
                catch (Exception ex)
                {
                    Response.Status=ex.Message;
                }
            }

        }
    }

}
