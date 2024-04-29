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
		private StockItem _selectedStockItem;

		public StockItem SelectedStockItem
        {
			get { return _selectedStockItem; }
			set 
			{
				SelectedStockItem = value;
				SelectedStockItemChanged?.Invoke();

            }
		}

		public event Action SelectedStockItemChanged;

	}
}
