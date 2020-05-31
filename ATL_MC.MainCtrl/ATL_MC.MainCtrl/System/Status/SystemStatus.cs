using ATL_MC.MainCtrl.Enum;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ATL_MC.MainCtrl.System
{
    /// <summary>
    /// 系统运行的相关变量
    /// </summary>
    public class SystemStatus : BaseStatus
    {
        /// <summary>
        /// 拉带线程Step
        /// </summary>
        public int Thread_MoveInStep { get; set; } = 1;
        /// <summary>
        /// 机械手线程Step
        /// </summary>
        public int Thread_RobotStep { get; set; } = 1;
        /// <summary>
        /// 料盘线程Step
        /// </summary>
        public int Thread_TrayStep { get; set; } = 1;
        /// <summary>
        /// 料盘视觉线程Step
        /// </summary>
        public int Thread_TrayVisionStep { get; set; }
        /// <summary>
        /// 回零线程Step
        /// </summary>
        public int Thread_HomeStep { get; set; }

        public int Thread_PLCStatusStep { get; set; }

        public int Thread_RobotIOStep { get; set; }



        #region 当前料盘放料索引
        /// <summary>
        /// 当前TrayA盘放料索引
        /// </summary>
        public int CurrentReleaseIndex_TrayA { get; set; }
        public int CurrentReleaseIndex_TrayB { get; set; }
        public int CurrentReleaseIndex_TrayC { get; set; }
        public int CurrentReleaseIndex_TrayD { get; set; }
        public int CurrentReleaseIndex_TrayE { get; set; }
        #endregion

        /// <summary>
        /// 设定当前运行的产品是否设置(是否读取产品参数)
        /// </summary>
        public bool IsProductSelected { get; set; }


        /// <summary>
        /// 料盘是否进行视觉检测
        /// </summary>
        public bool VisionChecked_TrayA { get; set; }
        public bool VisionChecked_TrayB { get; set; }
        public bool VisionChecked_TrayC { get; set; }
        public bool VisionChecked_TrayD { get; set; }
        public bool VisionChecked_TrayE { get; set; }



        /// <summary>
        /// 拉带相片路径
        /// </summary>
        public string PicturePath_MoveIn { get; set; }
        /// <summary>
        /// trayA相片路径
        /// </summary>
        //public string PicturePath_TrayA { get; set; }
        //public string PicturePath_TrayB { get; set; }
        //public string PicturePath_TrayC { get; set; }
        //public string PicturePath_TrayD { get; set; }
        //public string PicturePath_TrayE { get; set; }

        /// <summary>
        /// 拉带视觉处理完后赋值的X方向偏差值
        /// </summary>
        public double CatchOffsetX_Current { get; set; }

        /// <summary>
        /// 机械手正在抓取的 X偏差值
        /// </summary>
        public double CatchOffsetX_Running { get; set; }

        /// <summary>
        /// 拉带视觉处理完后赋值的Y方向偏差值
        /// </summary>
        public double CatchOffsetY_Current { get; set; }
        /// <summary>
        /// 机械手正在抓取的Y方向偏差值
        /// </summary>
        public double CatchOffsetY_Running { get; set; }

        /// <summary>
        /// 拉带视觉处理完后赋值的U轴角度偏差值
        /// </summary>
        public double CatchOffsetAngle_Current { get; set; }

        /// <summary>
        ///机械手正在抓取的U轴角度偏差值
        /// </summary>
        public double CatchOffsetAngle_Running { get; set; }

        /// <summary>
        /// 扫描枪扫码后赋值的BarCode
        /// </summary>
        public string BarCode_Current { get; set; }
        /// <summary>
        /// 机械手正在抓取的BarCode
        /// </summary>
        public string BarCode_Running { get; set; }

        /// <summary>
        /// 扫码枪扫描结果类别
        /// </summary>
        public EnumBarcodeScanner_ResultType EnumBarcodeScanner_ResultType { get; set; }

        #region 未知作用参数
        /// <summary>
        /// 放料成功的数量
        /// </summary>
        //public ulong CatchOKCount { get; set; }
        /// <summary>
        /// BIS数量
        /// </summary>
        //public ulong BisCount { get; set; }

        #endregion

        public UInt32 Yield_TrayA { get; set; }
        public UInt32 Yield_TrayB { get; set; }
        public UInt32 Yield_TrayC { get; set; }
        public UInt32 Yield_TrayD { get; set; }
        public UInt32 Yield_TrayE { get; set; }
        public UInt32 TotalYeild { get; set; }
        public Stopwatch TotalTime { get; set; } = new Stopwatch();

        #region 系统变量
        /// <summary>
        /// 系统暂停,运行中,错误,等状态暂不设定,后续一起考虑
        /// 严重错误,主要负载无法连接时,系统无法运行
        /// </summary>
        //public bool Sys_CriticalError { get; set; }

        //系统暂停,运行中,错误,等状态暂不设定,后续一起考虑
        //public bool Sys_Pause { get; set; }

        /// <summary>
        /// 拉带电池扫码后,等待机械手将手里电池放入tray盘
        /// </summary>
        public bool Sys_MoveInContinus { get; set; }

        /// <summary>
        /// 拉带电池扫码+BIS请求+拍照+视觉处理完成后 设置成True
        /// </summary>
        public bool Sys_BatteryIsReady { get; set; }

        /// <summary>
        /// 系统运行中
        /// </summary>
        public bool Sys_Running { get; set; }
        /// <summary>
        /// 系统暂停
        /// </summary>
        public bool Sys_Pause { get; set; }
        /// <summary>
        /// 系统报警
        /// </summary>
        public bool Sys_Alarm{ get; set; }
 




        #endregion



        #region Robot IO状态

        /// <summary>
        /// 门打开
        /// </summary>
        public bool Robot_Input_SafeDoorIsOpen { get; set; }
        /// <summary>
        /// NGBox在位
        /// </summary>
        public bool Robot_Input_NGBoxInplace { get; set; }
        /// <summary>
        /// NGBox满
        /// </summary>
        public bool Robot_Input_NGBoxIsFull { get; set; }
        /// <summary>
        /// 真空报警
        /// </summary>
        public bool Robot_Input_VacuumError { get; set; }
        /// <summary>
        /// 机械手在拉带区域
        /// </summary>
        public bool Robot_Ouput_RobotInMoveInArea { get; set; }
        /// <summary>
        /// 机械手在满Tray 1区
        /// </summary>
        public bool Robot_Ouput_RobotInFullTrayArea_1 { get; set; }
        /// <summary>
        /// 机械手在满Tray 2区
        /// </summary>
        public bool Robot_Ouput_RobotInFullTrayArea_2 { get; set; }
        /// <summary>
        /// 机械手在待机位A
        /// </summary>
        public bool Robot_Inner_RobotInStandbyA { get; set; }
        /// <summary>
        /// 机械手在待机位B
        /// </summary>
        public bool Robot_Inner_RobotInStandbyB { get; set; }


        /// <summary>
        /// 机械手报警
        /// </summary>
        public bool Robot_Output_RobotInAlarm { get; set; }
        /// <summary>
        /// 机械手严重报警
        /// </summary>
        public bool Robot_Output_RobotInFatalAlarm { get; set; }





        #endregion






        #region PLC状态   


        /// <summary>
        /// trayA出料中
        /// </summary>
        public bool PLC_Output_Discharging_TrayA { get; set; }
        public bool PLC_Output_Discharging_TrayB { get; set; }
        public bool PLC_Output_Discharging_TrayC { get; set; }
        public bool PLC_Output_Discharging_TrayD { get; set; }
        public bool PLC_Output_Discharging_TrayE { get; set; }
        /// <summary>
        /// trayA换料中
        /// </summary>
        public bool PLC_Output_Reloading_TrayA { get; set; }
        public bool PLC_Output_Reloading_TrayB { get; set; }
        public bool PLC_Output_Reloading_TrayC { get; set; }
        public bool PLC_Output_Reloading_TrayD { get; set; }
        public bool PLC_Output_Reloading_TrayE { get; set; }
        /// <summary>
        /// trayA清盘中
        /// </summary>
        public bool PLC_Output_Clearing_TrayA { get; set; }
        public bool PLC_Output_Clearing_TrayB { get; set; }
        public bool PLC_Output_Clearing_TrayC { get; set; }
        public bool PLC_Output_Clearing_TrayD { get; set; }
        public bool PLC_Output_Clearing_TrayE { get; set; }

        /// <summary>
        /// trayA已准备好
        /// </summary>
        public bool PLC_Output_IsReady_TrayA { get; set; }
        public bool PLC_Output_IsReady_TrayB { get; set; }
        public bool PLC_Output_IsReady_TrayC { get; set; }
        public bool PLC_Output_IsReady_TrayD { get; set; }
        public bool PLC_Output_IsReady_TrayE { get; set; }

        /// <summary>
        /// 复位中状态,为1
        /// </summary>
        public bool PLC_Output_Reset { get; set; }
        /// <summary>
        /// 开始状态,按下为1
        /// </summary>
        public bool PLC_Output_Start { get; set; }
        /// <summary>
        /// 暂停状态,为1
        /// </summary>
        public bool PLC_Output_Pause { get; set; }
        /// <summary>
        /// 急停状态,无效
        /// </summary>
        public bool PLC_Output_E_Stop { get; set; }

        /// <summary>
        /// 入料口电池到位可以扫码
        /// </summary>
        public bool PLC_Output_MoveInCanScan { get; set; }
        /// <summary>
        /// PLC报警,值为true时PLC报警
        /// </summary>
        public bool PLC_Output_Alarm { get; set; }
        /// <summary>
        /// PLC报警地址
        /// </summary>
        public ushort[] PLC_Output_AlarmAddrs { get; set; }


        #endregion






        #region BIS相关
        public bool IsBisRequestFinished { get; set; }


        /// <summary>
        /// Bis返回结果类别 Bis请求完赋值
        /// </summary>
        public EnumBisReturn_ResultType EnumBisReturn_ResultType_Current { get; set; }

        /// <summary>
        /// Bis返回结果类别 机械手正在抓取的
        /// </summary>
        public EnumBisReturn_ResultType EnumBisReturn_ResultType_Running { get; set; }



        #endregion





        #region 备用



        //public void SWStart()
        //{
        //    TotalTime.Start();
        //}

        //public void SWStop()
        //{
        //    TotalTime.Stop();
        //}

        //public long SWGetTime()
        //{
        //    return TotalTime.ElapsedMilliseconds;
        //}

        //public double GetPPM()
        //{
        //    double ppm = 0.0;
        //    return ppm;
        //}
        ///// <summary>
        ///// 获取完好产量
        ///// </summary>
        ///// <returns></returns>
        //public UInt32 GetOKYeild()
        //{
        //    UInt32 count = 0;
        //    mut.WaitOne();
        //    count = OKYeild;
        //    mut.ReleaseMutex();
        //    return count;
        //}
        ///// <summary>
        ///// 产量计算
        ///// </summary>
        ///// <param name="OKCount"></param>
        ///// <param name="NGCount"></param>
        //public void YeildADD(UInt32 OKCount, UInt32 NGCount)
        //{
        //    mut.WaitOne();
        //    NowYield += (OKCount + NGCount);
        //    OKYeild += OKCount;
        //    mut.ReleaseMutex();
        //}
        ///// <summary>
        ///// 获取总产量(完好产量+NG产量)
        ///// </summary>
        ///// <returns></returns>
        //public UInt32 GetYeild()
        //{
        //    mut.WaitOne();
        //    UInt32 count = NowYield;
        //    mut.ReleaseMutex();
        //    return count;
        //}
        ///// <summary>
        ///// 设置产量
        ///// </summary>
        ///// <param name="SetTotalYeild"></param>
        ///// <param name="SetOKYeild"></param>
        ///// <returns></returns>
        //public UInt32 SetYeild(UInt32 SetTotalYeild, UInt32 SetOKYeild)
        //{
        //    mut.WaitOne();
        //    NowYield = SetTotalYeild;
        //    OKYeild = SetOKYeild;
        //    mut.ReleaseMutex();
        //    return 0;
        //}

        #endregion









    }

}
