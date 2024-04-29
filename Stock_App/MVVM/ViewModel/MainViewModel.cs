using Stock_App.Core;
using Stock_App.MVVM.Stores;
using Stock_App.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Stock_App.MVVM.ViewModel
{
    public class MainViewModel : Core.ViewModel
    {
        private readonly SelectedStockItemStore _selectedStockItemStore;

        public INavigationService _navigation;

        public INavigationService Navigation
        {
            get => _navigation;
            set 
            {
                _navigation = value;
                OnPropertyChanged();
            }
        }
        public RelayCommand NavigateToHomeCommand { get; set; }
        public RelayCommand NavigateToProfileCommand { get; set; }
        public RelayCommand NavigateToPortfolioCommand { get; set; }

        public MainViewModel(INavigationService navService)
        {
            Navigation = navService;
            _selectedStockItemStore = new SelectedStockItemStore();
            NavigateToHomeCommand = new RelayCommand(execute:o => { Navigation.NavigateTo<HomeViewModel>(); }, canExecute:o => true);
            NavigateToProfileCommand = new RelayCommand(execute:o => { Navigation.NavigateTo<ProfileViewModel>(); }, canExecute:o => true);
            NavigateToPortfolioCommand = new RelayCommand(execute:o => { Navigation.NavigateTo<PortoflioViewModel>(); }, canExecute:o => true);
            Navigation.NavigateTo<PortoflioViewModel>();
        }
    }   
}
