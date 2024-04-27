using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Stock_App.MVVM.Model
{
    public class StockItem : Core.ObesrvableObject
    {
        public string Ticker { get; set; }
        public double Price { get; set; }
        public ICommand EditCommand { get; }
        public ICommand DeleteCommand { get; }

        public StockItem(string ticker, double price)
        {
            Ticker = ticker;
            Price = price;
        }
    }
}
