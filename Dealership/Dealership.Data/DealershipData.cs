using System;
using System.Collections.Generic;

using Dealership.Data.Contracts;
using Dealership.Models.Models.MongoDbSource;
using Dealership.Models.Models.XmlSource;

namespace Dealership.Data
{
    public class DealershipData : IDealershipData
    {
        private readonly IDealershipDbContext context;

        private readonly IDictionary<Type, object> repositories;

        public DealershipData(IDealershipDbContext context)
        {
            this.context = context;
            this.repositories = new Dictionary<Type, object>();
        }

        public IDealershipRepository<Vehicle> Vehicles
        {
            get
            {
                return this.GetRepository<Vehicle>();
            }
        }

        public IDealershipRepository<Tire> Tires
        {
            get
            {
                return this.GetRepository<Tire>();
            }
        }

        public IDealershipRepository<Battery> Batteries
        {
            get
            {
                return this.GetRepository<Battery>();
            }
        }

        public IDealershipRepository<TireType> TireTypes
        {
            get
            {
                return this.GetRepository<TireType>();
            }
        }

        public IDealershipRepository<TireBrand> TireBrands
        {
            get
            {
                return this.GetRepository<TireBrand>();
            }
        }

        public IDealershipRepository<VehicleType> VehicleTypes
        {
            get
            {
                return this.GetRepository<VehicleType>();
            }
        }

        public IDealershipRepository<BatteryBrand> BatteryBrands
        {
            get
            {
                return this.GetRepository<BatteryBrand>();
            }
        }

        public IDealershipRepository<Brand> Brands
        {
            get
            {
                return this.GetRepository<Brand>();
            }
        }

        public IDealershipRepository<Fuel> Fuels
        {
            get
            {
                return this.GetRepository<Fuel>();
            }
        }

        public IDealershipRepository<Address> Addresses
        {
            get
            {
                return this.GetRepository<Address>();
            }
        }

        public IDealershipRepository<Employee> Employees
        {
            get
            {
                return this.GetRepository<Employee>();
            }
        }

        public IDealershipRepository<City> Cities
        {
            get
            {
                return this.GetRepository<City>();
            }
        }

        public IDealershipRepository<Country> Countries
        {
            get
            {
                return this.GetRepository<Country>();
            }
        }

        public IDealershipRepository<Position> Positions
        {
            get
            {
                return this.GetRepository<Position>();
            }
        }

        public IDealershipRepository<Shop> Shops
        {
            get
            {
                return this.GetRepository<Shop>();
            }
        }

        public int SaveChanges()
        {
            return this.context.SaveChanges();
        }

        public void Dispose()
        {
            this.context.Dispose();
        }

        private IDealershipRepository<T> GetRepository<T>() where T : class
        {
            var typeOfModel = typeof(T);

            if (!this.repositories.ContainsKey(typeOfModel))
            {
                var typeOfRepository = typeof(DealershipRepository<T>);
                this.repositories.Add(typeOfModel, Activator.CreateInstance(typeOfRepository, this.context));
            }

            return (IDealershipRepository<T>)this.repositories[typeOfModel];
        }
    }
}
