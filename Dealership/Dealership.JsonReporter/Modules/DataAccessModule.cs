using Dealership.JsonReporter.Repositories;
using Dealership.JsonReporter.Repositories.Contracts;
using Dealership.MySQL;
using Ninject;
using Ninject.Modules;
using System;
using Telerik.OpenAccess;

namespace Dealership.JsonReporter.Modules
{
    public class DataAccessModule: NinjectModule
    {

        public override void Load()
        {
            Bind<OpenAccessContext>().To<DataAccessDbContext>().InSingletonScope();
            Bind(typeof(IGenericRepository<>)).To(typeof(DataAccessGenericRepository<>));
            Bind<Func<Repositories.Contracts.IUnitOfWork>>().ToMethod(ctx => () => ctx.Kernel.Get<Repositories.Contracts.IUnitOfWork>());
            Bind<Repositories.Contracts.IUnitOfWork>().To<DataAccessUnitOfWork>();
        }
    }
}
