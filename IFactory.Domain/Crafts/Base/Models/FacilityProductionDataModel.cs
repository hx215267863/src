using IFactory.Common;
using IFactory.Domain.Crafts.Baking.Models;
using IFactory.Domain.Crafts.Degassing.Models;
using IFactory.Domain.Crafts.FEF.Models;
using IFactory.Domain.Crafts.Injection.Models;
using IFactory.Domain.Crafts.Inspection1.Models;
using IFactory.Domain.Crafts.Inspection2.Models;
using IFactory.Domain.Crafts.MIB.Models;
using IFactory.Domain.Crafts.Mylar.Models;
using IFactory.Domain.Crafts.OCV1.Models;
using IFactory.Domain.Crafts.OCVB.Models;
using IFactory.Domain.Crafts.Packing.Models;
using IFactory.Domain.Crafts.PIEF.Models;
using IFactory.Domain.Crafts.RF.Models;
using System;

namespace IFactory.Domain.Crafts.Base.Models
{
    public class FacilityProductionDataModel
    {
        public int Iden { get; set; }

        public string BatteryBarCode { get; set; }

        public int DeviceGroupDID { get; set; }

        public string DeviceGroupName { get; set; }

        public string DeviceGroupNO { get; set; }

        public int No { get; set; }

        public static Type GetFacilityProductionDataType(string craftNO)
        {
            switch (CommonHelper.GetCraftShortNO(craftNO))
            {
                case "BAK":
                    return typeof(BakingFacilityProductionDataModel);
                case "DGA":
                    return typeof(DegassingFacilityProductionDataModel);
                case "FEF":
                    return typeof(FEFFacilityProductionDataModel);
                case "IN1":
                    return typeof(Inspection1FacilityProductionDataModel);
                case "IN2":
                    return typeof(Inspection2FacilityProductionDataModel);
                case "INJ":
                    return typeof(InjectionFacilityProductionDataModel);
                case "MIB":
                    return typeof(MIBFacilityProductionDataModel);
                case "MLA":
                    return typeof(MylarFacilityProductionDataModel);
                case "OC1":
                    return typeof(OCV1FacilityProductionDataModel);
                case "OCB":
                    return typeof(OCVBFacilityProductionDataModel);
                case "PAK":
                    return typeof(PackingFacilityProductionDataModel);
                case "PIE":
                    return typeof(PIEFFacilityProductionDataModel);
                case "RFP":
                    return typeof(RFFacilityProductionDataModel);
                default:
                    throw new Exception("无效的CraftNO");
            }
        }
    }
}
