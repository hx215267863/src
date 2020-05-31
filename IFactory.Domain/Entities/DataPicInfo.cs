using System;

namespace IFactory.Domain.Entities
{
    public class DataPicInfo
    {
        public int Iden { get; set; }

        public DateTime ProductTime { get; set; }

        public int FacilityDiD { get; set; }

        public string BatteryBarCode { get; set; }

        public string Operator { get; set; }

        public virtual FacilityInfo Facility { get; set; }

        public DateTime TimeStart { get; set; }

        public DateTime TimeEnd { get; set; }

        public string Keyword { get; set; }
    }
}
