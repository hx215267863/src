using IFactory.Domain.Crafts.Base.Mappings;
using IFactory.Domain.Crafts.OCV1.Entities;

namespace IFactory.Domain.Crafts.OCV1.Mappings
{
    public class OCV1FacilityRunArgMap : FacilityRunArgMap<OCV1FacilityRunArgInfo>
    {
        public override string TableName
        {
            get
            {
                return "ocv1_facility_run_arg";
            }
        }
    }
}
