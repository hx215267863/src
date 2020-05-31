using System;

namespace IFactory.Data
{
    public interface IDatabaseFactory : IDisposable
    {
        DataContext Get();
    }
}
