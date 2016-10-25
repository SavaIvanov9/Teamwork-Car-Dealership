using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

using Dealership.Data.Contracts;

namespace Dealership.Data
{
    public class DealershipRepository<T> : IDealershipRepository<T> where T : class
    {
        
        public DealershipRepository(IDealershipDbContext context)
        {
            if (context == null)
            {
                throw new ArgumentException("An instance of DbContext is required to use this repository.", "context");
            }

            this.Context = context;
            this.DbSet = this.Context.Set<T>();
        }

        protected IDbSet<T> DbSet { get; set; }

        protected IDealershipDbContext Context { get; set; }

        public IQueryable<T> All()
        {
            return this.DbSet.AsQueryable();
        }

        public IQueryable<T> Search(Expression<Func<T, bool>> condition)
        {
            return this.All().Where(condition);
        }

        public T GetById(int id)
        {
            return this.DbSet.Find(id);
        }

        public void Add(T entity)
        {
            this.ChangeEntityState(entity, EntityState.Added);
        }

        public void Update(T entity)
        {
            this.ChangeEntityState(entity, EntityState.Modified);
        }

        public void Delete(T entity)
        {
            this.ChangeEntityState(entity, EntityState.Deleted);
        }

        public void Delete(int id)
        {
            var entity = this.GetById(id);

            if (entity != null)
            {
                this.Delete(entity);
            }
        }

        public void Detach(T entity)
        {
            this.ChangeEntityState(entity, EntityState.Detached);
        }

        public int SaveChanges()
        {
            return this.Context.SaveChanges();
        }

        private void ChangeEntityState(T entity, EntityState newEntityState)
        {
            var entry = this.Context.Entry(entity);
            entry.State = newEntityState;
        }
    }
}
