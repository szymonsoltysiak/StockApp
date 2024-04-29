using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock_App.MVVM.Stores
{
    public class ModalNavigationStore
    {
		private Core.ViewModel _currentViewModel;

		public Core.ViewModel CurrentViewModel
        {
			get { return _currentViewModel; }
			set
			{
				_currentViewModel = value;
				CurrentViewModelChanged?.Invoke();

            }
		}

		public event Action CurrentViewModelChanged;
	}
}
