using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ModelLayer.DbContexts;
using ModelLayer.Mapping;
using ModelLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
//using ViewModelLayer.Services;
using ViewModelLayer.Stores;
using ViewModelLayer.ViewModels;

namespace ViewLayer
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly NavigationStore _navigationStore;
        private readonly string _connectionString = "Data Source=users.db"; // TODO delete it from code

        private readonly IHost _host;

        public App()
        {
            //_navigationStore = new NavigationStore();
            _host = Host.CreateDefaultBuilder().ConfigureServices(services =>
            {
                services.AddAutoMapper(typeof(MappingProfile));
                services.AddSingleton<IUsersDbContextFactory>(s => new UsersDbContextFactory(_connectionString));
                services.AddSingleton<IUsersRepository, UsersRepository>();
                services.AddSingleton<NavigationStore>();
                services.AddSingleton<UsersStore>();

                //services.AddSingleton<NavigationService<LoadUsersViewModel>>();
                //services.AddSingleton<NavigationService<ListUsersViewModel>>();
                //services.AddSingleton<NavigationService<EditUserViewModel>>();

                services.AddTransient<LoadUsersViewModel>(s => new LoadUsersViewModel(
                    s.GetRequiredService<NavigationStore>(), 
                    s.GetRequiredService<UsersStore>(),
                    s.GetRequiredService<IUsersRepository>()));
                services.AddTransient<ListUsersViewModel>(s => new ListUsersViewModel(
                    s.GetRequiredService<NavigationStore>(),
                    s.GetRequiredService<UsersStore>()));
                services.AddTransient<EditUserViewModel>(s => new EditUserViewModel(
                    s.GetRequiredService<UsersStore>()));

                //services.AddTransient(s => 
                //    new LoadUsersViewModel(s.GetRequiredService<NavigationService<ListUsersViewModel>>()));
                //services.AddSingleton<Func<LoadUsersViewModel>>(s => () => s.GetRequiredService<LoadUsersViewModel>());
                //services.AddSingleton<Func<ListUsersViewModel>>(s => () => s.GetRequiredService<ListUsersViewModel>());

                services.AddSingleton<MainViewModel>();
                services.AddSingleton(s => new MainWindow()
                {
                    DataContext = s.GetRequiredService<MainViewModel>()
                });
            }).Build();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            _host.Start();

            //DbContextOptions options = new DbContextOptionsBuilder().UseSqlite(_connectionString).Options;
            //UsersDbContext usersDbContext = new UsersDbContext(options);
            //usersDbContext.Database.Migrate();
            IUsersDbContextFactory usersDbContextFactory = _host.Services.GetRequiredService<IUsersDbContextFactory>();
            using(UsersDbContext dbContext = usersDbContextFactory.CreateDbContext())
            {
                dbContext.Database.Migrate();
            }

            NavigationStore navigationStore = _host.Services.GetRequiredService<NavigationStore>();
            //UsersStore usersStore = _host.Services.GetRequiredService<UsersStore>();
            navigationStore.CurrentViewModel = _host.Services.GetRequiredService<LoadUsersViewModel>();
            //NavigationService<LoadUsersViewModel> navigationService = _host.Services.GetRequiredService<NavigationService<LoadUsersViewModel>>();
            //navigationService.Navigate();

            MainWindow = _host.Services.GetRequiredService<MainWindow>();

            //_navigationStore.CurrentViewModel = new LoadUsersViewModel(_navigationStore);
            //MainWindow = new MainWindow()
            //{
            //    DataContext = new MainViewModel(_navigationStore)
            //};
            MainWindow.Show();
            base.OnStartup(e);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            _host.Dispose();
            base.OnExit(e);
        }
    }
}
