using Stock_App.MVVM.Stores;
using Stock_App.MVVM.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace Stock_App.Core
{
    public class LoadStockItemsCommand : AsyncCommandBase
    {
        private readonly PortfolioViewModel _portfolioViewModel;
        private readonly StockItemsStore _stockItemsStore;

        public LoadStockItemsCommand(PortfolioViewModel portfolioViewModel, StockItemsStore stockItemsStore)
        {
            _stockItemsStore = stockItemsStore;
            _portfolioViewModel = portfolioViewModel;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            try
            {
                await _stockItemsStore.Load();
            }
            catch (Exception)
            {
                _portfolioViewModel.ErrorMessage = "Failed to load stock items. Please try again later.";
            }
        }
    }
}
