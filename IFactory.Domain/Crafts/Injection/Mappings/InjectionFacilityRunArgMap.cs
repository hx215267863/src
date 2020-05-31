using IFactory.Domain.Crafts.Base.Mappings;
using IFactory.Domain.Crafts.Injection.Entities;

namespace IFactory.Domain.Crafts.Injection.Mappings
{
    public class InjectionFacilityRunArgMap : FacilityRunArgMap<InjectionFacilityRunArgInfo>
    {
        public override string TableName
        {
            get
            {
                return "injection_facility_run_arg";
            }
        }
    }
}
