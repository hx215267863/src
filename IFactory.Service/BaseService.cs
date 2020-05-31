using IFactory.Data;

namespace IFactory.Service
{
    public abstract class BaseService
    {
        protected DataContext DataContext { get; private set; }

        public BaseService(IDatabaseFactory databaseFactory)
        {
            this.DataContext = databaseFactory.Get();
        }
    }
}
