using DbUp;
using DbUp.Engine.Output;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace ITMat.Core.Data.Migrations
{
    public class DatabaseMigrator : IDatabaseMigrator
    {
        private readonly string connectionString;
        private readonly ILogger logger;

        public DatabaseMigrator(ILogger logger, string connectionString)
        {
            this.logger = logger;
            this.connectionString = connectionString;
        }

        public bool MigrateToLatest()
        {
            logger.LogInformation("Starting migration engine.");

            //TODO! This drops the database and MUST be removed in PROD
            DropDatabase.For.SqlDatabase(connectionString);

            EnsureDatabase.For.SqlDatabase(connectionString);

            var upgradeEngine = DeployChanges
                .To
                .SqlDatabase(connectionString)
                .WithTransactionPerScript()
                .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
                .LogTo(new DbupLogger(logger))
                .Build();

            var discoveredScripts = upgradeEngine.GetDiscoveredScripts();

            if (discoveredScripts.Count <= 0)
            {
                logger.LogWarning("No SQL-scripts found.");
                return false;
            }

            logger.LogInformation($"Discovered {discoveredScripts.Count} scripts:");
            discoveredScripts.ForEach(s => logger.LogInformation(s.Name));

            if (!upgradeEngine.IsUpgradeRequired())
            {
                logger.LogInformation("Database upgrade is not required.");
                return true;
            }

            logger.LogInformation("Database upgrade is required.");

            logger.LogInformation("Executing the following new scripts in order:");
            upgradeEngine.GetScriptsToExecute().ForEach(s => logger.LogInformation(s.Name));

            var result = upgradeEngine.PerformUpgrade();

            if (result.Successful)
                logger.LogInformation("Success!");
            else
                logger.LogError(result.Error.Message);

            return result.Successful;
        }

        private class DbupLogger : IUpgradeLog
        {
            private readonly ILogger logger;

            public DbupLogger(ILogger logger)
                => this.logger = logger;

            public void WriteError(string format, params object[] args)
                => logger.LogError(format, args);

            public void WriteInformation(string format, params object[] args)
                => logger.LogInformation(format, args);

            public void WriteWarning(string format, params object[] args)
                => logger.LogWarning(format, args);
        }
    }
}