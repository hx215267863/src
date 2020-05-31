using IFactory.Domain.Crafts.Base.Models;
using System;

namespace IFactory.Domain.Crafts.Inspection2.Models
{
    public class Inspection2FacilityProductionDataModel : FacilityProductionDataModel
    {
        public DateTime? StartDate { get; set; }

        public string ProductNo { get; set; }

        public string TabBarCode { get; set; }

        public float? PumpPressureOut { get; set; }

        public float? ServoTravel1 { get; set; }

        public float? ServoTravel2 { get; set; }

        public float? ServoSpeed1 { get; set; }

        public float? ServoSpeed2 { get; set; }

        public float? PumpAddTime { get; set; }

        public float? PumpSaveTime { get; set; }

        public string OpenMould { get; set; }

        public float? ServoSortFirstDistance { get; set; }

        public float? TopTemp { get; set; }

        public float? TopPressure { get; set; }

        public float? TopTime { get; set; }

        public float? BottomTemp { get; set; }

        public float? BottomPressure { get; set; }

        public float? BottomTime { get; set; }

        public float? SideTemp { get; set; }

        public float? SidePressure { get; set; }

        public float? SideTime { get; set; }

        public float? AngleTemp { get; set; }

        public float? AnglePressure { get; set; }

        public float? AngleTime { get; set; }

        public float? TabTestVoltage { get; set; }
    }
}
