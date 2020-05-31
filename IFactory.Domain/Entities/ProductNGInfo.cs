using System;

namespace IFactory.Domain.Entities
{
    public class ProductNGInfo
    {
        public string DeviceNo { get; set; }

        public DateTime ProductTime { get; set; }

        public DateTime NGTime { get; set; }

        public int MotorNG { get; set; }

        public int QiGangNG { get; set; }

        public int GanYingNG { get; set; }

        public string ProductionNo { get; set; }

        public DateTime ProductionStart { get; set; }

        public DateTime ProductionEnd { get; set; }

        public DateTime ProductionTimer { get; set; }

        public DateTime WaitTime { get; set; }

        public virtual FacilityInfo Facility { get; set; }
    }
}
