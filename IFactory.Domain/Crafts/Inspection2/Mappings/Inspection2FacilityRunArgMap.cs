using IFactory.Domain.Crafts.Base.Mappings;
using IFactory.Domain.Crafts.Inspection2.Entities;

namespace IFactory.Domain.Crafts.Inspection2.Mappings
{
    public class Inspection2FacilityRunArgMap : FacilityRunArgMap<Inspection2FacilityRunArgInfo>
    {
        public override string TableName
        {
            get
            {
                return "autoinspection2_facility_run_arg";
            }
        }
    }
}
