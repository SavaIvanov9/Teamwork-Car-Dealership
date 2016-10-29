using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;

namespace Dealership.Data.Contracts
{
    public interface IDealershipRepository<T>
        where T : class
    {
        IQueryable<T> All();

        bool All(Func<T, bool> condition);

        ObservableCollection<T> Local { get; }

        IQueryable<T> Search(Expression<Func<T, bool>> condition);

        T GetById(int id);

        T FirstOrDefault(Expression<Func<T, bool>> condition);

        bool Any();

        void Add(T entity);

        void Update(T entity);

        void Delete(T entity);

        void Delete(int id);

        void Detach(T entity);

        int SaveChanges();
    }
}
