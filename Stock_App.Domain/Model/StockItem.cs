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
        public Guid Id { get; }
        public string Ticker { get; }
        public double Price { get; }

        public StockItem(Guid id, string ticker, double price)
        {
            Id = id;
            Ticker = ticker;
            Price = price;
        }
    }
}
