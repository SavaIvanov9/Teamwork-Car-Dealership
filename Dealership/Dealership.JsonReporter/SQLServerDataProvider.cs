using Dealership.JsonReporter.Repositories.Contracts;
using Dealership.Models.Models.MongoDbSource;
using Dealership.Models.Models.SalesReportSource;
using System;

namespace Dealership.JsonReporter
{
    public class SQLServerDataProvider
    {
        private IGenericRepository<Vehicle> vehicles;
        private IGenericRepository<Sale> sales;
        private Func<IUnitOfWork> unitOfWork;

        public SQLServerDataProvider(Func<IUnitOfWork> unitOfWork, IGenericRepository<Vehicle> vehicles, IGenericRepository<Sale> sales)
        {
            this.Vehicles = vehicles;
            this.UnitOfWork = unitOfWork;
            this.Sales = sales;
        }

        public IGenericRepository<Vehicle> Vehicles
        {
            get
            {
                return vehicles;
            }

            set
            {
                vehicles = value;
            }
        }

        public Func<IUnitOfWork> UnitOfWork
        {
            get
            {
                return unitOfWork;
            }

            set
            {
                unitOfWork = value;
            }
        }

        public IGenericRepository<Sale> Sales
        {
            get
            {
                return sales;
            }

            set
            {
                sales = value;
            }
        }
    }
}
