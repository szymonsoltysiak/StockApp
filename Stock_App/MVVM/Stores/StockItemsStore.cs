using Stock_App.Domain.Commands;
using Stock_App.Domain.Model;
using Stock_App.Domain.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock_App.MVVM.Stores
{
    public class StockItemsStore
    {
        private readonly ICreateStockItemCommand _createStockItemCommand;
        private readonly IGetAllStockItemsQuery _getAllStockItemsQuery;
        private readonly IUpdateStockItemCommand _updateStockItemCommand;
        private readonly IDeleteStockItemCommand _deleteStockItemCommand;

        private readonly List<StockItem> _stockItems;
        public IEnumerable<StockItem> StockItems => _stockItems;

        public event Action StockItemsLoaded;
        public event Action<StockItem> StockItemAdded;
        public event Action<StockItem> StockItemUpdated;
        public event Action<String> StockItemDeleted;

        public StockItemsStore(IGetAllStockItemsQuery getAllStockItemsQuery,
            ICreateStockItemCommand createStockItemCommand,
            IUpdateStockItemCommand updateStockItemCommand,
            IDeleteStockItemCommand deleteStockItemCommand)
        {
            _createStockItemCommand = createStockItemCommand;
            _getAllStockItemsQuery = getAllStockItemsQuery;
            _updateStockItemCommand = updateStockItemCommand;
            _deleteStockItemCommand = deleteStockItemCommand;

            _stockItems = new List<StockItem>();
        }

        public async Task Load() 
        {
            IEnumerable<StockItem> stockItems = await _getAllStockItemsQuery.Execute();
            _stockItems.Clear();
            _stockItems.AddRange(stockItems);
            StockItemsLoaded?.Invoke();
        }

        public async Task Add(StockItem stockItem)
        {
            await _createStockItemCommand.Execute(stockItem);
            _stockItems.Add(stockItem);
            StockItemAdded?.Invoke(stockItem);
        }

        public async Task Update(StockItem stockItem)
        {
            await _updateStockItemCommand.Execute(stockItem);
            int foundIndex = _stockItems.FindIndex(x => x?.Ticker == stockItem.Ticker);
            
            if (foundIndex != -1)
            {
                _stockItems[foundIndex] = stockItem;
            } 
            else
            {
                _stockItems.Add(stockItem);
            }
                
            StockItemUpdated?.Invoke(stockItem);
        }

        public async Task Delete(string ticker)
        {
            await _deleteStockItemCommand.Execute(ticker);

            _stockItems.RemoveAll(x => x.Ticker == ticker);

            StockItemDeleted?.Invoke(ticker);
        }
    }
}
