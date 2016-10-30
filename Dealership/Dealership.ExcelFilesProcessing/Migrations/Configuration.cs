namespace Dealership.Data.Migrations
{
    using ExcelFilesProcessing;
    using System.Data.Entity.Migrations;

    public sealed class Configuration1 : DbMigrationsConfiguration<SalesReportsDBContext>
    {
        public Configuration1()
        {
            this.AutomaticMigrationsEnabled = true;
            this.AutomaticMigrationDataLossAllowed = true;
            this.ContextKey = "Dealership.Data.DealershipDbContext";
        }
    }
}
