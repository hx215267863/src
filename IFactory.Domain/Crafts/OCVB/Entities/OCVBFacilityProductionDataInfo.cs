using IFactory.Domain.Crafts.Base.Entities;
using System;

namespace IFactory.Domain.Crafts.OCVB.Entities
{
    public class OCVBFacilityProductionDataInfo : FacilityProductionDataInfo
    {
        public DateTime? StartDate { get; set; }

        public string ProductNo { get; set; }

        public string TabBarCode { get; set; }

        public string Operator { get; set; }

        public DateTime? TestTime { get; set; }

        public float? Voltage { get; set; }

        public float? coreResistance { get; set; }

        public float? Temprature_E { get; set; }

        public float? Temprature_base { get; set; }

        public int OCVChannel { get; set; }

        public int IVChannel { get; set; }

        public float? Result { get; set; }

        public float? VIValue { get; set; }

        public int UserId { get; set; }
    }
}
