namespace Dealership.SQLite
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class SQLiteDbContext : DbContext
    {
        public SQLiteDbContext()
            : base("DealershipSqlite")
        {
        }

        public DbSet<Item> Items { get; set; }
    }
}
