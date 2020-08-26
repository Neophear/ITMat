using ITMat.UI.WindowsApp.Pages;
using ITMat.UI.WindowsApp.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Windows;

namespace ITMat.UI.WindowsApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static IServiceProvider ServiceProvider { get; private set; }
        public IConfiguration Configuration { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(config)
                .Build();

            Configuration = configuration;

            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            ServiceProvider = serviceCollection.BuildServiceProvider();
        }

        private void ConfigureServices(ServiceCollection services)
        {
            services.AddTransient(s => Configuration);
            services.AddTransient<MainWindow>();
            services.AddTransient<IEmployeeService, Services.MockService.MockEmployeeService>();
            services.AddTransient<ILoanService, Services.MockService.MockLoanService>();

            services.AddTransient<EmployeesPage>();
        }

        private readonly IEnumerable<KeyValuePair<string, string>> config = new List<KeyValuePair<string, string>>
        {
            new KeyValuePair<string, string>("api_url", "https://localhost:44377/api")
        };
    }
}