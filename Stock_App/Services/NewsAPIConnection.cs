using NewsAPI.Constants;
using NewsAPI.Models;
using NewsAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock_App.Services
{
    public class ArticleNews
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public ArticleNews()
        {
            Title = string.Empty;
            Author = string.Empty;
            Description = string.Empty;
        }
        public ArticleNews(string title, string author, string description)
        {
            Title = title;
            Author = author;
            Description = description;
        }
    }

    public class News
    {
        public List<ArticleNews> NewsList { get; set; }

        public News()
        {
            NewsList = new List<ArticleNews>();
        }


        public void Fill()
        {
            var newsApiClient = new NewsApiClient("1d1cb83956234b089fc68e32deecf989");
            var articlesResponse = newsApiClient.GetEverything(new EverythingRequest
            {
                Q = "Stock Market",
                SortBy = SortBys.Popularity,
                Language = Languages.EN,
                From = DateTime.Now.AddDays(-2)
            });

            if (articlesResponse.Status == Statuses.Ok)
            {
                int numResults = articlesResponse.TotalResults;

                for (int i = 0; i < 3 && i < numResults; i++)
                {
                    {
                        NewsList.Add(new ArticleNews(articlesResponse.Articles[i].Title, articlesResponse.Articles[i].Author, articlesResponse.Articles[i].Description));
                    }
                }
            }
        }
    }
}
