using Stock_App.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock_App.MVVM.Stores
{
    public class SelectedStockItemStore
    {
        private readonly StockItemsStore _stockItemsStore;

        private StockItem _selectedStockItem;
        public StockItem SelectedStockItem
        {
            get { return _selectedStockItem; }
            set
            {
                _selectedStockItem = value;
                SelectedStockItemChanged?.Invoke();

            }
        }

        public event Action SelectedStockItemChanged;

        public SelectedStockItemStore(StockItemsStore stockItemsStore)
        {
            _stockItemsStore = stockItemsStore;

            _stockItemsStore.StockItemAdded += StockItemsStore_StockItemAdded;
            _stockItemsStore.StockItemUpdated += StockItemsStore_StockItemUpdated;
        }

        private void StockItemsStore_StockItemAdded(StockItem stockItem)
        {
            SelectedStockItem = stockItem;
        }

        private void StockItemsStore_StockItemUpdated(StockItem stockItem)
        {
            if (stockItem.Id == SelectedStockItem?.Id)
            {
                SelectedStockItem = stockItem;
            }
        }

    }
}
