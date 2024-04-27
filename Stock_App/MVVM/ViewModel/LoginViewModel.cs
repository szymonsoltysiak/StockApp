using Microsoft.Extensions.DependencyInjection;
using Stock_App.Core;
using Stock_App.MVVM.View;
using Stock_App.Services;
using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Stock_App.MVVM.ViewModel
{
    public class LoginViewModel : Core.ViewModel
    {
        private int clickCount;

        public int ClickCount
        {
            get { return clickCount; }
            set { 
                clickCount = value;
                OnPropertyChanged();
            } 
        }

        public LoginViewModel(ServiceProvider _serviceProvider)
        {
            serviceProvider = _serviceProvider;
            clickCount = 0;
        }

        public RelayCommand LoggingInCommand => new RelayCommand(execute:o => LoggingIn(), canExecute:o => { return true; });

        public void LoggingIn()
        {
            var mainWindow = serviceProvider.GetRequiredService<MainWindow>();
            var homeWindow = serviceProvider.GetRequiredService<HomeViewModel>();
            mainWindow.Show();
        }

        private readonly ServiceProvider serviceProvider;
    }

}
