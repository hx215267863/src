using HalconDotNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace ATL_MC.Vision
{
    public class Vision
    {
        public static Mutex m_VisionMutex = new Mutex();

        public static CameraConfig cc;
        public static BatteryVisionConfig batteryCFG;

        private Tools tto;
        private HDevelopExport_Location_3_0 movein;
        //private HDevelopExport_5_0 tray;
        private HDevelopExport_7_0 tray;
        public static HTuple[] M = new HTuple[13];
        public static HTuple[] N = new HTuple[13];
        public static HTuple[] P = new HTuple[13];

        public static double[] mp1 = null;

        public Vision()
        {
            cc = new CameraConfig();
            batteryCFG = new BatteryVisionConfig();
            tto = new Tools();
            movein = new HDevelopExport_Location_3_0();
            tray = new HDevelopExport_7_0();
        }

        public void SetPara(double [] para)
        {
            mp1 = para;
        }

        public void SetMNP(int index, int[] Mvalue, int[] Nvalue, int[] Pvalue)
        {
            M[index] = new HTuple();
            N[index] = new HTuple();
            P[index] = new HTuple();

            for (int i = 0; i < Mvalue.Length; i++)
            {
                M[index][i] = Mvalue[i];
                N[index][i] = Nvalue[i];
                P[index][i] = Pvalue[i];
            }
        }

        public void SetBatteryType(BatteryVisionConfig s)
        {
            tray.inited = false;
            batteryCFG = s;
        }

        public void SetCameraConfig(CameraConfig s)
        {
            cc = s;
        }
/*
功      能：    
参      数：    
返  回  值：    
备      注：    无
*/
        public int GetBatteryPos(Byte[] bitmap ,long width,long height,out double deltaX,out double deltaY,out double deltaAngle,string name,bool flag)
        {
            deltaX = 0.0;
            deltaY = 0.0;
            deltaAngle = 00;
            double bx, by;
            if (flag)
            {
                return 0;
            }
  
            return movein.action_parse_coordinate(bitmap, width, height, out deltaX, out deltaY, out bx,out by,out deltaAngle,name);
        }

        public int GetBatteryPosByFile(string bitmap, long width, long height, out double deltaX, out double deltaY, out double deltaAngle, string name, bool flag)
        {
            deltaX = 0.0;
            deltaY = 0.0;
            deltaAngle = 00;
            double bx, by;
            if (flag)
            {
                return 0;
            }

            return movein.action_parse_coordinate(null, width, height, out deltaX, out deltaY, out bx, out by, out deltaAngle, name);
        }
        /*
        功      能：    
        参      数：    
        返  回  值：    
        备      注：    无
        */
        public int GetBatteryPosSP(Byte[] bitmap, long width, long height, out double deltaX, out double deltaY, out double bx, out double by, out double deltaAngle, string name, bool flag)
        {
            deltaX = 0.0;
            deltaY = 0.0;
            deltaAngle = 00;
            bx = 0.0;
            by = 0.0;
            if (flag)
            {
                return 0;
            }

            return movein.action_parse_coordinate(bitmap, width, height, out deltaX, out deltaY, out bx,out by,out deltaAngle, name);
        }

        public int CheckTrayTooDark(Byte[] bitmap, long width, long height, int index,int Threshold,out double MeanGray)
        {
            MeanGray = 0;

            return tray.CheckTrayTooDark(bitmap, width, height,index, Threshold, out MeanGray);       
        }

        /*
        功      能：    
        参      数：    
        返  回  值：    
        备      注：    无
        */
        public int CheckBatteryStatus(Byte[] bitmap, long width, long height,int index, string name, out double x, out double y, out double a,bool flag,bool zzz)
        {
            x = 0.0;
            y = 0.0;
            a = 0.0;

            if (flag)
            {
                return 0;
            }
            double x1, y1, a1;
            int iret = tray.actionTray(batteryCFG.product, bitmap, width, height, index, name, out x1, out y1, out a1,zzz);
            //TODO:视觉参数需要处理
            //x = (y1 - batteryCFG.dTrayTargetY) * cc.dTrayCameraScale;
            //y = (batteryCFG.dTrayTargetX - x1) * cc.dTrayCameraScale;
            a = 0.0;
            return iret;
        }
/*
功      能：    
参      数：    
返  回  值：    
备      注：    无
*/
        public int CheckBatteryStatusFile(string bitmap, long width, long height, int index, string name, out double x, out double y, out double a, bool flag, bool zzz)
        {
            x = 0.0;
            y = 0.0;
            a = 0.0;

            if (flag)
            {
                return 0;
            }
            double x1, y1, a1;
            int iret = tray.actionTray(batteryCFG.product, bitmap, width, height, index, name, out x1, out y1, out a1, zzz);
            //TODO:视觉参数需要处理
            //x = (y1 - batteryCFG.dTrayTargetY) * cc.dTrayCameraScale;
            //y = (batteryCFG.dTrayTargetX - x1) * cc.dTrayCameraScale;
            a = 0.0;
            return iret;
        }

        public int SavePicBMP(Byte[] bmp, int width, int height, string filename)
        {
            return tto.SavePicBMP(bmp, width, height, filename);
        }

        public int SavePicJPEG(Byte[] bmp, int width, int height, string filename)
        {
            return tto.SavePicJPEG(bmp, width, height, filename);
        }
    }
}
