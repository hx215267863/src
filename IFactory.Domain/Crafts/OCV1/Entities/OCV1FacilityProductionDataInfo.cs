using IFactory.Domain.Crafts.Base.Entities;
using System;

namespace IFactory.Domain.Crafts.OCV1.Entities
{
    public class OCV1FacilityProductionDataInfo : FacilityProductionDataInfo
    {
        public DateTime? StartDate { get; set; }

        public string ProductNo { get; set; }

        public string TabBarCode { get; set; }

        public string Operator { get; set; }

        public DateTime? TestTime { get; set; }

        public float? Voltage { get; set; }

        public float? coreResistance { get; set; }

        public float? ServoSpeed2 { get; set; }

        public float? Temprature_E { get; set; }

        public float? Temprature_base { get; set; }

        public int OCVChannel { get; set; }

        public int UserId { get; set; }
    }
}
