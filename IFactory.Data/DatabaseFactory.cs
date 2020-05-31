using IFactory.Common;
using System;

namespace IFactory.Data
{
    public class DatabaseFactory : Disposable, IDatabaseFactory, IDisposable
    {
        private DataContext dataContext;

        public DataContext Get()
        {
            return this.dataContext ?? (this.dataContext = new DataContext());
        }

        protected override void DisposeCore()
        {
            if (this.dataContext == null)
                return;
            this.dataContext.Dispose();
        }
    }
}
