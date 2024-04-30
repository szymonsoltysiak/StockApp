using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Stock_App.Core;
using Stock_App.MVVM.Model;
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



namespace Stock_App.MVVM.ViewModel
{

    public class PortoflioViewModel : Core.ViewModel
    {
        StockDB context;
        private readonly ObservableCollection<StockItemDB> _stockItemList;
        public IEnumerable<StockItemDB> StockItemList => _stockItemList;



        public PortoflioViewModel()
        {
            context = new StockDB();
            _stockItemList = new ObservableCollection<StockItemDB>();

            foreach(StockItemDB stock in context.Stocks)
            {
                _stockItemList.Add(stock);
            }

            TotalSum = 0;
            foreach (StockItemDB stockItem in context.Stocks) 
            {
                TotalSum += stockItem.Price;
            }
        }

        private StockItemDB selectedStockItem;
        public StockItemDB SelectedStockItem
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
                StockItemDB newstock = new StockItemDB(Ticker, Price);
                context.Stocks.Add(newstock);
                context.SaveChanges();
                _stockItemList.Add(newstock);
                TotalSum += Price;
            }
        }
        private void DeleteStockItem()
        {
            TotalSum -= SelectedStockItem.Price;
            context.Stocks.Remove(SelectedStockItem);
            context.SaveChanges();
            _stockItemList.Remove(SelectedStockItem);
        }

        private void EditStockItem()
        {
            TotalSum -= SelectedStockItem.Price;
            Price = double.Parse(PriceString);
            TotalSum += Price;
            var stockitem = context.Stocks.First(x => x.ID == SelectedStockItem.ID);
            stockitem.Ticker= Ticker;
            stockitem.Price= Price;
            context.SaveChanges();
            _stockItemList[_stockItemList.IndexOf(selectedStockItem)].Ticker = Ticker;
            _stockItemList[_stockItemList.IndexOf(selectedStockItem)].Price = Price;
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

                foreach (StockItemDB stockItem in StockItemList)
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
