using System.Collections.Generic;

namespace IFactory.Service.Crafts
{
    public interface IBaseCraftService<T> where T : class
    {
        T Get(object id);

        T Insert(T entity);

        T Update(T entity);

        void Delete(T entity);

        IList<T> GetAll();
    }
}
