using IFactory.Domain.Crafts.Base.Mappings;
using IFactory.Domain.Crafts.Mylar.Entities;

namespace IFactory.Domain.Crafts.Mylar.Mappings
{
    public class MylarFacilityRunArgMap : FacilityRunArgMap<MylarFacilityRunArgInfo>
    {
        public override string TableName
        {
            get
            {
                return "mylar_facility_run_arg";
            }
        }
    }
}
