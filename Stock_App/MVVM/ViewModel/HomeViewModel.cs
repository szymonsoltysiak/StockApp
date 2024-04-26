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
            ArticleList.Add(new ArticleNews("Tytul ", " Autor ", " Opis"));
            /*NewsProvider.Fill();
            foreach (ArticleNews article in NewsProvider.NewsList)
            {
                ArticleList.Add(new ArticleNews(article.Title, article.Author, article.Description));
            }*/
            StockList = new List<Stock>();
            Stocks = new PopularStocks();
            List<string> tickerList = new List<string>() { "AAPL", "GOOG", "NVDA" };
            Stocks.Fill(tickerList);
            StockList.Add(new Stock("AAPL", 12.6, 2.3, true));
            /*foreach (Stock stock in Stocks.PopularStockList)
            {
                StockList.Add(new Stock(stock.Ticker, stock.Price, stock.Procent, stock.IsUp));
            }*/
        }

        private List<ArticleNews> _articleList;
        private News _newsProvider;
        private List<Stock> _stockList;
        private PopularStocks _stocks;

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
