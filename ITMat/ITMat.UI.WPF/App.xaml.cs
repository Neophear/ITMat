﻿using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using ITMat.UI.WPF.Interfaces;
using ITMat.UI.WPF.ServiceLayer;
using ITMat.UI.WPF.ViewModels;
using ITMat.UI.WPF.Pages;

namespace ITMat.UI.WPF
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
            services.AddTransient<IConfiguration>(services => Configuration);
            services.AddTransient<MainWindow>();
            services.AddTransient<IEmployeeService, EmployeeService>();

            services.AddSingleton<EmployeesViewModel>();

            services.AddTransient<EmployeesPage>();
        }

        private readonly IEnumerable<KeyValuePair<string, string>> config = new List<KeyValuePair<string, string>>
        {
            new KeyValuePair<string, string>("api_url", "https://localhost:44377/api")
        };
    }
}