using ITMat.Data.Migrations;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.IO;

namespace ITMat.API
{
    public class Program
    {
        private const int EXIT_SUCCESS = 0x00,
                          EXIT_FAILURE = 0x01;

        public static int Main(string[] args)
        {
            //Build and get config. This to get connectionString for database migrator
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true)
                .AddCommandLine(args)
                .AddEnvironmentVariables()
                .Build();

            var connString = config.GetConnectionString("DbConnectionString");

            //Build and get logger for database migrator
            var logger = LoggerFactory.Create(builder => builder.AddConsole().AddDebug()).CreateLogger("");

            var migrator = new DatabaseMigrator(logger, connString);

            //Migrate to latest migration.
            if (!migrator.MigrateToLatest())
                return EXIT_FAILURE;

            //Run application
            CreateHostBuilder(args).Build().Run();

            return EXIT_SUCCESS;
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}