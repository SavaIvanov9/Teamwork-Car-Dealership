using System;

namespace Dealership.Data.Contracts
{
    public interface IDealershipData : IDisposable
    {
        int SaveChanges();
    }
}
