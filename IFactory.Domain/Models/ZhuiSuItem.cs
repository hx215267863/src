using System;

namespace IFactory.Domain.Models
{
    public class ZhuiSuItem
    {
        public int Iden { get; set; }

        public DateTime ProductTime { get; set; }

        public string FacilityDiD { get; set; }

        public string BatteryBarCode { get; set; }

        public string DeviceNo { get; set; }

        public string Operator { get; set; }

        public int Result { get; set; }
        public string LocateReturn { get; set; }
        public int LocateErrcode { get; set; }
        public string LsideReturn { get; set; }
        public int LsideErrcode { get; set; }
		public string RsideReturn { get; set; }
        public int RsideErrcode { get; set; }
		public string TailReturn { get; set; }
        public int TailErrcode { get; set; }
        public string HipotReturn { get; set; }
        public int HipotErrcode { get; set; }

        public DateTime TimeStart { get; set; }

        public DateTime TimeEnd { get; set; }

        public string Keyword { get; set; }

        public DateTime? StartDate { get; set; }

        public string ProductNo { get; set; }

        public string ProcessDID { get; set; }

        public int FacilityDID { get; set; }

        public string FrontRetrun { get; set; }

        public double SidestripHeight { get; set; }

        public double SidestripWidth { get; set; }

        public double TopstripHeight { get; set; }

        public double MainBodyWidthTop { get; set; }

        public double MainBodyWidthButtom { get; set; }

        public double MainBodyWidth { get; set; }

        public double MainBodyHeight { get; set; }

        public double TopsealHeight { get; set; }

        public double DistanceBetweenTabs { get; set; }

        public double DistanceBetweenTab1Left { get; set; }

        public double DistanceBetwwnTab2Left { get; set; }

        public double DistanceBetweenTab { get; set; }

        public double LeftTabHeight { get; set; }
        public double LeftFoldingHightTop { get; set; }
        public double LeftFoldingHightBottom { get; set; }
        public double RightFoldingHightTop { get; set; }
        public double RightFoldingHightBottom { get; set; }

        public double SideLeftFoldingHightTop { get; set; }

        public double SideLeftFoldingHightButtom { get; set; }

        public double SideRightFoldingHightTop { get; set; }
		
		public double SideRightFoldingHightButtom { get; set; }

        public double SideThickness1 { get; set; }

        public double BagAreaWidth { get; set; }
        public string BackReturn { get; set; }
        public double BackErrcode { get; set; }
        public string FrontReturn { get; set; }
        public double FrontErrcode { get; set; }


        public double TabALToSlotDistanceRight { get; set; }

        public double TabALToSlotDistanceLeft { get; set; }

        public double SealantHeightOfLeft1 { get; set; }

        public double SealantHeightOfLeft2 { get; set; }

        public double SealantHeightOfRight1 { get; set; }

        public double SealantHeightOfRight2 { get; set; }

        public double SealantToSlotDistanceLeft { get; set; }

        public double SealantToSlotDistanceRight { get; set; }

        public int measmode { get; set; }

        public string FinalResult { get; set; }

        public double SidePoint1 { get; set; }

        public double SidePoint2 { get; set; }

        public double SidePoint3 { get; set; }

        public double TopPoint1 { get; set; }

        public double TopPoint2 { get; set; }

        public double TopPoint3 { get; set; }

        public double TabPoint1 { get; set; }

        public double TabPoint2 { get; set; }

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

        public string No { get; set; }

        public string EndProductno { get; set; }

        public string DeviceGroupDID { get; set; }
    }
}
