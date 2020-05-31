using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace ATL_MC.Vision
{
    public class CameraConfig
    {
        public double CameraScale_MoveIn { get; set; } = 0.13215859030837004405286343612335;
        public double CameraScale_TrayA { get; set; } = 0.18798449612403100775193798449612;
        public double CameraScale_TrayB { get; set; } = 0.18798449612403100775193798449612;
        public double CameraScale_TrayC { get; set; } = 0.18798449612403100775193798449612;
        public double CameraScale_TrayD { get; set; } = 0.18798449612403100775193798449612;
        public double CameraScale_TrayE { get; set; } = 0.18798449612403100775193798449612;

        public long CameraWidth_MoveIn { get; set; } = 1700;
        public long CameraWidthOffset_MoveIn { get; set; } = 0;
        public long CameraHeight_MoveIn { get; set; } = 1800;
        public long CameraHeightOffset_MoveIn { get; set; } = 120;

        public long CameraWidth_TrayA { get; set; } = 2592;
        public long CameraWidthOffset_TrayA { get; set; } = 0;
        public long CameraHeight_TrayA { get; set; } = 1944;
        public long CameraHeightOffset_TrayA { get; set; } = 0;

        public long CameraWidth_TrayB { get; set; } = 2592;
        public long CameraWidthOffset_TrayB { get; set; } = 0;
        public long CameraHeight_TrayB { get; set; } = 1944;
        public long CameraHeightOffset_TrayB { get; set; } = 0;
        public long CameraWidth_TrayC { get; set; } = 2592;
        public long CameraWidthOffset_TrayC { get; set; } = 0;
        public long CameraHeight_TrayC { get; set; } = 1944;
        public long CameraHeightOffset_TrayC { get; set; } = 0;
        public long CameraWidth_TrayD { get; set; } = 2592;
        public long CameraWidthOffset_TrayD { get; set; } = 0;
        public long CameraHeight_TrayD { get; set; } = 1944;
        public long CameraHeightOffset_TrayD { get; set; } = 0;
        public long CameraWidth_TrayE { get; set; } = 2592;
        public long CameraWidthOffset_TrayE { get; set; } = 0;
        public long CameraHeight_TrayE { get; set; } = 1944;
        public long CameraHeightOffset_TrayE { get; set; } = 0;
        //定位视觉边界参数Row1
        public long lLocationParametersRow1;
        //定位视觉边界参数Row2
        public long lLocationParametersRow2;
        //定位视觉边界参数Col1
        public long lLocationParametersCol1;
        //定位视觉边界参数Col2
        public long lLocationParametersCol2;


    }

    public class BatteryVisionConfig
    {
        public string product { get; set; } = "416587";
        public double dSliveryMoveInCameraExposureTime { get; set; } = 18000;      
        public double dBlackMoveInCameraExposureTime { get; set; } = 30000;
        public double CameraExposureTime_TrayA { get; set; } = 10000;
        public double CameraExposureTime_TrayB { get; set; } = 10000;
        public double CameraExposureTime_TrayC { get; set; } = 10000;
        public double CameraExposureTime_TrayD { get; set; } = 10000;
        public double CameraExposureTime_TrayE { get; set; } = 10000;
        //异物检测图片曝光时间
        public double CameraForiegnDetectExposureTime_TrayA { get; set; }
        public double CameraForiegnDetectExposureTime_TrayB { get; set; }
        public double CameraForiegnDetectExposureTime_TrayC { get; set; }
        public double CameraForiegnDetectExposureTime_TrayD { get; set; }
        public double CameraForiegnDetectExposureTime_TrayE { get; set; }
        //料盒里面有电池检测图片的曝光时间
        public double BatteryInTrayDetectExposureTime_TrayA { get; set; }
        public double BatteryInTrayDetectExposureTime_TrayB { get; set; }
        public double BatteryInTrayDetectExposureTime_TrayC { get; set; }
        public double BatteryInTrayDetectExposureTime_TrayD { get; set; }
        public double BatteryInTrayDetectExposureTime_TrayE { get; set; }
        public double dWidth { get; set; } = 65.0;
        public double dHeight { get; set; } = 87.0;
        public double dMoveInTargetX { get; set; } = 930.77;
        public double dMoveInTargetY { get; set; } = 909.01;
        //TODO:视觉参数，干嘛用？
        public double dTrayTargetX { get; set; } = 887.0;
        //TODO:视觉参数，干嘛用？
        public double dTrayTargetY { get; set; } = 810.0;
        //1银色   0白色
        public int iBatteryColor { get; set; } = 0;
        //视觉参数
        public double[] imageprocessparamater { get; set; } = new double[20];

        public BatteryVisionConfig()
        {
            for (int i = 0; i < imageprocessparamater.Length; i++)
            {
                imageprocessparamater[i] = 0.0;
            }
        }
    }
}
