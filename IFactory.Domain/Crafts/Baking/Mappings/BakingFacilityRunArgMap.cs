using IFactory.Domain.Crafts.Baking.Entities;
using IFactory.Domain.Crafts.Base.Mappings;

namespace IFactory.Domain.Crafts.Baking.Mappings
{
    public class BakingFacilityRunArgMap : FacilityRunArgMap<BakingFacilityRunArgInfo>
    {
        public override string TableName
        {
            get
            {
                return "freebaking_facility_run_arg";
            }
        }
    }
}
