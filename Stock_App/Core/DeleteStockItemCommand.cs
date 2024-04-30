using Stock_App.Domain.Model;
using Stock_App.MVVM.Stores;
using Stock_App.MVVM.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock_App.Core
{
    class DeleteStockItemCommand : AsyncCommandBase
    {
        private readonly PortfolioViewModel _portfolioViewModel;
        private readonly StockItemsStore _stockItemsStore;
        private readonly SelectedStockItemStore _selectedStockItemStore;

        public DeleteStockItemCommand(PortfolioViewModel portfolioViewModel, StockItemsStore stockItemsStore, SelectedStockItemStore selectedStockItemStore)
        {
            _portfolioViewModel = portfolioViewModel;
            _stockItemsStore = stockItemsStore;
            _selectedStockItemStore = selectedStockItemStore;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _portfolioViewModel.ErrorMessage = null;
            if (_selectedStockItemStore.SelectedStockItem == null)
            {
                _portfolioViewModel.ErrorMessage = "Choose item to delete.";
                return;
            }

            Guid toDelete = _selectedStockItemStore.SelectedStockItem.Id;

            try
            {
                await _stockItemsStore.Delete(toDelete);
                _selectedStockItemStore.SelectedStockItem = null;
            }
            catch (Exception)
            {
                _portfolioViewModel.ErrorMessage = "Failed to delete stock item. Please try again later.";
            }
        }
    }
}
