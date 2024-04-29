using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Stock_App.Core;
using Stock_App.Domain.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.IO;
using Microsoft.Win32;
using Stock_App.Domain.Queries;
using Stock_App.Domain.Commands;
using Stock_App.MVVM.Stores;
using System.Windows.Input;
using Stock_App.EntityFramework.Commands;
using Stock_App.EntityFramework.Queries;
using Stock_App.EntityFramework;
using Microsoft.EntityFrameworkCore;



namespace Stock_App.MVVM.ViewModel
{

    public class PortoflioViewModel : Core.ViewModel
    {
        public PortoflioViewModel(StockItemsStore stockItemStore, SelectedStockItemStore selectedStockItemStore)
        {
            _stockItemList = new ObservableCollection<StockItem>();
            _stockItemList.Add(new StockItem("AAPL", 12.36));
            _stockItemList.Add(new StockItem("MSFT", 22.17));
            _stockItemList.Add(new StockItem("NVDA", 112.36));
            TotalSum = 0;
            foreach (StockItem stockItem in _stockItemList)
            {
                TotalSum += stockItem.Price;
            }
        }

        private readonly ObservableCollection<StockItem> _stockItemList;
        public IEnumerable<StockItem> StockItemList => _stockItemList;

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
        public RelayCommand ExportPortfolioCommand => new RelayCommand(execute: o => ExportPortfolio(), canExecute: o => StockItemList != null && StockItemList.Any());
        public void AddStockItem()
        {
            if (IsDoubleRealNumber(PriceString) && !String.IsNullOrEmpty(Ticker))
            {
                Price = double.Parse(PriceString);
                _stockItemList.Add(new StockItem(Ticker, Price));
                TotalSum += Price;
            }
        }
        private void DeleteStockItem()
        {
            TotalSum -= SelectedStockItem.Price;
            _stockItemList.Remove(SelectedStockItem);
        }

        private void EditStockItem()
        {
            TotalSum-=SelectedStockItem.Price;
            Price = double.Parse(PriceString);
            TotalSum+= Price;
            _stockItemList[_stockItemList.IndexOf(selectedStockItem)] = new StockItem(Ticker, Price);
        }

        private void ExportPortfolio()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            string filter = "CSV file (*.csv)|*.csv";
            saveFileDialog.Filter = filter;
            saveFileDialog.Title = "Save Portoflio in csv";
            saveFileDialog.ShowDialog();

            if (saveFileDialog.FileName != "")
            {
                filter = saveFileDialog.FileName;
                StreamWriter writer = new StreamWriter(filter);
                writer.WriteLine("Ticker,Price");

                foreach (StockItem stockItem in StockItemList)
                {
                    writer.WriteLine($"{stockItem.Ticker},{stockItem.Price.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture)}");
                }
                writer.WriteLine($"Total Sum,{TotalSum.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture)}");
                writer.Close();
            }
        }

        private double totalSum;

        public double TotalSum
        {
            get { return totalSum; }
            set 
            {
                totalSum = value;
                OnPropertyChanged();
            }
        }

    }
}
