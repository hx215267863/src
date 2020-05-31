using IFactory.Domain.Crafts.Base.Mappings;
using IFactory.Domain.Crafts.Inspection1.Entities;

namespace IFactory.Domain.Crafts.Inspection1.Mappings
{
    public class Inspection1FacilityRunArgMap : FacilityRunArgMap<Inspection1FacilityRunArgInfo>
    {
        public override string TableName
        {
            get
            {
                return "autoinspection_facility_run_arg";
            }
        }
    }
}
