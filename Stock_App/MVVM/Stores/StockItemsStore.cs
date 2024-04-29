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

        public StockItemsStore(IGetAllStockItemsQuery getAllStockItemsQuery,
            ICreateStockItemCommand createStockItemCommand,
            IUpdateStockItemCommand updateStockItemCommand,
            IDeleteStockItemCommand deleteStockItemCommand)
        {
            _createStockItemCommand = createStockItemCommand;
            _getAllStockItemsQuery = getAllStockItemsQuery;
            _updateStockItemCommand = updateStockItemCommand;
            _deleteStockItemCommand = deleteStockItemCommand;
        }

        public event Action<StockItem> StockItemAdded;
        public event Action<StockItem> StockItemUpdated;

        public async Task Add(StockItem stockItem)
        {
            await _createStockItemCommand.Execute(stockItem);
            StockItemAdded?.Invoke(stockItem);
        }

        public async Task Update(StockItem stockItem)
        {
            await _updateStockItemCommand.Execute(stockItem);
            StockItemUpdated?.Invoke(stockItem);
        }
    }
}
