using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using NewsAPI.Models;
using Stock_App.Services;


namespace Stock_App.MVVM.ViewModel
{


    public class HomeViewModel : Core.ViewModel, INotifyPropertyChanged
    {
        public HomeViewModel() 
        {
            ArticleList = new List<ArticleNews>();
            NewsProvider = new News();
            //DownloadNews();
            List<string> tickerList = new List<string>() { "MSFT", "GOOG", "NVDA", "TSLA", "AAPL"};
            StockList = new List<Stock>();
            Stocks = new PopularStocks();
            DownloadStock(tickerList);
        }

        private List<ArticleNews> _articleList;
        private News _newsProvider;
        private List<Stock> _stockList;
        private PopularStocks _stocks;

        public async void DownloadStock(List<string> tickerList)
        {
            await Stocks.Fill(tickerList);
            foreach (Stock stock in Stocks.PopularStockList)
            {
                StockList.Add(new Stock(stock.Ticker, Math.Round(stock.Price, 2), Math.Round(stock.Procent,2), stock.IsUp));
            }
        }

        public void DownloadNews()
        {
            ArticleList.Add(new ArticleNews("Tytul ", " Autor ", " Opis"));
            NewsProvider.Fill();
            foreach (ArticleNews article in NewsProvider.NewsList)
            {
                ArticleList.Add(new ArticleNews(article.Title, article.Author, article.Description));
            }
        }

        public List<ArticleNews> ArticleList
        {
            get { return _articleList; }
            set
            {
                _articleList = value;
                OnPropertyChanged();
            }
        }

        public News NewsProvider
        {
            get { return _newsProvider; }
            set
            {
                _newsProvider = value;
                OnPropertyChanged();
            }
        }

        public List<Stock> StockList
        {
            get { return _stockList; }
            set
            {
                _stockList = value;
                OnPropertyChanged();
            }
        }

        public PopularStocks Stocks
        {
            get { return _stocks; }
            set
            {
                _stocks = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
