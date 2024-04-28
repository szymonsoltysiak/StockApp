using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Stock_App.Core;
using Stock_App.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Stock_App.MVVM.ViewModel
{

    public class PortoflioViewModel : Core.ViewModel
    {
        private readonly ObservableCollection<StockItem> _stockItemList;
        public IEnumerable<StockItem> StockItemList => _stockItemList;
        

        public PortoflioViewModel()
        {
            _stockItemList = new ObservableCollection<StockItem>();
            _stockItemList.Add(new StockItem("AAPL", 12.36));
            _stockItemList.Add(new StockItem("MSFT", 22.17));
            _stockItemList.Add(new StockItem("NVDA", 112.36));
        }

        private StockItem selectedStockItem;
        public StockItem SelectedStockItem
        {
            get { return selectedStockItem; }
            set 
            { 
                selectedStockItem = value;
                OnPropertyChanged();
            }
        }
        private string ticker;
        public string Ticker
        {
            get { return ticker; }
            set
            {
                if (!string.Equals(ticker, value))
                {
                    ticker = value;
                    OnPropertyChanged();
                }
            }
        }

        public static bool IsDoubleRealNumber(string valueToTest)
        {
            if (double.TryParse(valueToTest, out double d) && !Double.IsNaN(d) && !Double.IsInfinity(d))
            {
                return true;
            }

            return false;
        }

        private string priceString;
        public string PriceString
        {
            get { return priceString; }
            set
            {
                if (!string.Equals(priceString, value))
                {
                    priceString = value;
                    OnPropertyChanged();
                }
            }
        }

        public double Price { get; set; }

        public RelayCommand AddStockItemCommand => new RelayCommand(execute:o => AddStockItem(), canExecute:o => {return true; });
        public RelayCommand DeleteStockItemCommand => new RelayCommand(execute: o => DeleteStockItem(), canExecute: o => SelectedStockItem != null );
        public RelayCommand EditStockItemCommand => new RelayCommand(execute: o => EditStockItem(), canExecute: o => SelectedStockItem != null && IsDoubleRealNumber(PriceString) && !String.IsNullOrEmpty(Ticker));
        public void AddStockItem()
        {
            if (IsDoubleRealNumber(PriceString) && !String.IsNullOrEmpty(Ticker))
            {
                Price = double.Parse(PriceString);
                _stockItemList.Add(new StockItem(Ticker, Price));
            }
        }
        private void DeleteStockItem()
        {
            _stockItemList.Remove(SelectedStockItem);
        }

        private void EditStockItem()
        {
            Price = double.Parse(PriceString);
            /*SelectedStockItem.Ticker = Ticker;
            SelectedStockItem.Price = Price;*/
            /*_stockItemList.(_stockItemList.IndexOf(selectedStockItem), new StockItem(Ticker, Price));*/
            _stockItemList[_stockItemList.IndexOf(selectedStockItem)] = new StockItem(Ticker, Price);
        }
    }
}
