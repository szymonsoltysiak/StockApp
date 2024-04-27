using Microsoft.Extensions.DependencyInjection;
using Stock_App.Core;
using Stock_App.MVVM.View;
using Stock_App.MVVM.ViewModel;
using Stock_App.Services;
using System.Configuration;
using System.Data;
using System.Windows;

namespace Stock_App
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
       private readonly ServiceProvider _serviceProvider;

       public App()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddSingleton<LoginWindow>();
            services.AddSingleton<MainWindow>(provider => new MainWindow
            {
                DataContext = provider.GetRequiredService<MainViewModel>()
            });
            services.AddSingleton<MainViewModel>();
            services.AddSingleton<HomeViewModel>();
            services.AddSingleton<ProfileViewModel>();
            services.AddSingleton<PortoflioViewModel>();
            services.AddSingleton<INavigationService, NavigationService>();

            services.AddSingleton<Func<Type, ViewModel>>(serviceProvider => viewModelType => (ViewModel)serviceProvider.GetRequiredService(viewModelType));

            _serviceProvider = services.BuildServiceProvider();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            var loginWindow = _serviceProvider.GetRequiredService<LoginWindow>();
            loginWindow.Show();
            var mainWindow = _serviceProvider.GetRequiredService<MainWindow>();
            var homeWindow = _serviceProvider.GetRequiredService<HomeViewModel>();
            mainWindow.Show();
            homeWindow.Equals(mainWindow);
            base.OnStartup(e);
        }

    }

}
