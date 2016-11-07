using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Dealership.JsonReporter.Repositories.Contracts
{
    public interface IGenericRepository<T> where T : class
    {
        IEnumerable<T> GetAll();

        void Add(T entity);

        void Delete(T entity);


        IEnumerable<T> GetAll(Expression<Func<T, bool>> predicate);
    }
}
