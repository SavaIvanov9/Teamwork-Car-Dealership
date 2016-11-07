using Dealership.JsonReporter.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dealership.JsonReporter.Repositories
{
    public class EntityFrameworkGenericRepository<T> : IGenericRepository<T> where T : class
    {
        public EntityFrameworkGenericRepository(DbContext context)
        {
            if (context == null)
            {
                throw new ArgumentException("An instance of DbContext is required to use this repository.", nameof(context));
            }

            this.Context = context;
            this.DbSet = this.Context.Set<T>();
        }


        protected IDbSet<T> DbSet { get; set; }

        protected DbContext Context { get; set; }

        public void Add(T entity)
        {
            var entry = this.Context.Entry(entity);
            if (entry.State != EntityState.Detached)
            {
                entry.State = EntityState.Added;
            }
            else
            {
                this.DbSet.Add(entity);
            }
        }

        public void Delete(T entity)
        {
            var entry = this.Context.Entry(entity);
            if (entry.State != EntityState.Deleted)
            {
                entry.State = EntityState.Deleted;
            }
            else
            {
                this.DbSet.Attach(entity);
                this.DbSet.Remove(entity);
            }
        }

        public IEnumerable<T> GetAll()
        {
            return this.DbSet.ToList();
        }

        public IEnumerable<T> GetAll(System.Linq.Expressions.Expression<Func<T, bool>> filterExpression)
        {
            return this.DbSet.Where(filterExpression).ToList();
        }
    }
}
