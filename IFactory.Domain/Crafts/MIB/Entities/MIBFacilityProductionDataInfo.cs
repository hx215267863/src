using IFactory.Domain.Crafts.Base.Entities;
using System;

namespace IFactory.Domain.Crafts.MIB.Entities
{
    public class MIBFacilityProductionDataInfo : FacilityProductionDataInfo
    {
        public DateTime? StartDate { get; set; }

        public string ProductNo { get; set; }

        public string TabBarCode { get; set; }

        public string Operator { get; set; }

        public DateTime? InTime { get; set; }

        public DateTime? OutTime { get; set; }

        public int BoxNum { get; set; }

        public int Floor { get; set; }

        public int Location { get; set; }

        public int TempratureIndex { get; set; }

        public float? Temprature1 { get; set; }

        public float? Temprature2 { get; set; }

        public float? Vacuum { get; set; }

        public int UserId { get; set; }
    }
}
