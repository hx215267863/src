using IFactory.Domain.Crafts.Base.Mappings;
using IFactory.Domain.Crafts.MIB.Entities;

namespace IFactory.Domain.Crafts.MIB.Mappings
{
    public class MIBFacilityRunArgMap : FacilityRunArgMap<MIBFacilityRunArgInfo>
    {
        public override string TableName
        {
            get
            {
                return "mib_facility_run_arg";
            }
        }
    }
}
