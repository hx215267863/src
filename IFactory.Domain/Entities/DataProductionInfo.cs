using System;

namespace IFactory.Domain.Entities
{
    public class DataProductionInfo
    {
        public int Iden { get; set; }

        public DateTime ProductTime { get; set; }

        public int TotalProduction { get; set; }

        public int OKProduction { get; set; }

        public int EnableProduction { get; set; }

        public DateTime TimeStart { get; set; }

        public DateTime TimeEnd { get; set; }

        public string Keyword { get; set; }

        public virtual FacilityInfo Facility { get; set; }
    }
}
