using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ModelLayer.DbContexts;
using ModelLayer.Mapping;
using ModelLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using ViewModelLayer.Services;
using ViewModelLayer.Stores;
using ViewModelLayer.ViewModels;

namespace ViewLayer
{
    public partial class App : Application
    {
        private readonly NavigationStore _navigationStore;

        private readonly IHost _host;

        public App()
        {
            _host = Host.CreateDefaultBuilder()
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    var settingPath = Path.GetFullPath(Path.Combine(@"../../../../ModelLayer/appsettings.json"));
                    config.AddJsonFile(settingPath);
                })
                .ConfigureServices((hostContext, services) =>
                {
                string connectionString = hostContext.Configuration.GetConnectionString("Default");

                services.AddAutoMapper(typeof(MappingProfile));
                    services.AddSingleton<IUsersDbContextFactory>(s => new UsersDbContextFactory(connectionString));
                    services.AddScoped<IUsersRepository, UsersRepository>();
                    services.AddSingleton<NavigationStore>();
                    services.AddSingleton<UsersStore>();

                    services.AddSingleton<Func<LoadUsersViewModel>>(s => () => s.GetRequiredService<LoadUsersViewModel>());
                    services.AddSingleton<Func<EditUserViewModel>>(s => () => s.GetRequiredService<EditUserViewModel>());
                    services.AddSingleton<Func<ListUsersViewModel>>(s => () => s.GetRequiredService<ListUsersViewModel>());

                    services.AddSingleton<NavigationService<LoadUsersViewModel>>();
                    services.AddSingleton<NavigationService<ListUsersViewModel>>();
                    services.AddSingleton<NavigationService<EditUserViewModel>>();

                    services.AddTransient<LoadUsersViewModel>();
                    services.AddTransient<ListUsersViewModel>();
                    services.AddTransient<EditUserViewModel>();

                    services.AddSingleton<MainViewModel>();
                    services.AddSingleton(s => new MainWindow()
                    {
                        DataContext = s.GetRequiredService<MainViewModel>()
                    });
                })
                .Build();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            _host.Start();

            IUsersDbContextFactory usersDbContextFactory = _host.Services.GetRequiredService<IUsersDbContextFactory>();
            using(UsersDbContext dbContext = usersDbContextFactory.CreateDbContext())
            {
                dbContext.Database.Migrate();
            }

            NavigationService<LoadUsersViewModel> navigationService = _host.Services.GetRequiredService<NavigationService<LoadUsersViewModel>>();
            navigationService.Navigate();

            MainWindow = _host.Services.GetRequiredService<MainWindow>();
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
