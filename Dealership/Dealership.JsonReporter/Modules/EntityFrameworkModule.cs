using Dealership.Data;
using Dealership.JsonReporter.Repositories;
using Dealership.JsonReporter.Repositories.Contracts;
using Dealership.Models.Contracts.MongoDbSource;
using Dealership.Models.Contracts.SalesReportSource;
using Dealership.Models.Models.MongoDbSource;
using Dealership.Models.Models.SalesReportSource;
using Ninject;
using Ninject.Modules;
using System;
using System.Data.Entity;

namespace Dealership.JsonReporter.Modules
{
    public class EntityFrameworkModule : NinjectModule
    {
        public override void Load()
        {
            Bind<DbContext>().To<DealershipDbContext>().InSingletonScope();
            Bind(typeof(IGenericRepository<>)).To(typeof(EntityFrameworkGenericRepository<>));
            Bind<Func<IUnitOfWork>>().ToMethod(ctx => () => ctx.Kernel.Get<IUnitOfWork>());
            Bind<IUnitOfWork>().To<EntityFrameworkUnitOfWork>();

            //Bind<IVehicle>().To<Vehicle>();
            //Bind<ISale>().To<Sale>();
        }
    }
}
