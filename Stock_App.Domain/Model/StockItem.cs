using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Stock_App.Domain.Model
{
    public class StockItem
    {
        public string Ticker { get; set; }
        public double Price { get; set; }

        public StockItem(string ticker, double price)
        {
            Ticker = ticker;
            Price = price;
        }
    }
}
