using Dealership.JsonReporter.Repositories.Contracts;
using Dealership.MySQL;
using System;

namespace Dealership.JsonReporter
{
    public class MySQLDataProvider
    {
        private IGenericRepository<JsonReport> jsonReports;
        private Func<IUnitOfWork> unitOfWork;

        public MySQLDataProvider(Func<IUnitOfWork> unitOfWork, IGenericRepository<JsonReport> jsonReport)
        {
            this.JsonReports = jsonReport;
            this.UnitOfWork = unitOfWork;
        }

        public IGenericRepository<JsonReport> JsonReports
        {
            get
            {
                return jsonReports;
            }

            set
            {
                jsonReports = value;
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
    }
}