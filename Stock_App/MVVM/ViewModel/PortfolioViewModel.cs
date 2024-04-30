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
using System.Collections.Specialized;



namespace Stock_App.MVVM.ViewModel
{

    public class PortfolioViewModel : Core.ViewModel
    {
        private readonly StockItemsStore _stockItemsStore;
        private readonly SelectedStockItemStore _selectedStockItemStore;

        private readonly ObservableCollection<StockItem> _stockItemsList;
        public IEnumerable<StockItem> StockItemsList => _stockItemsList;

        public StockItem SelectedStockItem
        {
            get
            {
                return _stockItemsList.FirstOrDefault(x => x?.Id == _selectedStockItemStore.SelectedStockItem?.Id);
            }
            set
            {
                _selectedStockItemStore.SelectedStockItem = value;
                OnPropertyChanged();
            }
        }
        public ICommand LoadStockItemsCommand { get; }
        public ICommand AddStockItemCommand { get; }
        public ICommand EditStockItemCommand { get; }
        public ICommand DeleteStockItemCommand { get; }

        public PortfolioViewModel(StockItemsStore stockItemsStore, SelectedStockItemStore selectedStockItemStore)
        {
            TotalSum = 0;

            _stockItemsStore = stockItemsStore;
            _selectedStockItemStore = selectedStockItemStore;
            _stockItemsList = new ObservableCollection<StockItem>();

            _selectedStockItemStore.SelectedStockItemChanged += SelectedStockItemStore_SelectedStockItemChanged;

            _stockItemsStore.StockItemsLoaded += StockItemsStore_StockItemsLoaded;
            _stockItemsStore.StockItemAdded += StockItemsStore_StockItemAdded;
            _stockItemsStore.StockItemUpdated += StockItemsStore_StockItemUpdated;
            _stockItemsStore.StockItemDeleted += StockItemsStore_StockItemDeleted;

            _stockItemsList.CollectionChanged += StockItemsList_CollectionChanged;

            LoadStockItemsCommand = new LoadStockItemsCommand(this, stockItemsStore);
            AddStockItemCommand = new AddStockItemCommand(this, stockItemsStore);
            DeleteStockItemCommand = new Core.DeleteStockItemCommand(this, stockItemsStore, selectedStockItemStore);
            EditStockItemCommand = new EditStockItemCommand(this, stockItemsStore, selectedStockItemStore);

            LoadStockItemsCommand.Execute(null);
        }

        private void SelectedStockItemStore_SelectedStockItemChanged()
        {
            OnPropertyChanged(nameof(SelectedStockItem));
        }

        private void StockItemsStore_StockItemsLoaded()
        {
            _stockItemsList.Clear();

            foreach (StockItem stockItem in _stockItemsStore.StockItems)
            {
                AddStockItem(stockItem);
            }
        }

        private void StockItemsStore_StockItemAdded(StockItem stockItem)
        {
            AddStockItem(stockItem);
        }

        private void StockItemsStore_StockItemUpdated(StockItem stockItem)
        {
            StockItem foundStockItem =
                _stockItemsList.FirstOrDefault(x => x?.Id == stockItem.Id);

            if (foundStockItem != null)
            {
                totalSum -= foundStockItem.Price;
                TotalSum += stockItem.Price;
                _stockItemsList[_stockItemsList.IndexOf(foundStockItem)] = stockItem;
                OnPropertyChanged();
            }
        }

        private void StockItemsStore_StockItemDeleted(Guid id)
        {
            StockItem stockItem = _stockItemsList.FirstOrDefault(x => x?.Id == id);

            if (stockItem != null)
            {
                _stockItemsList.Remove(stockItem);
                TotalSum -= stockItem.Price;
            }
        }

        private void StockItemsList_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged(nameof(SelectedStockItem));
        }

        private void AddStockItem(StockItem stockItem)
        {
            _stockItemsList.Add(stockItem);
            TotalSum += stockItem.Price;
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
        public RelayCommand ExportPortfolioCommand => new RelayCommand(execute: o => ExportPortfolio(), canExecute: o => StockItemsList != null && StockItemsList.Any());
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

                foreach (StockItem stockItem in StockItemsList)
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

        private string _errorMessage;
        public string ErrorMessage
        {
            get
            {
                return _errorMessage;
            }
            set
            {
                _errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
                ///OnPropertyChanged(nameof(HasErrorMessage));
            }
        }

    }
}
