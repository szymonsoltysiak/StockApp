using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Stock_App.Services;
using YahooFinanceApi;


namespace Stock_App.MVVM.ViewModel
{


    public class HomeViewModel : Core.ViewModel, INotifyPropertyChanged
    {
        public HomeViewModel()
        {
            string apiKey = "1d1cb83956234b089fc68e32deecf989";
            NewsClient = new NewsApiClient(apiKey);
            DownloadNews();

            List<string> tickerList = new List<string>() { "MSFT", "GOOG", "NVDA", "TSLA", "AAPL" };
            Stocks = new PopularStocks();
            DownloadStock(tickerList);

            ChartDataProvider = new ChartData();
            string ticker = "AAPL";
            DateTime start = DateTime.Now.AddDays(-31);
            DateTime end = DateTime.Now.AddDays(-1);
            DownloadChartData(ticker, start, end);
        }

        private PopularStocks _stocks;
        private ChartData _chartDataProvider;
        private NewsApiClient _newsClient;

        public async void DownloadStock(List<string> tickerList)
        {
            await Stocks.Fill(tickerList);
        }

        public async void DownloadChartData(string ticker, DateTime start, DateTime end)
        {
            await ChartDataProvider.Fill(ticker, start, end);
        }

        public async void DownloadNews()
        {
            await NewsClient.GetTopHeadlinesAsync();
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

        public ChartData ChartDataProvider
        {
            get { return _chartDataProvider; }
            set
            {
                _chartDataProvider = value;
                OnPropertyChanged();
            }
        }

        public NewsApiClient NewsClient
        {
            get { return _newsClient; }
            set
            {
                _newsClient = value;
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
