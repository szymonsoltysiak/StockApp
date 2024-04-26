using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YahooFinanceApi;
using LiveCharts;


namespace Stock_App.Services
{
    public class Stock
    {
        public string Ticker { get; set; }
        public double Price { get; set; }
        public double Procent { get; set; }
        public bool IsUp { get; set; }
        public Stock()
        {
            Ticker = string.Empty;
            Price = 0;
            Procent = 0;
            IsUp = false;
        }
        public Stock(string ticker, double price, double procent, bool isUp)
        {
            Ticker = ticker;
            Price = price;
            Procent = procent;
            IsUp = isUp;
        }
    }

    public class ChartData
    {
        public string Ticker { get; set; }
        public ChartValues<double> Values { get; set; }
        DateTime Start { get; set; }
        DateTime End { get; set; }
        public string[] Labels { get; set; }

        public ChartData()
        {
            Ticker = string.Empty;
            Values = new ChartValues<double>();
            Start = DateTime.Now.AddDays(-31);
            End = DateTime.Now.AddDays(-1);
            Labels = [];
        }

        public async Task Fill(string ticker, DateTime start, DateTime end)
        {
            Ticker = ticker;
            Start = start;
            End = end;
            Values.Clear();
            List<string> labels = new List<string>();

            var history = await Yahoo.GetHistoricalAsync(ticker, start, end, Period.Daily);


            foreach (var candle in history)
            {
                Values.Add(decimal.ToDouble(candle.Close));
                labels.Add(candle.DateTime.ToString("dd-MM-yyyy"));
            }
            Labels = labels.ToArray();
        }

    }

    public class PopularStocks
    {
        public List<Stock> PopularStockList { get; set; }
        public PopularStocks()
        {
            PopularStockList = new List<Stock>();
        }

        public async Task Fill(List<string> tickerList)
        {
            foreach (string ticker in tickerList)
            {
                var securities = await Yahoo.Symbols(ticker).Fields(Field.Symbol, Field.RegularMarketPrice, Field.RegularMarketChange).QueryAsync();
                var stock = securities[ticker];
                double price = stock[Field.RegularMarketPrice];
                double procent = stock[Field.RegularMarketChange] * 100 / stock[Field.RegularMarketPrice];
                bool isup = stock[Field.RegularMarketChange] >= 0;
                PopularStockList.Add(new Stock(ticker, price, procent, isup));
            }
        }
    }
}
