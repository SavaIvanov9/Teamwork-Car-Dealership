using System;
using System.Linq;
using System.Linq.Expressions;

namespace Dealership.Data.Contracts
{
    public interface IDealershipRepository<T>
        where T : class
    {
        IQueryable<T> All();

        IQueryable<T> Search(Expression<Func<T, bool>> condition);

        T GetById(int id);

        void Add(T entity);

        void Update(T entity);

        void Delete(T entity);

        void Delete(int id);

        void Detach(T entity);

        int SaveChanges();
    }
}
