using Stock_App.Core;
using Stock_App.MVVM.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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


        public RelayCommand AddStockItemCommand => new RelayCommand(execute:o => AddStockItem(), canExecute:o => {return true; });
        public RelayCommand DeleteStockItemCommand => new RelayCommand(execute: o => DeleteStockItem(), canExecute: o => SelectedStockItem != null );
        public void AddStockItem()
        {
            _stockItemList.Add(new StockItem ( "XXXX", 99.99 ));
        }
        private void DeleteStockItem()
        {
            _stockItemList.Remove(SelectedStockItem);
        }
    }
}
