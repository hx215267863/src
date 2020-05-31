using IFactory.Domain.Crafts.Base.Mappings;
using IFactory.Domain.Crafts.Degassing.Entities;

namespace IFactory.Domain.Crafts.Degassing.Mappings
{
    public class DegassingFacilityRunArgMap : FacilityRunArgMap<DegassingFacilityRunArgInfo>
    {
        public override string TableName
        {
            get
            {
                return "degassing_facility_run_arg";
            }
        }
    }
}
