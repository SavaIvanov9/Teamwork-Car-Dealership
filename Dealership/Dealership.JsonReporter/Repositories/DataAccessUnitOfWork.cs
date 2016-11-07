using Telerik.OpenAccess;

namespace Dealership.JsonReporter.Repositories
{
    public class DataAccessUnitOfWork : Contracts.IUnitOfWork
    {
        private readonly OpenAccessContext dbContext;

        public DataAccessUnitOfWork(OpenAccessContext dbContext)
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
