using System;

namespace IFactory.Data.Crafts
{
    public interface ICraftDbFactory : IDisposable
    {
        string CraftNO { get; set; }

        CraftDbContext Get();
    }
}
