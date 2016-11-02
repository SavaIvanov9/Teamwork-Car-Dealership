using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

using Dealership.Data.Contracts;
using Dealership.Data.Migrations;
using Dealership.Models.Models.MongoDbSource;
using Dealership.Models.Models.XmlSource;
using Dealership.Models.Models.SalesReportSource;

namespace Dealership.Data
{
    public class DealershipDbContext : DbContext, IDealershipDbContext
    {
        public DealershipDbContext()
            : base("DealershipDb")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<DealershipDbContext, Configuration>());
        }

        public virtual IDbSet<Vehicle> Vehicles { get; set; }

        public virtual IDbSet<Fuel> Fuels { get; set; }

        public virtual IDbSet<Brand> Brands { get; set; }

        public virtual IDbSet<VehicleType> VehiclesTypes { get; set; }

        public virtual IDbSet<Address> Addresses { get; set; }

        public virtual IDbSet<City> Cities { get; set; }

        public virtual IDbSet<Country> Countries { get; set; }

        public virtual IDbSet<Employee> Employees { get; set; }

        public virtual IDbSet<Position> Positions { get; set; }

        public virtual IDbSet<Shop> Shops { get; set; }

        public virtual IDbSet<Sale> Sales { get; set; }

        public new IDbSet<T> Set<T>() where T : class
        {
            return base.Set<T>();
        }

        public new int SaveChanges()
        {
            return base.SaveChanges();
        }

        public new void Dispose()
        {
            base.Dispose();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            base.OnModelCreating(modelBuilder);
        }
    }
}
