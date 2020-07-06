namespace ITMat.Data.Migrations
{
    public interface IDatabaseMigrator
    {
        bool MigrateToLatest();
    }
}