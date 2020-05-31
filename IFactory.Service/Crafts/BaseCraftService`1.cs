using IFactory.Data.Crafts;
using System.Collections.Generic;
using System.Linq;

namespace IFactory.Service.Crafts
{
    public abstract class BaseCraftService<T> : IBaseCraftService<T> where T : class
    {
        protected CraftDbContext DataContext { get; private set; }

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

        public BaseCraftService(ICraftDbFactory databaseFactory)
        {
            this.DataContext = databaseFactory.Get();
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
            return this.Table.ToList<T>();
        }
    }
}
