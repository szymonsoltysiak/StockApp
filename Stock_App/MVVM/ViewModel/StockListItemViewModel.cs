using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Stock_App.MVVM.ViewModel
{
    public class StockListItemViewModel : Core.ViewModel
    {
        public string Ticker { get; set; }
        public double Price { get; set; }
        public ICommand EditCommand { get; }
        public ICommand DeleteCommand { get; }

        public StockListItemViewModel(string ticker, double price) 
        {
            Ticker = ticker;
            Price = price;
        }
    }
}
