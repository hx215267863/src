using System;

namespace IFactory.Domain.Models
{
    public class DataPicItem
    {
        public int Iden { get; set; }

        public float Quality { get; set; }

        public int CapacityPage { get; set; }

        public int PPM { get; set; }

        public int total { get; set; }

        public double flo { get; set; }

        public int OK { get; set; }

        public DateTime ProductTime { get; set; }

        public string FacilityDiD { get; set; }

        public string BatteryBarCode { get; set; }

        public string DeviceNo { get; set; }

        public string Operator { get; set; }

        public DateTime TimeStart { get; set; }

        public DateTime TimeEnd { get; set; }

        public string Keyword { get; set; }

        public DateTime WorkDays { get; set; }

        public int Count { get; set; }
    }
}
