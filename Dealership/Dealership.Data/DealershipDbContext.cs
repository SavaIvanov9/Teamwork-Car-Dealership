using System;
using System.Data.Entity;

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

        public virtual IDbSet<Tire> Tires { get; set; }

        public virtual IDbSet<Battery> Batteries { get; set; }

        public virtual IDbSet<Vehicle> Vehicles { get; set; }

        public virtual IDbSet<Fuel> Fuels { get; set; }

        public virtual IDbSet<Brand> Brands { get; set; }

        public virtual IDbSet<VehicleType> VehiclesTypes { get; set; }

        public virtual IDbSet<TireType> TireTypes { get; set; }

        public virtual IDbSet<TireBrand> TyreBrands { get; set; }

        public virtual IDbSet<BatteryBrand> BatteryBrands { get; set; }

        public IDbSet<Address> Addresses { get; set; }

        public IDbSet<City> Cities { get; set; }

        public IDbSet<Country> Countries { get; set; }

        public IDbSet<Employee> Employees { get; set; }

        public IDbSet<Position> Positions { get; set; }

        public IDbSet<Shop> Shops { get; set; }

        public IDbSet<Sale> Sales { get; set; }

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
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Shop>().HasRequired(sh => sh.Address).WithMany().WillCascadeOnDelete(false);
        }
    }
}
