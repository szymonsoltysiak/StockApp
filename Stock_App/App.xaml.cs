﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Stock_App.Core;
using Stock_App.Domain.Commands;
using Stock_App.Domain.Queries;
using Stock_App.EntityFramework;
using Stock_App.EntityFramework.Commands;
using Stock_App.EntityFramework.Queries;
using Stock_App.MVVM.Stores;
using Stock_App.MVVM.View;
using Stock_App.MVVM.ViewModel;
using Stock_App.Services;
using System;
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

/*        private readonly StockItemsStore _stockItemsStore;
        private readonly SelectedStockItemStore _selectedStockItemStore;
        private readonly StockAppDbContextFactory _stockAppDbContextFactory;

        private readonly ICreateStockItemCommand _createStockItemCommand;
        private readonly IGetAllStockItemsQuery _getAllStockItemsQuery;
        private readonly IUpdateStockItemCommand _updateStockItemCommand;
        private readonly IDeleteStockItemCommand _deleteStockItemCommand;*/

        public App()
        {
            IServiceCollection services = new ServiceCollection();

            string connectionString = "Data Source=StockApp.db";
            services.AddSingleton<DbContextOptions>(new DbContextOptionsBuilder().UseSqlite(connectionString).Options);
            services.AddSingleton<StockAppDbContextFactory>();

            services.AddSingleton<IGetAllStockItemsQuery, GetAllStockItemsQuery>();
            services.AddSingleton<ICreateStockItemCommand, CreateStockItemCommand>();
            services.AddSingleton<IUpdateStockItemCommand, UpdateStockItemCommand>();
            services.AddSingleton<IDeleteStockItemCommand, EntityFramework.Commands.DeleteStockItemCommand>();

            services.AddSingleton<StockItemsStore>();
            services.AddSingleton<SelectedStockItemStore>();

/*            _stockAppDbContextFactory = new StockAppDbContextFactory(
                new DbContextOptionsBuilder().UseSqlite(connectionString).Options);
            _getAllStockItemsQuery = new GetAllStockItemsQuery(_stockAppDbContextFactory);
            _createStockItemCommand = new CreateStockItemCommand(_stockAppDbContextFactory);
            _updateStockItemCommand = new UpdateStockItemCommand(_stockAppDbContextFactory);
            _deleteStockItemCommand = new DeleteStockItemCommand(_stockAppDbContextFactory);
            _stockItemsStore = new StockItemsStore(_getAllStockItemsQuery, _createStockItemCommand, _updateStockItemCommand, _deleteStockItemCommand);
            _selectedStockItemStore = new SelectedStockItemStore();*/

            services.AddSingleton<MainWindow>(provider => new MainWindow
            {
                DataContext = provider.GetRequiredService<MainViewModel>()
            });
            services.AddSingleton<MainViewModel>();
            services.AddSingleton<HomeViewModel>();
            services.AddSingleton<ProfileViewModel>();
            services.AddSingleton<PortfolioViewModel>();
            services.AddSingleton<INavigationService, NavigationService>();

            services.AddSingleton<Func<Type, ViewModel>>(serviceProvider => viewModelType => (ViewModel)serviceProvider.GetRequiredService(viewModelType));

            _serviceProvider = services.BuildServiceProvider();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            StockAppDbContextFactory _stockAppDbContextFactory = _serviceProvider.GetRequiredService<StockAppDbContextFactory>();
            using (StockAppDbContext context = _stockAppDbContextFactory.Create())
            {
                context.Database.Migrate();
            }
            /*var loginWindow = new LoginWindow(_serviceProvider);
            loginWindow.Show();*/
            var mainWindow = _serviceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();
            base.OnStartup(e);
        }

    }

}
