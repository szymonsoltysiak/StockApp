using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YahooFinanceApi;

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

        /*public void Fill(List<string> tickerList)
        {
            foreach (string ticker in tickerList)
            {
                var task = FillOne(ticker);
                task.Wait();
            }
        }*/

    }
}
