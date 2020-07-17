namespace ITMat.Core.Data.Migrations
{
    public interface IDatabaseMigrator
    {
        bool MigrateToLatest();
    }
}