using IFactory.Data;
using System.Collections.Generic;
using System.Linq;

namespace IFactory.Service
{
    public abstract class BaseService<T> : BaseService, IBaseService<T> where T : class
    {
        protected virtual IQueryable<T> Table
        {
            get
            {
                return this.DataContext.Set<T>();
            }
        }

        protected virtual System.Data.Entity.DbSet<T> DbSet
        {
            get
            {
                return this.DataContext.Set<T>();
            }
        }

        public BaseService(IDatabaseFactory databaseFactory)
          : base(databaseFactory)
        {
        }

        public virtual T Get(object id)
        {
            return this.DbSet.Find(id);
        }

        public virtual T Insert(T entity)
        {
            this.DbSet.Add(entity);
            this.DataContext.SaveChanges();
            return entity;
        }

        public virtual T Update(T entity)
        {
            this.DataContext.SaveChanges();
            return entity;
        }

        public virtual void Delete(T entity)
        {
            this.DbSet.Remove(entity);
            this.DataContext.SaveChanges();
        }

        public virtual IList<T> GetAll()
        {
            return this.Table.ToList();
        }
    }
}
