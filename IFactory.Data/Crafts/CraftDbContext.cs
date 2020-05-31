using IFactory.Domain.Crafts.Base.Entities;
using System.Data.Entity;
using System.Linq;

namespace IFactory.Data.Crafts
{
    public abstract class CraftDbContext : DbContext
    {
        public abstract IQueryable<FacilityProductionDataInfo> FacilityProductionDataInfos { get; }

        public abstract IQueryable<FacilityRunArgInfo> FacilityRunArgInfos { get; }

        public CraftDbContext(string nameOrConnection) : base(nameOrConnection)
        {
        }
    }
}
