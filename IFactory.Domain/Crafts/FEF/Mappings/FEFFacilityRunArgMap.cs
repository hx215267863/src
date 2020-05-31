using IFactory.Domain.Crafts.Base.Mappings;
using IFactory.Domain.Crafts.FEF.Entities;

namespace IFactory.Domain.Crafts.FEF.Mappings
{
    public class FEFFacilityRunArgMap : FacilityRunArgMap<FEFFacilityRunArgInfo>
    {
        public override string TableName
        {
            get
            {
                return "fef_facility_run_arg";
            }
        }
    }
}
