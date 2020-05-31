using IFactory.Domain.Crafts.Base.Entities;
using System;

namespace IFactory.Domain.Crafts.Degassing.Entities
{
    public class DegassingFacilityProductionDataInfo : FacilityProductionDataInfo
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

        public string HeatNo { get; set; }

        public float? HeatTemp { get; set; }

        public float? HeatPressure { get; set; }

        public float? HeatTime { get; set; }

        public string TopNo { get; set; }

        public float? TopTemp { get; set; }

        public float? TopPressure { get; set; }

        public float? TopTime { get; set; }

        public string BottomNo { get; set; }

        public float? BottomTemp { get; set; }

        public float? BottomPressure { get; set; }

        public float? BottomTime { get; set; }

        public string SideNo { get; set; }

        public float? SideTemp { get; set; }

        public float? SidePressure { get; set; }

        public float? SideTime { get; set; }

        public string AngleNo { get; set; }

        public float? AngleTemp { get; set; }

        public float? AnglePressure { get; set; }

        public float? AngleTime { get; set; }

        public string InsulationTestNo { get; set; }

        public string InsulationTestResult { get; set; }

        public float? InsulationTabTestVoltage { get; set; }

        public float? InsulationTabTestTime { get; set; }

        public float? InsulationTabTestSize { get; set; }

        public float? InsulationTabBorderTestVoltage { get; set; }

        public float? InsulationTabBorderTestTime { get; set; }

        public float? InsulationTabBorderVoltage { get; set; }

        public float? InsulationTabBorderTime { get; set; }
    }
}
