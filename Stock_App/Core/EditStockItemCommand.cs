using Stock_App.Domain.Model;
using Stock_App.MVVM.Stores;
using Stock_App.MVVM.ViewModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock_App.Core
{
    public class EditStockItemCommand : AsyncCommandBase
    {
        private readonly PortfolioViewModel _portfolioViewModel;
        private readonly StockItemsStore _stockItemsStore;
        private readonly SelectedStockItemStore _selectedStockItemStore;

        public EditStockItemCommand(PortfolioViewModel portfolioViewModel, StockItemsStore stockItemsStore, SelectedStockItemStore selectedStockItemStore)
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
                _portfolioViewModel.ErrorMessage = "Choose item to update.";
                return;
            }
            ///formViewModel.IsSubmitting = true;
            double price = double.Parse(_portfolioViewModel.PriceString);

            StockItem stockItem = new StockItem(
                _selectedStockItemStore.SelectedStockItem.Id,
            _portfolioViewModel.Ticker,
            price);

            try
            {
                await _stockItemsStore.Update(stockItem);
            }
            catch (Exception)
            {
                _portfolioViewModel.ErrorMessage = "Failed to update stock item. Please try again later.";
            }
        }
    }
}
