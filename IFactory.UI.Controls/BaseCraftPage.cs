using System.Threading.Tasks;

namespace IFactory.UI.Controls
{
    public class BaseCraftPage : BasePage
    {
        public virtual int CraftDID { get; set; }

        public virtual string AlarmCheck { get; set; }

        public virtual string FacilityDid { get; set; }

        public virtual string CraftNO { get; set; }

        public override int? GetFacilityState()
        {
            return this.GetGetFacilityStateByCraft(this.CraftDID);
        }
    }
}
