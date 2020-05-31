using IFactory.Common;
using IFactory.Domain.Crafts.Baking.Entities;
using IFactory.Domain.Crafts.Degassing.Entities;
using IFactory.Domain.Crafts.FEF.Entities;
using IFactory.Domain.Crafts.Injection.Entities;
using IFactory.Domain.Crafts.Inspection1.Entities;
using IFactory.Domain.Crafts.Inspection2.Entities;
using IFactory.Domain.Crafts.MIB.Entities;
using IFactory.Domain.Crafts.Mylar.Entities;
using IFactory.Domain.Crafts.OCV1.Entities;
using IFactory.Domain.Crafts.OCVB.Entities;
using IFactory.Domain.Crafts.Packing.Entities;
using IFactory.Domain.Crafts.PIEF.Entities;
using IFactory.Domain.Crafts.RF.Entities;
using System;

namespace IFactory.Domain.Crafts.Base.Entities
{
    public abstract class FacilityProductionDataInfo
    {
        public int Iden { get; set; }

        public string BatteryBarCode { get; set; }

        public int DeviceGroupDID { get; set; }

        public int No { get; set; }



        public static Type GetFacilityProductionDataType(string craftNO)
        {
            switch (CommonHelper.GetCraftShortNO(craftNO))
            {
                case "IN1":
                    return typeof(Inspection1FacilityProductionDataInfo);
                case "IN2":
                    return typeof(Inspection2FacilityProductionDataInfo);
                default:
                    throw new Exception("无效的CraftNO");
            }
        }
    }
}
