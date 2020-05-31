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
    public abstract class FacilityRunArgInfo
    {
        public int DID { get; set; }

        public int FacilityDID { get; set; }

        public DateTime MCCollectDDate { get; set; }

        public long? MCCount { get; set; }

        public long? MCBanCount { get; set; }

        public int? MCType { get; set; }

        public int? MCHour { get; set; }

        public long? MCTotalCount { get; set; }

        public long? MCTotalBadCount { get; set; }

        public long? MCOpenRunTime { get; set; }

        public long? MCOpenRunTotalTime { get; set; }

        public long? MCWaitTime { get; set; }

        public long? MCWaitTotalTime { get; set; }

        public long? MCAutoRunTime { get; set; }

        public long? MCAutoRunTotalTime { get; set; }

        public long? MCRuningTime { get; set; }

        public long? MCRuningTotalTime { get; set; }

        public long? MCAutoRunWarningTime { get; set; }

        public long? MCAutoRunWarningTotalTime { get; set; }

        public long? MCStopTime { get; set; }

        public long? MCStopTotalTime { get; set; }

        public static Type GetFacilityRunArgType(string craftNO)
        {
            switch (CommonHelper.GetCraftShortNO(craftNO))
            {
                case "BAK":
                    return typeof(BakingFacilityRunArgInfo);
                case "DGA":
                    return typeof(DegassingFacilityRunArgInfo);
                case "FEF":
                    return typeof(FEFFacilityRunArgInfo);
                case "IN1":
                    return typeof(Inspection1FacilityRunArgInfo);
                case "IN2":
                    return typeof(Inspection2FacilityRunArgInfo);
                case "INJ":
                    return typeof(InjectionFacilityRunArgInfo);
                case "MIB":
                    return typeof(MIBFacilityRunArgInfo);
                case "MLA":
                    return typeof(MylarFacilityRunArgInfo);
                case "OC1":
                    return typeof(OCV1FacilityRunArgInfo);
                case "OCB":
                    return typeof(OCVBFacilityRunArgInfo);
                case "PAK":
                    return typeof(PackingFacilityRunArgInfo);
                case "PIE":
                    return typeof(PIEFFacilityRunArgInfo);
                case "RFP":
                    return typeof(RFFacilityRunArgInfo);
                default:
                    throw new Exception("无效的CraftNO");
            }
        }
    }
}
