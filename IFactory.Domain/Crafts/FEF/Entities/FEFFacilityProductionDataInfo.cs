using IFactory.Domain.Crafts.Base.Entities;
using System;

namespace IFactory.Domain.Crafts.FEF.Entities
{
    public class FEFFacilityProductionDataInfo : FacilityProductionDataInfo
    {
        public DateTime? StartDate { get; set; }

        public string ProductNo { get; set; }

        public string Operator { get; set; }

        public float? Temprature_Top { get; set; }

        public float? Temprature_Bottom { get; set; }

        public float? UserId { get; set; }
    }
}
