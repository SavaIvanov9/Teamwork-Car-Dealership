using Dealership.Data.Contracts;

namespace Dealership.Data
{
    public class DealershipData : IDealershipData
    {
        private readonly IDealershipDbContext context;

        public DealershipData(IDealershipDbContext context)
        {
            this.context = context;
        }

        public int SaveChanges()
        {
            return this.context.SaveChanges();
        }

        public void Dispose()
        {
            this.context.Dispose();
        }
    }
}
