using Stock_App.Domain.Model;
using Stock_App.MVVM.Stores;
using Stock_App.MVVM.View;
using Stock_App.MVVM.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock_App.Core
{
    public class AddStockItemCommand : AsyncCommandBase
    {
        private readonly PortfolioViewModel _portfolioViewModel;
        private readonly StockItemsStore _stockItemsStore;

        public AddStockItemCommand(PortfolioViewModel portfolioViewModel,StockItemsStore stockItemsStore)
        {
            _portfolioViewModel = portfolioViewModel;
            _stockItemsStore = stockItemsStore;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _portfolioViewModel.ErrorMessage = null;

            StockItem stockItem = new StockItem(
            _portfolioViewModel.Ticker,
            _portfolioViewModel.Price);

            try
            {
                await _stockItemsStore.Add(stockItem);
            }
            catch (Exception)
            {
                _portfolioViewModel.ErrorMessage = "Failed to add stock item. Please try again later.";
            }
            /*finally
            {
                ///.IsSubmitting = false;
            }*/
        }
    }
}
