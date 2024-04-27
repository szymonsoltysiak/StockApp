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
        private readonly ObservableCollection<StockListItemViewModel> _stockListItemVM;
        public IEnumerable<StockListItemViewModel> StockListItemVM => _stockListItemVM;

        public PortoflioViewModel() 
        {
            _stockListItemVM = new ObservableCollection<StockListItemViewModel>();
            _stockListItemVM.Add(new StockListItemViewModel("AAPL", 12.36));
            _stockListItemVM.Add(new StockListItemViewModel("MSFT", 22.17));
            _stockListItemVM.Add(new StockListItemViewModel("NVDA", 112.36));
        }
    }
}
