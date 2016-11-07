using Dealership.JsonReporter.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dealership.JsonReporter.Repositories
{
    public class EntityFrameworkUnitOfWork : IUnitOfWork
    {
        private readonly DbContext dbContext;

        public EntityFrameworkUnitOfWork(DbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Commit()
        {
            this.dbContext.SaveChanges();
        }

        public void Dispose()
        {
        }
    }
}
